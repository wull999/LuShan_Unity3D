using ExcelDataReader;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Data;
using UnityEngine.UI;
using UnityEditor;
using XCharts;

public class newExcelLine : MonoBehaviour
{
    private string filePath;

    public Dropdown FileControlDropdown;

    public List<Vector3> vectorList = new List<Vector3>();  //��ȡexcel�е�����

    public bool flag = true;

    public float demUnityHeight = 0.5f;
    private float lsRealHeight = 1469.255f;
    //�������
    Vector2 latLngTopLeft = new Vector2(115.667314f, 29.832046f); // ���Ͻǿ��Ƶ�ľ�γ��
    Vector2 latLngBottomRight = new Vector2(116.268867f, 29.230493f);// ���½ǿ��Ƶ�ľ�γ��
   //��������洢����λ�ã����洢ÿ��ʵ�����㣻
    public Vector3[] Scenecs; 
    public GameObject[] ScenesPoints;
    private GameObject parentObject;
  

    public List<GameObject> visibleBalls = new List<GameObject>(); // ����һ����ſɼ������ List

    public MonoScript scriptToAttach; // Ҫ��ӵĽű��ļ���������ʾ�Ľű�

    public Tooltip tooltip; // ��ʾ���

    public LineRenderer lineRenderer; // ����LineRenderer���

    public float lineWidth = 0.01f;  //�����߿�

    // �洢��Vector3������
    public Vector3[] ReadExcelDataVector3()
    {

        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet dataSet = excelReader.AsDataSet();
                DataTable dataTable = dataSet.Tables[0];

                // ��ȡDataTable�е�����
                int rowCount = dataTable.Rows.Count;
                if (vectorList.Count <= rowCount)
                {
                    // ����ÿһ������
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // ������һ�У�����λ��Ϊ0��
                        if (dataTable.Rows.IndexOf(row) == 0)
                        {
                            continue;
                        }

                        // ��ȡ�ڶ��������У���ת��ΪVector3����
                        float x = float.Parse(row[1].ToString());
                        float y = float.Parse(row[2].ToString());
                        float z = float.Parse(row[3].ToString());

                        // �ж��Ƿ������ݣ���ĳһ��Ϊ����ֹͣ�������
                        if (x == 0 && y == 0 && z == 0)
                        {
                            break;
                        }

                        Vector3 vector = new Vector3(x, y, z);

                        // ��Vector3��ӵ�������
                        vectorList.Add(vector);

                    }

                }

            }


        }
        flag = false;
        return vectorList.ToArray();
    }


    //�ж��Ƿ�������ѡ����ļ�
    void Start()
    {
        //openFileButton.onClick.AddListener(OnOpenFile);
        FileControlDropdown.onValueChanged.AddListener(delegate {
            OnOpenFile(FileControlDropdown);
        });

    }

    //����ļ�
    private void Update()
    {

        switch(FileControlDropdown.value)
        {
            case 0:
                break;

            case 1:
                if(flag)
                {
                    Vector3[] vectorArray = ReadExcelDataVector3();
                    PointShowLine3();
                }
                
                break;

            case 2:
                CleanLine();
                break;
        }

        //if (filePath != null&&flag==true)
        //{
        //    Vector3[] vectorArray = ReadExcelDataVector3();
        //    ////// ���Vector3�����е�ÿ��Ԫ��
        //    //foreach (Vector3 vector in vectorArray)
        //    //{
        //    //    Debug.Log("ExcelTest�ж�ȡ��γ��");
        //    //    Debug.Log(vector);
        //    //}
        //    PointShowLine3();
        //}
    }

    //���ļ�
    void OnOpenFile(Dropdown dropdown)
    {
        if (dropdown.value == 1)
        {
            string path = EditorUtility.OpenFilePanel("Open Excel File", "", "xlsx");
            if (!string.IsNullOrEmpty(path))
            {
                filePath = path;
                Debug.Log("Selected file path: " + filePath);
            }
        }
    }



    public void PointShowLine3()
    {
        // ��ȡ���εĴ�С  �ֶ���ֵ
        float terrainWidth = 10;
        float terrainLength = 10;
        int length = vectorList.Count;

        //���Ƶ� ���辭γ��
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //����������г�ʼ��
        Scenecs = new Vector3[vectorList.Count];
        ScenesPoints = new GameObject[vectorList.Count];

        //���������� ����ͳһ����
        if (parentObject == null)
        {
            parentObject = new GameObject("ParentObject");
            for (var i = 0; i < parentObject.transform.childCount; i++)
            {
                Destroy(parentObject.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < vectorList.Count; i++)
        {
            // ���Ե��ڵ����е�λ��
            Scenecs[i] = ConvertLatLngToTerrainLine3(new Vector3(vectorList[i].x, vectorList[i].y, vectorList[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //�Ѹ���洢����
            ScenesPoints[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //��Ĵ�С��λ��
            ScenesPoints[i].transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

            if (i == 0 || i == length - 1)  //���ɼ���λ��
            {
                ScenesPoints[i].SetActive(true);  
                if(i==0)
                {
                    ScenesPoints[i].name = "���";
                }
                else if(i==length-1)
                {
                    ScenesPoints[i].name = "�յ�";
                }
            }
           else
            {
                ScenesPoints[i].SetActive(false);  //ʹԭ���岻�ɼ�
            }

            ScenesPoints[i].AddComponent(scriptToAttach.GetClass());  //Ϊÿһ������ӽű�

            //�ѵ�ŵ����ص�����������
            ScenesPoints[i].transform.parent = parentObject.transform;

            // ��������ӵ���ʾ���
            tooltip.AddTooltip(ScenesPoints[i]);   //���tag

            //Ϊÿ������Ӵ���
            ScenesPoints[i].AddComponent<OBJInputText>();

            // ��ȡ�������ײ�����
            SphereCollider sphereCollider = ScenesPoints[i].GetComponent<SphereCollider>();  //��ȡ�������������������ײ��

            // ������ײ��İ뾶
            sphereCollider.radius = 0.55f; // ����������ײ��İ뾶Ϊ1
        }

        CreateLine();

    }

    /// <summary>
    /// ���߷�һ��������
    /// </summary>
    public void CreateLine()
    {
        lineRenderer.positionCount = ScenesPoints.Length;  //�õ���ĸ���

        for (int i = 0; i < ScenesPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, ScenesPoints[i].transform.position);
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }

    }

    // ���о�γ�Ȼ���
    Vector3 ConvertLatLngToTerrainLine3(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
    {
        //���㾭��
        float point_topleftLong = Mathf.Abs(p.x - topLeft.x);
        float topleft_botrightLong = Mathf.Abs(bottomRight.x - topLeft.x);
        float x = point_topleftLong / topleft_botrightLong * terrainWidth;
        // Debug.Log("x:"+x);

        //����γ��
        float point_botrightLat = Mathf.Abs(p.z - bottomRight.z);
        float topleft_botrightLat = Mathf.Abs(topLeft.z - bottomRight.z);
        float z = (point_botrightLat / topleft_botrightLat) * 1.0f * terrainLength;

        //����߶�
        float real_height = GetRealHeightLine3(p.y);

        return new Vector3(x, real_height, z);
    }


    // ������unity����ʵ�߶�
    public float GetRealHeightLine3(float realhight)
    {

        float real_Y = realhight * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        //return real_Y + 0.05f + 0.3f ;
        return real_Y + 0.05f + 0.1f - 0.009f - 0.04f - 0.0173f;
    }


    //�����������
    public void CleanLine()
    {
        lineRenderer.positionCount = 0;
        for (int i = 0; i < ScenesPoints.Length; i++)
        {
            Destroy(ScenesPoints[i]);
        }
        flag = true;

        // �������ÿ�
        ScenesPoints = null;
        Scenecs = null;
        vectorList.Clear();
       

    }
}
