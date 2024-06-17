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
    //Vector2 latLngTopLeft = new Vector2(115.83727f, 29.69411f); // 左上角控制点的经纬度
    //Vector2 latLngBottomRight = new Vector2(116.13963f, 29.39175f);// 右下角控制点的经纬度

    //testnew terrain
    //Vector2 latLngTopLeft = new Vector2(115.77687f, 29.770594f); // 左上角控制点的经纬度
    //Vector2 latLngBottomRight = new Vector2(116.250397f, 29.30655f);// 右下角控制点的经纬度

    public Terrain terrain; // 地形

    Vector2 latLngTopLeft = new Vector2(115.667314f, 29.832046f); // 左上角控制点的经纬度
    Vector2 latLngBottomRight = new Vector2(116.268867f, 29.230493f);// 右下角控制点的经纬度

    public Transform p1, p2;  //两个控制点

    public float demUnityHeight;   //地形高度
    private float lsRealHeight = 1469.255f;

   public   Vector3[] vector3Array;   //用数组存储景点数据

    //创建数组存储景点位置，并存储每个实例化点；
    public Vector3[] Scenecs;
   public GameObject[] ScenesPoints;

    public LineRenderer lineRenderer; // 引用LineRenderer组件

    public Tooltip tooltip; // 提示组件

    public MonoScript scriptToAttach; // 要添加的脚本文件，高亮显示的脚本

    public static DrawLines instance_DrawLines;   //方便旗帜那边调用这里的父对象

    public float lineWidth = 0.01f;  //控制线宽

    public GameObject player;  //控制漫游

    private GameObject parentObject;
    private GameObject newParent;

    public List<GameObject> visibleBalls = new List<GameObject>(); // 定义一个存放可见球体的 List

    //public List<Vector3> calculatePoint = new List<Vector3>();  //展示点的经纬度

    public List<int> calculateIndex = new List<int>();

    //数据库
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



    // 显示路线一
    public void PointShowLine1()
    {
        int length = GetvecterPoints.length;

        vector3Array = new Vector3[length];

        vector3Array = GetvecterPoints.instance.vectorList.ToArray();   //得到原始点数组


        // 获取地形的大小
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;

        //控制点 仅需经纬度
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //并对数组进行初始化
       Scenecs = new Vector3[vector3Array.Length];
       ScenesPoints = new GameObject[vector3Array.Length];

        //创建父对象 便于统一管理
        if (parentObject == null)
        {
            parentObject = new GameObject("ParentObject");
            for (var i = 0; i < parentObject.transform.childCount; i++)
            {
                Destroy(parentObject.transform.GetChild(i).gameObject);
            }
        }

        //点转换
        for (int i = 0; i < vector3Array.Length; i++)
        {
            // 测试点在地形中的位置
            Scenecs[i] = ConvertLatLngToTerrainLine1(new Vector3(vector3Array[i].x, vector3Array[i].y, vector3Array[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //把各点存储起来
            ScenesPoints[i]= GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //点的大小和位置
            //ScenesPoints[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

            if (i == 0 || i == 6 || i == 36 || i == 43 || i == 57||i== length - 1)
            {
                visibleBalls.Add(ScenesPoints[i]);  //存放能看见的物体

                //存放看得见的点的经纬度和海拔信息
                //calculatePoint.Add(vector3Array[i]);

                calculateIndex.Add(i);

            }

            ScenesPoints[i].SetActive(false);  //使原物体不可见

            //把点放到加载到父对象下面
            ScenesPoints[i].transform.parent = parentObject.transform;

            
        }

        //创建父对象 便于统一管理
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

            //Debug.Log("各点位置:"+visibleBalls[i].transform.localPosition);

            switch (i)
            {
                case 0: visibleBalls[i].name = "起点"; break;
                case 1: visibleBalls[i].name = "飞来石"; break;
                case 2: visibleBalls[i].name = "汉口峡"; break;
                case 3: visibleBalls[i].name = "大月山水库"; break;
                case 4: visibleBalls[i].name = "七里冲路"; break;
                case 5: visibleBalls[i].name = "终点"; break;
            }

            visibleBalls[i].AddComponent(scriptToAttach.GetClass());  //为每一个点添加脚本

            visibleBalls[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

            // 将球体添加到提示组件
            tooltip.AddTooltip(visibleBalls[i]);   //添加tag

            //为每个点添加代码
            visibleBalls[i].AddComponent<OBJInputText>();

            // 获取物体的碰撞体组件
            SphereCollider sphereCollider = visibleBalls[i].GetComponent<SphereCollider>();  //获取所有子物体添加球体碰撞器

            // 设置碰撞体的半径
            sphereCollider.radius = 0.55f; // 设置球形碰撞体的半径为1
        }

       
        CreateLine();

    }

    // 进行经纬度换算
    Vector3 ConvertLatLngToTerrainLine1(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
    {
        //计算经度
        float point_topleftLong = Mathf.Abs(p.x - topLeft.x);
        float topleft_botrightLong = Mathf.Abs(bottomRight.x - topLeft.x);
        float x = point_topleftLong / topleft_botrightLong * terrainWidth;
        // Debug.Log("x:"+x);

        //计算纬度
        float point_botrightLat = Mathf.Abs(p.z - bottomRight.z);
        float topleft_botrightLat = Mathf.Abs(topLeft.z - bottomRight.z);
        float z = (point_botrightLat / topleft_botrightLat) * 1.0f * terrainLength;

        //计算高度
        float real_height = GetRealHeightLine1(p.y);
        
        return new Vector3(x, real_height, z);
    }

    // 计算在unity的真实高度
    public float GetRealHeightLine1(float realhight)
    {

        float real_Y = realhight * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        //return real_Y + 0.05f + 0.3f ;
        return real_Y + 0.05f + 0.1f - 0.009f - 0.08f;
    }





    /// 显示路线二
    public void PointShowLine2()
    {
        //输出数据库信息
        dataGetLine2 = FindObjectOfType<DatabaseLine2>(); // 获取SQLDataLoader脚本的实例

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
        Debug.Log("数据库中的length:" + length);

        vector3Array = new Vector3[length];

        //vector3Array = getLine2.instance.vectorList.ToArray();   //得到原始点数组
        vector3Array = dataGetLine2.vectorList.ToArray();   //得到原始点数组


        // 获取地形的大小
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;

        //控制点 仅需经纬度
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //并对数组进行初始化
        Scenecs = new Vector3[vector3Array.Length];
        ScenesPoints = new GameObject[vector3Array.Length];

        //创建父对象 便于统一管理
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
            // 测试点在地形中的位置
            Scenecs[i] = ConvertLatLngToTerrainLine2(new Vector3(vector3Array[i].x, vector3Array[i].y, vector3Array[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //把各点存储起来
            ScenesPoints[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //点的大小和位置
            //ScenesPoints[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);
            ScenesPoints[i].SetActive(false);  //使原物体不可见

            if (i == 0 || i == 8 || i == 36 || i == 43 || i == length - 1)
            {
                visibleBalls.Add(ScenesPoints[i]);  //存放能看见的物体

                calculateIndex.Add(i);

            }


            ScenesPoints[i].AddComponent(scriptToAttach.GetClass());  //为每一个点添加脚本

            //把点放到加载到父对象下面
            ScenesPoints[i].transform.parent = parentObject.transform;

            // 将球体添加到提示组件
            tooltip.AddTooltip(ScenesPoints[i]);   //添加tag
            //tooltip.ShowTip();  //调用展示tips功能  调用失败

            //为每个点添加代码
            ScenesPoints[i].AddComponent<OBJInputText>();

            // 获取物体的碰撞体组件
            SphereCollider sphereCollider = ScenesPoints[i].GetComponent<SphereCollider>();  //获取所有子物体添加球体碰撞器

            // 设置碰撞体的半径
            sphereCollider.radius = 0.55f; // 设置球形碰撞体的半径为1
        }

        //创建父对象 便于统一管理
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
                case 0: visibleBalls[i].name = "起点"; break;
                case 1: visibleBalls[i].name = "剪刀峡"; break;
                case 2: visibleBalls[i].name = "碧龙潭"; break;
                case 3: visibleBalls[i].name = "电站大坝"; break;
                case 4: visibleBalls[i].name = "终点"; break;
            }
        }


        CreateLine();

    }

    // 进行经纬度换算
    Vector3 ConvertLatLngToTerrainLine2(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
    {
        //计算经度
        float point_topleftLong = Mathf.Abs(p.x - topLeft.x);
        float topleft_botrightLong = Mathf.Abs(bottomRight.x - topLeft.x);
        float x = point_topleftLong / topleft_botrightLong * terrainWidth;
        // Debug.Log("x:"+x);

        //计算纬度
        float point_botrightLat = Mathf.Abs(p.z - bottomRight.z);
        float topleft_botrightLat = Mathf.Abs(topLeft.z - bottomRight.z);
        float z = (point_botrightLat / topleft_botrightLat) * 1.0f * terrainLength;

        //计算高度
        float real_height = GetRealHeightLine2(p.y);

        return new Vector3(x, real_height, z);
    }

    // 计算在unity的真实高度
    public float GetRealHeightLine2(float realhight)
    {

        float real_Y = realhight * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        //return real_Y + 0.05f + 0.3f ;
        return real_Y + 0.05f + 0.1f - 0.009f - 0.04f;
    }




    // 显示路线三
    public void PointShowLine3()
    {
        int length = Getline3.length;
        Debug.Log("DraeLine3中的length:" + length);

        vector3Array = new Vector3[length];

        vector3Array = Getline3.instance.vectorList.ToArray();   //得到原始点数组

        // 获取地形的大小
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;

        //控制点 仅需经纬度
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //并对数组进行初始化
        Scenecs = new Vector3[vector3Array.Length];
        ScenesPoints = new GameObject[vector3Array.Length];

        //创建父对象 便于统一管理
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
            // 测试点在地形中的位置
            Scenecs[i] = ConvertLatLngToTerrainLine3(new Vector3(vector3Array[i].x, vector3Array[i].y, vector3Array[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //把各点存储起来
            ScenesPoints[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //点的大小和位置
            ScenesPoints[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

            if (i == 0 || i == 22 || i == 28 || i == 35 || i == 68 || i == length - 1)
            {
                visibleBalls.Add(ScenesPoints[i]);  //存放能看见的物体
                calculateIndex.Add(i);
            }
            ScenesPoints[i].SetActive(false);  //使原物体不可见

            ScenesPoints[i].AddComponent(scriptToAttach.GetClass());  //为每一个点添加脚本

            //把点放到加载到父对象下面
            ScenesPoints[i].transform.parent = parentObject.transform;

            // 将球体添加到提示组件
            tooltip.AddTooltip(ScenesPoints[i]);   //添加tag

            //为每个点添加代码
            ScenesPoints[i].AddComponent<OBJInputText>();

            // 获取物体的碰撞体组件
            SphereCollider sphereCollider = ScenesPoints[i].GetComponent<SphereCollider>();  //获取所有子物体添加球体碰撞器

            // 设置碰撞体的半径
            sphereCollider.radius = 0.55f; // 设置球形碰撞体的半径为1
        }

        //创建父对象 便于统一管理
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
                case 0: visibleBalls[i].name = "起点"; break;
                case 1: visibleBalls[i].name = "美庐"; break;
                case 2: visibleBalls[i].name = "庐山石刻博物馆"; break;
                case 3: visibleBalls[i].name = "庐山会议旧址"; break;
                case 4: visibleBalls[i].name = "芦林湖"; break;
                case 5: visibleBalls[i].name = "终点"; break;
            }
        }

        CreateLine();

    }

    // 进行经纬度换算
    Vector3 ConvertLatLngToTerrainLine3(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
    {
        //计算经度
        float point_topleftLong = Mathf.Abs(p.x - topLeft.x);
        float topleft_botrightLong = Mathf.Abs(bottomRight.x - topLeft.x);
        float x = point_topleftLong / topleft_botrightLong * terrainWidth;
        // Debug.Log("x:"+x);

        //计算纬度
        float point_botrightLat = Mathf.Abs(p.z - bottomRight.z);
        float topleft_botrightLat = Mathf.Abs(topLeft.z - bottomRight.z);
        float z = (point_botrightLat / topleft_botrightLat) * 1.0f * terrainLength;

        //计算高度
        float real_height = GetRealHeightLine3(p.y);

        return new Vector3(x, real_height, z);
    }

    // 计算在unity的真实高度
    public float GetRealHeightLine3(float realhight)
    {

        float real_Y = realhight * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        //return real_Y + 0.05f + 0.3f ;
        return real_Y + 0.05f + 0.1f - 0.009f - 0.04f-0.0173f;
    }




    // 显示路线4
    public void PointShowLine4()
    {
        //int length = GetLine4.length;

        //vector3Array = new Vector3[length];

        //vector3Array = GetLine4.instance.vectorList.ToArray();   //得到原始点数组



        //输出数据库信息
        dataGetLine4 = FindObjectOfType<DatabaseLine4>(); // 获取SQLDataLoader脚本的实例

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
        Debug.Log("数据库中的length:" + length);

        vector3Array = new Vector3[length];

        vector3Array = dataGetLine4.vectorList.ToArray();   //得到原始点数组


        // 获取地形的大小
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;

        //控制点 仅需经纬度
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //并对数组进行初始化
        Scenecs = new Vector3[vector3Array.Length];
        ScenesPoints = new GameObject[vector3Array.Length];

        //创建父对象 便于统一管理
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
            // 测试点在地形中的位置
            Scenecs[i] = ConvertLatLngToTerrainLine4(new Vector3(vector3Array[i].x, vector3Array[i].y, vector3Array[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //把各点存储起来
            ScenesPoints[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //点的大小和位置
            ScenesPoints[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

            if (i == 0 || i == 8 || i == 50 || i == 80 || i == length - 1)
            {
                visibleBalls.Add(ScenesPoints[i]);  //存放能看见的物体
                calculateIndex.Add(i);
            }
            ScenesPoints[i].SetActive(false);  //使原物体不可见

            ScenesPoints[i].AddComponent(scriptToAttach.GetClass());  //为每一个点添加脚本

            //把点放到加载到父对象下面
            ScenesPoints[i].transform.parent = parentObject.transform;

          
        }

        //创建父对象 便于统一管理
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
                case 0: visibleBalls[i].name = "起点"; break;
                case 1: visibleBalls[i].name = "太乙峰"; break;
                case 2: visibleBalls[i].name = "五老峰"; break;
                case 3: visibleBalls[i].name = "三叠泉"; break;
                case 4: visibleBalls[i].name = "终点"; break;
            }

            visibleBalls[i].AddComponent(scriptToAttach.GetClass());  //为每一个点添加脚本

            visibleBalls[i].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

            // 将球体添加到提示组件
            tooltip.AddTooltip(visibleBalls[i]);   //添加tag

            //为每个点添加代码
            visibleBalls[i].AddComponent<OBJInputText>();

            // 获取物体的碰撞体组件
            SphereCollider sphereCollider = visibleBalls[i].GetComponent<SphereCollider>();  //获取所有子物体添加球体碰撞器

            // 设置碰撞体的半径
            sphereCollider.radius = 0.55f; // 设置球形碰撞体的半径为1

        }


        CreateLine();

    }

    // 进行经纬度换算
    Vector3 ConvertLatLngToTerrainLine4(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
    {
        //计算经度
        float point_topleftLong = Mathf.Abs(p.x - topLeft.x);
        float topleft_botrightLong = Mathf.Abs(bottomRight.x - topLeft.x);
        float x = point_topleftLong / topleft_botrightLong * terrainWidth;
        // Debug.Log("x:"+x);

        //计算纬度
        float point_botrightLat = Mathf.Abs(p.z - bottomRight.z);
        float topleft_botrightLat = Mathf.Abs(topLeft.z - bottomRight.z);
        float z = (point_botrightLat / topleft_botrightLat) * 1.0f * terrainLength;

        //计算高度
        float real_height = GetRealHeightLine4(p.y);

        return new Vector3(x, real_height, z);
    }

    // 计算在unity的真实高度
    public float GetRealHeightLine4(float realhight)
    {

        float real_Y = realhight * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        //return real_Y + 0.05f + 0.3f ;
        return real_Y + 0.05f + 0.1f - 0.009f - 0.04f-0.049437f;
    }




    //画线法一，简单连接
    public void CreateLine()
    {
        lineRenderer.positionCount = ScenesPoints.Length;  //得到点的个数

        for (int i = 0; i < ScenesPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, ScenesPoints[i].transform.position);
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }

    }


    // 画线法二
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


    //重置
    public void CleanLine()
    {
        // 清除所有之前创建的点
        foreach (GameObject point in ScenesPoints)
        {
            Destroy(point);
        }

        // 清除父对象
        if (parentObject != null)
        {
            Destroy(parentObject);
        }

        // 清除所有之前创建的可见点
        foreach (GameObject visibleBall in visibleBalls)
        {
            Destroy(visibleBall);
        }

        // 清除新父对象
        if (newParent != null)
        {
            Destroy(newParent);
        }


        // 清空各点和线相关数组和列表
        ScenesPoints = null;
        Scenecs = null;
        vector3Array = null;
        visibleBalls.Clear();
        calculateIndex.Clear();

        // 重置画线
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
