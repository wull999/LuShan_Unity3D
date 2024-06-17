using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ExcelLine : MonoBehaviour
{
    public ExcelTest GetVector3Name;

    public List<string> nameCopyFile = new List<string>();  //�洢��������
    public List<Vector3> ExcelPoints = new List<Vector3>();   //���ExcelTest�е�����

    public Terrain terrain; // ����
    private float demUnityHeight;   //���θ߶�
    Vector2 latLngTopLeft = new Vector2(115.667314f, 29.832046f); // ���Ͻǿ��Ƶ�ľ�γ��
    Vector2 latLngBottomRight = new Vector2(116.268867f, 29.230493f);// ���½ǿ��Ƶ�ľ�γ��

    //��������洢����λ�ã����洢ÿ��ʵ�����㣻
    public Vector3[] Scenecs;
    public GameObject[] ScenesPoints_file;

    private float lsRealHeight = 1469.255f;

    public MonoScript HeighShow; // Ҫ��ӵĽű��ļ���������ʾ�Ľű�

    public Tooltip tooltip; // ��ʾ���

    private bool flag=true;

    public LineRenderer lineRenderer_file; // ����LineRenderer���
    public float lineWidth = 0.02f;  //�����߿�

    public Dropdown FileControlDropdown;
    private GameObject parentObject;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("��ExcleLine��Start");
    }

    // Update is called once per frame
    void Update()
    {
        switch(FileControlDropdown.value)
        {
            case 0:
                break;

            case 1:
                if (GetVector3Name.vectorList.Count != 0 && flag == true)
                {
                    //���鴫��
                    for (int i = 0; i < GetVector3Name.vectorList.Count; i++)
                    {
                        ExcelPoints.Add(GetVector3Name.vectorList[i]); // ������a��Ԫ�������ӵ�List<Vector3>����b��
                        if (i == GetVector3Name.vectorList.Count - 1)
                        {
                            flag = false;
                        }
                    }

                    
                         for (int i = 0; i < GetVector3Name.namesList.Count; i++)
                    {
                        nameCopyFile.Add(GetVector3Name.namesList[i]);
                        Debug.Log("ExcelLine�и��������鸳ֵ"+nameCopyFile);
                        
                    }

                    // ��ȡ���εĴ�С
                    float terrainWidth = terrain.terrainData.size.x;
                    float terrainLength = terrain.terrainData.size.z;
                    //terrain_width = terrainWidth;
                    demUnityHeight = terrain.terrainData.size.y;

                    //���Ƶ� ���辭γ��
                    Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
                    Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

                    //����������г�ʼ��
                    Scenecs = new Vector3[ExcelPoints.Count];
                    ScenesPoints_file = new GameObject[ExcelPoints.Count];

                    //���������� ����ͳһ����
                    if (parentObject == null)
                    {
                        parentObject = new GameObject("ParentObject");
                        for (var i = 0; i < parentObject.transform.childCount; i++)
                        {
                            Destroy(parentObject.transform.GetChild(i).gameObject);
                        }
                    }


                    for (int i = 0; i < ExcelPoints.Count; i++)
                    {
                        // ���Ե��ڵ����е�λ��
                        Scenecs[i] = ConvertLatLngToTerrain(new Vector3(ExcelPoints[i].x, ExcelPoints[i].y, ExcelPoints[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

                        //�Ѹ���洢����
                        ScenesPoints_file[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                        

                        //��Ĵ�С��λ��
                        ScenesPoints_file[i].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        ScenesPoints_file[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

                        ScenesPoints_file[i].AddComponent(HeighShow.GetClass());  //Ϊÿһ������ӽű�

                        //�ѵ�ŵ����ص�����������
                        ScenesPoints_file[i].transform.parent = parentObject.transform;

                        // ��������ӵ���ʾ���
                        tooltip.AddTooltip(ScenesPoints_file[i]);   //���tag
                                                                    //tooltip.ShowTip();  //����չʾtips����  ����ʧ��

                        //Ϊÿ������Ӵ���
                        ScenesPoints_file[i].AddComponent<OBJInputText>();

                        // ��ȡ�������ײ�����
                        SphereCollider sphereCollider = ScenesPoints_file[i].GetComponent<SphereCollider>();  //��ȡ�������������������ײ��

                        // ������ײ��İ뾶
                        sphereCollider.radius = 0.55f; // ����������ײ��İ뾶Ϊ1

                    }

                    //���������
                    for(int i=0;i< nameCopyFile.Count;i++)
                    {
                        ScenesPoints_file[i].name = nameCopyFile[i];
                        ScenesPoints_file[i].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    }

                    //����
                    CreateLine();


                }

                break;

            case 2:
                CleanLine();
                break;
        }
     
    }

    //��γ�Ȼ���
    Vector3 ConvertLatLngToTerrain(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
    {
        //���㾭��
        float point_topleftLong = Mathf.Abs(p.x - topLeft.x);
        float topleft_botrightLong = Mathf.Abs(bottomRight.x - topLeft.x);
        float x = point_topleftLong / topleft_botrightLong * terrainWidth;

        //����γ��
        float point_botrightLat = Mathf.Abs(p.z - bottomRight.z);
        float topleft_botrightLat = Mathf.Abs(topLeft.z - bottomRight.z);
        float z = (point_botrightLat / topleft_botrightLat) * 1.0f * terrainLength;

        //����߶�
        float real_height = GetRealHeight(p.y);

        return new Vector3(x, real_height, z);
    }

    //����dem������unity�еĸ߶�
    float GetRealHeight(float dem)
    {

        float real_H = dem * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        return real_H + 0.05f + 0.1f-0.009f;
    }

    //����
    public void CreateLine()
    {
        lineRenderer_file.positionCount = ScenesPoints_file.Length;  //�õ���ĸ���

        for (int i = 0; i < ScenesPoints_file.Length; i++)
        {
            lineRenderer_file.SetPosition(i, ScenesPoints_file[i].transform.position);
            lineRenderer_file.startWidth = lineWidth;
            lineRenderer_file.endWidth = lineWidth;
        }

    }

    //�����������
    public void CleanLine()
    {
        lineRenderer_file.positionCount = 0;
        for(int i=0;i< ScenesPoints_file.Length;i++)
        {
            Destroy(ScenesPoints_file[i]);
        }
    }
}
