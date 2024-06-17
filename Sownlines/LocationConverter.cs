using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LocationConverter : MonoBehaviour
{
    public Terrain terrain; // 地形

    Vector2 latLngTopLeft = new Vector2(115.83727f, 29.69411f); // 左上角控制点的经纬度
    Vector2 latLngBottomRight = new Vector2(116.13963f, 29.39175f);// 右下角控制点的经纬度

    public Transform p1, p2;  //两个控制点

    //景点
    Vector3 testLatLng = new Vector3(115.94649f, 573.5f, 29.55544f);


    //public string modelName = "正方体"; // 模型文件名
    private string modelName0 = "旗帜3"; // 模型文件名   只能用一次，首次设置名

    public Vector3 position;
    public Vector3 scale = new Vector3(10, 10, 10); // 模型缩放

    private float demUnityHeight;   //地形高度
    private float lsRealHeight = 1469.255f;


    GameObject modelInstance;
    // Start is called before the first frame update
    void Start()
    {
        //控制点 仅需经纬度
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        // 获取地形的大小
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;
        demUnityHeight = terrain.terrainData.size.y;

        // 测试点在地形中的位置
        Vector3 testPos = ConvertLatLngToTerrain(new Vector3(testLatLng.x, testLatLng.y, testLatLng.z), topLeft, bottomRight, terrainWidth, terrainLength);

        //// 加载并实例化模型
        GameObject model = Resources.Load<GameObject>(modelName0);   //+40
        position = new Vector3(testPos.x, testPos.y, testPos.z);

        //GameObject modelInstance = Instantiate(model);
         modelInstance = Instantiate(model, position, Quaternion.identity);
        modelInstance.name = modelName0; // 设置实例化物体的名称
        modelInstance.transform.localScale = scale;
        //modelInstance.name = "测试点";

        OnDisable();   //销毁点


        ////在Unity中显示测试点
        //GameObject testPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //testPoint.name = "测试点";
        //testPoint.transform.localScale = new Vector3(6, 6, 6);
        //testPoint.transform.position = new Vector3(testPos.x, testPos.y, testPos.z);


    }
    Vector3 ConvertLatLngToTerrain(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
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
        Debug.Log("point_botrightLat1111:" + point_botrightLat);
        Debug.Log("topleft_botrightLat1111:" + topleft_botrightLat);
        Debug.Log("terrainLength1111:" + terrainLength);
        Debug.Log("z1111:" + z);

        //计算高度
        float real_height = GetRealHeight(p.y);

        return new Vector3(x, real_height, z);
    }

    float GetRealHeight(float dem)
    {
      
        float real_H = dem * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        return real_H + 5 + 10 + 40;
    }

    // Update is called once per frame

    public void OnDisable()
    {

        DestroyImmediate(modelInstance);

        //Destroy(gameObject, 5.0f); // 5秒后销毁

        Debug.Log("正在销毁哦");

        //point.SetActive(false);  //不激活
        //Debug.Log("设置为不激活啦");

        //// 刷新场景视图
        //EditorApplication.QueuePlayerLoopUpdate();
        //    Debug.Log("场景刷新好啦");
        
    }

    void Update()
    {
        
    }
}
