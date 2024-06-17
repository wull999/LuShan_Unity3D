using System.Collections;
using System.Data;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using Excel;
using System.Data.SqlClient;

public class DrawLines : MonoBehaviour
{
    //Vector2 latLngTopLeft = new Vector2(115.83727f, 29.69411f); // ���Ͻǿ��Ƶ�ľ�γ��
    //Vector2 latLngBottomRight = new Vector2(116.13963f, 29.39175f);// ���½ǿ��Ƶ�ľ�γ��

    //testnew terrain
    //Vector2 latLngTopLeft = new Vector2(115.77687f, 29.770594f); // ���Ͻǿ��Ƶ�ľ�γ��
    //Vector2 latLngBottomRight = new Vector2(116.250397f, 29.30655f);// ���½ǿ��Ƶ�ľ�γ��

    public Terrain terrain; // ����

    Vector2 latLngTopLeft = new Vector2(115.667314f, 29.832046f); // ���Ͻǿ��Ƶ�ľ�γ��
    Vector2 latLngBottomRight = new Vector2(116.268867f, 29.230493f);// ���½ǿ��Ƶ�ľ�γ��

    public Transform p1, p2;  //�������Ƶ�

    public float demUnityHeight;   //���θ߶�
    private float lsRealHeight = 1469.255f;

   public   Vector3[] vector3Array;   //������洢��������

    //��������洢����λ�ã����洢ÿ��ʵ�����㣻
    public Vector3[] Scenecs;
   public GameObject[] ScenesPoints;

    public LineRenderer lineRenderer; // ����LineRenderer���

    public Tooltip tooltip; // ��ʾ���

    public MonoScript scriptToAttach; // Ҫ��ӵĽű��ļ���������ʾ�Ľű�

    public static DrawLines instance_DrawLines;   //���������Ǳߵ�������ĸ�����

    public float lineWidth = 0.01f;  //�����߿�

    public GameObject player;  //��������

    private GameObject parentObject;
    private GameObject newParent;

    public List<GameObject> visibleBalls = new List<GameObject>(); // ����һ����ſɼ������ List

    //public List<Vector3> calculatePoint = new List<Vector3>();  //չʾ��ľ�γ��

    public List<int> calculateIndex = new List<int>();

    //���ݿ�
    private DatabaseLine2 dataGetLine2;
    private DatabaseLine4 dataGetLine4;

    private void Start()
    {
        demUnityHeight = terrain.terrainData.size.y;
    }


    private void Awake()
    {
        instance_DrawLines = this;

    }



    // ��ʾ·��һ
    public void PointShowLine1()
    {
        int length = GetvecterPoints.length;

        vector3Array = new Vector3[length];

        vector3Array = GetvecterPoints.instance.vectorList.ToArray();   //�õ�ԭʼ������


        // ��ȡ���εĴ�С
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;

        //���Ƶ� ���辭γ��
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //����������г�ʼ��
       Scenecs = new Vector3[vector3Array.Length];
       ScenesPoints = new GameObject[vector3Array.Length];

        //���������� ����ͳһ����
        if (parentObject == null)
        {
            parentObject = new GameObject("ParentObject");
            for (var i = 0; i < parentObject.transform.childCount; i++)
            {
                Destroy(parentObject.transform.GetChild(i).gameObject);
            }
        }

        //��ת��
        for (int i = 0; i < vector3Array.Length; i++)
        {
            // ���Ե��ڵ����е�λ��
            Scenecs[i] = ConvertLatLngToTerrainLine1(new Vector3(vector3Array[i].x, vector3Array[i].y, vector3Array[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //�Ѹ���洢����
            ScenesPoints[i]= GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //��Ĵ�С��λ��
            //ScenesPoints[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

            if (i == 0 || i == 6 || i == 36 || i == 43 || i == 57||i== length - 1)
            {
                visibleBalls.Add(ScenesPoints[i]);  //����ܿ���������

                //��ſ��ü��ĵ�ľ�γ�Ⱥͺ�����Ϣ
                //calculatePoint.Add(vector3Array[i]);

                calculateIndex.Add(i);

            }

            ScenesPoints[i].SetActive(false);  //ʹԭ���岻�ɼ�

            //�ѵ�ŵ����ص�����������
            ScenesPoints[i].transform.parent = parentObject.transform;

            
        }

        //���������� ����ͳһ����
        if (newParent == null)
        {
            newParent = new GameObject("newParent");
            for (var i = 0; i < newParent.transform.childCount; i++)
            {
                Destroy(newParent.transform.GetChild(i).gameObject);
            }
        }

        for (int i=0;i<visibleBalls.Count;i++)
        {
            visibleBalls[i].SetActive(true);
            visibleBalls[i].transform.parent = newParent.transform;

            //Debug.Log("����λ��:"+visibleBalls[i].transform.localPosition);

            switch (i)
            {
                case 0: visibleBalls[i].name = "���"; break;
                case 1: visibleBalls[i].name = "����ʯ"; break;
                case 2: visibleBalls[i].name = "����Ͽ"; break;
                case 3: visibleBalls[i].name = "����ɽˮ��"; break;
                case 4: visibleBalls[i].name = "�����·"; break;
                case 5: visibleBalls[i].name = "�յ�"; break;
            }

            visibleBalls[i].AddComponent(scriptToAttach.GetClass());  //Ϊÿһ������ӽű�

            visibleBalls[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

            // ��������ӵ���ʾ���
            tooltip.AddTooltip(visibleBalls[i]);   //���tag

            //Ϊÿ������Ӵ���
            visibleBalls[i].AddComponent<OBJInputText>();

            // ��ȡ�������ײ�����
            SphereCollider sphereCollider = visibleBalls[i].GetComponent<SphereCollider>();  //��ȡ�������������������ײ��

            // ������ײ��İ뾶
            sphereCollider.radius = 0.55f; // ����������ײ��İ뾶Ϊ1
        }

       
        CreateLine();

    }

    // ���о�γ�Ȼ���
    Vector3 ConvertLatLngToTerrainLine1(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
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
        float real_height = GetRealHeightLine1(p.y);
        
        return new Vector3(x, real_height, z);
    }

    // ������unity����ʵ�߶�
    public float GetRealHeightLine1(float realhight)
    {

        float real_Y = realhight * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        //return real_Y + 0.05f + 0.3f ;
        return real_Y + 0.05f + 0.1f - 0.009f - 0.08f;
    }





    /// ��ʾ·�߶�
    public void PointShowLine2()
    {
        //������ݿ���Ϣ
        dataGetLine2 = FindObjectOfType<DatabaseLine2>(); // ��ȡSQLDataLoader�ű���ʵ��

        if (dataGetLine2 != null)
        {
            foreach (Vector3 vector in dataGetLine2.vectorList)
            {
                Debug.Log(vector);
            }
        }
        else
        {
            Debug.LogError("dataGetLine2 script not found");
        }




        //  int length = getLine2.length;

        int length = DatabaseLine2.length;
        Debug.Log("���ݿ��е�length:" + length);

        vector3Array = new Vector3[length];

        //vector3Array = getLine2.instance.vectorList.ToArray();   //�õ�ԭʼ������
        vector3Array = dataGetLine2.vectorList.ToArray();   //�õ�ԭʼ������


        // ��ȡ���εĴ�С
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;

        //���Ƶ� ���辭γ��
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //����������г�ʼ��
        Scenecs = new Vector3[vector3Array.Length];
        ScenesPoints = new GameObject[vector3Array.Length];

        //���������� ����ͳһ����
        if (parentObject == null)
        {
            parentObject = new GameObject("ParentObject");
            for (var i = 0; i < parentObject.transform.childCount; i++)
            {
                Destroy(parentObject.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < vector3Array.Length; i++)
        {
            // ���Ե��ڵ����е�λ��
            Scenecs[i] = ConvertLatLngToTerrainLine2(new Vector3(vector3Array[i].x, vector3Array[i].y, vector3Array[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //�Ѹ���洢����
            ScenesPoints[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //��Ĵ�С��λ��
            //ScenesPoints[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);
            ScenesPoints[i].SetActive(false);  //ʹԭ���岻�ɼ�

            if (i == 0 || i == 8 || i == 36 || i == 43 || i == length - 1)
            {
                visibleBalls.Add(ScenesPoints[i]);  //����ܿ���������

                calculateIndex.Add(i);

            }


            ScenesPoints[i].AddComponent(scriptToAttach.GetClass());  //Ϊÿһ������ӽű�

            //�ѵ�ŵ����ص�����������
            ScenesPoints[i].transform.parent = parentObject.transform;

            // ��������ӵ���ʾ���
            tooltip.AddTooltip(ScenesPoints[i]);   //���tag
            //tooltip.ShowTip();  //����չʾtips����  ����ʧ��

            //Ϊÿ������Ӵ���
            ScenesPoints[i].AddComponent<OBJInputText>();

            // ��ȡ�������ײ�����
            SphereCollider sphereCollider = ScenesPoints[i].GetComponent<SphereCollider>();  //��ȡ�������������������ײ��

            // ������ײ��İ뾶
            sphereCollider.radius = 0.55f; // ����������ײ��İ뾶Ϊ1
        }

        //���������� ����ͳһ����
        if (newParent == null)
        {
            newParent = new GameObject("newParent");
            for (var i = 0; i < newParent.transform.childCount; i++)
            {
                Destroy(newParent.transform.GetChild(i).gameObject);
            }
        }


        for (int i = 0; i < visibleBalls.Count; i++)
        {
            visibleBalls[i].SetActive(true);
            visibleBalls[i].transform.parent = newParent.transform;
            visibleBalls[i].transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);

            switch (i)
            {
                case 0: visibleBalls[i].name = "���"; break;
                case 1: visibleBalls[i].name = "����Ͽ"; break;
                case 2: visibleBalls[i].name = "����̶"; break;
                case 3: visibleBalls[i].name = "��վ���"; break;
                case 4: visibleBalls[i].name = "�յ�"; break;
            }
        }


        CreateLine();

    }

    // ���о�γ�Ȼ���
    Vector3 ConvertLatLngToTerrainLine2(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
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
        float real_height = GetRealHeightLine2(p.y);

        return new Vector3(x, real_height, z);
    }

    // ������unity����ʵ�߶�
    public float GetRealHeightLine2(float realhight)
    {

        float real_Y = realhight * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        //return real_Y + 0.05f + 0.3f ;
        return real_Y + 0.05f + 0.1f - 0.009f - 0.04f;
    }




    // ��ʾ·����
    public void PointShowLine3()
    {
        int length = Getline3.length;
        Debug.Log("DraeLine3�е�length:" + length);

        vector3Array = new Vector3[length];

        vector3Array = Getline3.instance.vectorList.ToArray();   //�õ�ԭʼ������

        // ��ȡ���εĴ�С
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;

        //���Ƶ� ���辭γ��
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //����������г�ʼ��
        Scenecs = new Vector3[vector3Array.Length];
        ScenesPoints = new GameObject[vector3Array.Length];

        //���������� ����ͳһ����
        if (parentObject == null)
        {
            parentObject = new GameObject("ParentObject");
            for (var i = 0; i < parentObject.transform.childCount; i++)
            {
                Destroy(parentObject.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < vector3Array.Length; i++)
        {
            // ���Ե��ڵ����е�λ��
            Scenecs[i] = ConvertLatLngToTerrainLine3(new Vector3(vector3Array[i].x, vector3Array[i].y, vector3Array[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //�Ѹ���洢����
            ScenesPoints[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //��Ĵ�С��λ��
            ScenesPoints[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

            if (i == 0 || i == 22 || i == 28 || i == 35 || i == 68 || i == length - 1)
            {
                visibleBalls.Add(ScenesPoints[i]);  //����ܿ���������
                calculateIndex.Add(i);
            }
            ScenesPoints[i].SetActive(false);  //ʹԭ���岻�ɼ�

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

        //���������� ����ͳһ����
        if (newParent == null)
        {
            newParent = new GameObject("newParent");
            for (var i = 0; i < newParent.transform.childCount; i++)
            {
                Destroy(newParent.transform.GetChild(i).gameObject);
            }
        }


        for (int i = 0; i < visibleBalls.Count; i++)
        {
            visibleBalls[i].SetActive(true);
            visibleBalls[i].transform.parent = newParent.transform;
            visibleBalls[i].transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);

            switch (i)
            {
                case 0: visibleBalls[i].name = "���"; break;
                case 1: visibleBalls[i].name = "��®"; break;
                case 2: visibleBalls[i].name = "®ɽʯ�̲����"; break;
                case 3: visibleBalls[i].name = "®ɽ�����ַ"; break;
                case 4: visibleBalls[i].name = "«�ֺ�"; break;
                case 5: visibleBalls[i].name = "�յ�"; break;
            }
        }

        CreateLine();

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
        return real_Y + 0.05f + 0.1f - 0.009f - 0.04f-0.0173f;
    }




    // ��ʾ·��4
    public void PointShowLine4()
    {
        //int length = GetLine4.length;

        //vector3Array = new Vector3[length];

        //vector3Array = GetLine4.instance.vectorList.ToArray();   //�õ�ԭʼ������



        //������ݿ���Ϣ
        dataGetLine4 = FindObjectOfType<DatabaseLine4>(); // ��ȡSQLDataLoader�ű���ʵ��

        if (dataGetLine4 != null)
        {
            foreach (Vector3 vector in dataGetLine4.vectorList)
            {
                Debug.Log(vector);
            }
        }
        else
        {
            Debug.LogError("dataGetLine4 script not found");
        }

        int length = DatabaseLine4.length;
        Debug.Log("���ݿ��е�length:" + length);

        vector3Array = new Vector3[length];

        vector3Array = dataGetLine4.vectorList.ToArray();   //�õ�ԭʼ������


        // ��ȡ���εĴ�С
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;

        //���Ƶ� ���辭γ��
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //����������г�ʼ��
        Scenecs = new Vector3[vector3Array.Length];
        ScenesPoints = new GameObject[vector3Array.Length];

        //���������� ����ͳһ����
        if (parentObject == null)
        {
            parentObject = new GameObject("ParentObject");
            for (var i = 0; i < parentObject.transform.childCount; i++)
            {
                Destroy(parentObject.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < vector3Array.Length; i++)
        {
            // ���Ե��ڵ����е�λ��
            Scenecs[i] = ConvertLatLngToTerrainLine4(new Vector3(vector3Array[i].x, vector3Array[i].y, vector3Array[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //�Ѹ���洢����
            ScenesPoints[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //��Ĵ�С��λ��
            ScenesPoints[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

            if (i == 0 || i == 8 || i == 50 || i == 80 || i == length - 1)
            {
                visibleBalls.Add(ScenesPoints[i]);  //����ܿ���������
                calculateIndex.Add(i);
            }
            ScenesPoints[i].SetActive(false);  //ʹԭ���岻�ɼ�

            ScenesPoints[i].AddComponent(scriptToAttach.GetClass());  //Ϊÿһ������ӽű�

            //�ѵ�ŵ����ص�����������
            ScenesPoints[i].transform.parent = parentObject.transform;

          
        }

        //���������� ����ͳһ����
        if (newParent == null)
        {
            newParent = new GameObject("newParent");
            for (var i = 0; i < newParent.transform.childCount; i++)
            {
                Destroy(newParent.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < visibleBalls.Count; i++)
        {
            visibleBalls[i].SetActive(true);
            visibleBalls[i].transform.parent = newParent.transform;
            visibleBalls[i].transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);

            switch (i)
            {
                case 0: visibleBalls[i].name = "���"; break;
                case 1: visibleBalls[i].name = "̫�ҷ�"; break;
                case 2: visibleBalls[i].name = "���Ϸ�"; break;
                case 3: visibleBalls[i].name = "����Ȫ"; break;
                case 4: visibleBalls[i].name = "�յ�"; break;
            }

            visibleBalls[i].AddComponent(scriptToAttach.GetClass());  //Ϊÿһ������ӽű�

            visibleBalls[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

            // ��������ӵ���ʾ���
            tooltip.AddTooltip(visibleBalls[i]);   //���tag

            //Ϊÿ������Ӵ���
            visibleBalls[i].AddComponent<OBJInputText>();

            // ��ȡ�������ײ�����
            SphereCollider sphereCollider = visibleBalls[i].GetComponent<SphereCollider>();  //��ȡ�������������������ײ��

            // ������ײ��İ뾶
            sphereCollider.radius = 0.55f; // ����������ײ��İ뾶Ϊ1

        }


        CreateLine();

    }

    // ���о�γ�Ȼ���
    Vector3 ConvertLatLngToTerrainLine4(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
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
        float real_height = GetRealHeightLine4(p.y);

        return new Vector3(x, real_height, z);
    }

    // ������unity����ʵ�߶�
    public float GetRealHeightLine4(float realhight)
    {

        float real_Y = realhight * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        //return real_Y + 0.05f + 0.3f ;
        return real_Y + 0.05f + 0.1f - 0.009f - 0.04f-0.049437f;
    }




    //���߷�һ��������
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


    // ���߷���
    public IEnumerator DelayForKeepUpdate()
    {
        WaitForSeconds waitTime = new(1 / 30f);

        while (true)
        {
            yield return waitTime;

            Vector3[] linePoints = new Vector3[ScenesPoints.Length];
            for (int i = 0; i < ScenesPoints.Length; i++)
            {
                linePoints[i] = ScenesPoints[i].transform.position;
                linePoints[i].y = 0.1f;
            }

            lineRenderer.positionCount = linePoints.Length;
            lineRenderer.SetPositions(linePoints);
        }
        
    }


    //����
    public void CleanLine()
    {
        // �������֮ǰ�����ĵ�
        foreach (GameObject point in ScenesPoints)
        {
            Destroy(point);
        }

        // ���������
        if (parentObject != null)
        {
            Destroy(parentObject);
        }

        // �������֮ǰ�����Ŀɼ���
        foreach (GameObject visibleBall in visibleBalls)
        {
            Destroy(visibleBall);
        }

        // ����¸�����
        if (newParent != null)
        {
            Destroy(newParent);
        }


        // ��ո���������������б�
        ScenesPoints = null;
        Scenecs = null;
        vector3Array = null;
        visibleBalls.Clear();
        calculateIndex.Clear();

        // ���û���
        lineRenderer.positionCount = 0;

        
    }



    void Update()
    {
        if (calculateIndex.Count != 0)
        {
            //Debug.Log("calculateIndex");
            //foreach (int vector in calculateIndex)
            //{
            //    Debug.Log(vector);
            //}

        }
        else
        {
            //Debug.Log("calculateIndex");
        }

    }
}
