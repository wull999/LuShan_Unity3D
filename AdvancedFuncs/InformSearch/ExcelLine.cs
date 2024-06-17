using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ExcelLine : MonoBehaviour
{
    public ExcelTest GetVector3Name;

    public List<string> nameCopyFile = new List<string>();  //存储景点名字
    public List<Vector3> ExcelPoints = new List<Vector3>();   //存放ExcelTest中的数组

    public Terrain terrain; // 地形
    private float demUnityHeight;   //地形高度
    Vector2 latLngTopLeft = new Vector2(115.667314f, 29.832046f); // 左上角控制点的经纬度
    Vector2 latLngBottomRight = new Vector2(116.268867f, 29.230493f);// 右下角控制点的经纬度

    //创建数组存储景点位置，并存储每个实例化点；
    public Vector3[] Scenecs;
    public GameObject[] ScenesPoints_file;

    private float lsRealHeight = 1469.255f;

    public MonoScript HeighShow; // 要添加的脚本文件，高亮显示的脚本

    public Tooltip tooltip; // 提示组件

    private bool flag=true;

    public LineRenderer lineRenderer_file; // 引用LineRenderer组件
    public float lineWidth = 0.02f;  //控制线宽

    public Dropdown FileControlDropdown;
    private GameObject parentObject;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("在ExcleLine中Start");
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
                    //数组传递
                    for (int i = 0; i < GetVector3Name.vectorList.Count; i++)
                    {
                        ExcelPoints.Add(GetVector3Name.vectorList[i]); // 将数组a的元素逐个添加到List<Vector3>数组b中
                        if (i == GetVector3Name.vectorList.Count - 1)
                        {
                            flag = false;
                        }
                    }

                    
                         for (int i = 0; i < GetVector3Name.namesList.Count; i++)
                    {
                        nameCopyFile.Add(GetVector3Name.namesList[i]);
                        Debug.Log("ExcelLine中给名字数组赋值"+nameCopyFile);
                        
                    }

                    // 获取地形的大小
                    float terrainWidth = terrain.terrainData.size.x;
                    float terrainLength = terrain.terrainData.size.z;
                    //terrain_width = terrainWidth;
                    demUnityHeight = terrain.terrainData.size.y;

                    //控制点 仅需经纬度
                    Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
                    Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

                    //并对数组进行初始化
                    Scenecs = new Vector3[ExcelPoints.Count];
                    ScenesPoints_file = new GameObject[ExcelPoints.Count];

                    //创建父对象 便于统一管理
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
                        // 测试点在地形中的位置
                        Scenecs[i] = ConvertLatLngToTerrain(new Vector3(ExcelPoints[i].x, ExcelPoints[i].y, ExcelPoints[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

                        //把各点存储起来
                        ScenesPoints_file[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                        

                        //点的大小和位置
                        ScenesPoints_file[i].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        ScenesPoints_file[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

                        ScenesPoints_file[i].AddComponent(HeighShow.GetClass());  //为每一个点添加脚本

                        //把点放到加载到父对象下面
                        ScenesPoints_file[i].transform.parent = parentObject.transform;

                        // 将球体添加到提示组件
                        tooltip.AddTooltip(ScenesPoints_file[i]);   //添加tag
                                                                    //tooltip.ShowTip();  //调用展示tips功能  调用失败

                        //为每个点添加代码
                        ScenesPoints_file[i].AddComponent<OBJInputText>();

                        // 获取物体的碰撞体组件
                        SphereCollider sphereCollider = ScenesPoints_file[i].GetComponent<SphereCollider>();  //获取所有子物体添加球体碰撞器

                        // 设置碰撞体的半径
                        sphereCollider.radius = 0.55f; // 设置球形碰撞体的半径为1

                    }

                    //赋予点名字
                    for(int i=0;i< nameCopyFile.Count;i++)
                    {
                        ScenesPoints_file[i].name = nameCopyFile[i];
                        ScenesPoints_file[i].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    }

                    //画线
                    CreateLine();


                }

                break;

            case 2:
                CleanLine();
                break;
        }
     
    }

    //经纬度换算
    Vector3 ConvertLatLngToTerrain(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
    {
        //计算经度
        float point_topleftLong = Mathf.Abs(p.x - topLeft.x);
        float topleft_botrightLong = Mathf.Abs(bottomRight.x - topLeft.x);
        float x = point_topleftLong / topleft_botrightLong * terrainWidth;

        //计算纬度
        float point_botrightLat = Mathf.Abs(p.z - bottomRight.z);
        float topleft_botrightLat = Mathf.Abs(topLeft.z - bottomRight.z);
        float z = (point_botrightLat / topleft_botrightLat) * 1.0f * terrainLength;

        //计算高度
        float real_height = GetRealHeight(p.y);

        return new Vector3(x, real_height, z);
    }

    //根据dem计算在unity中的高度
    float GetRealHeight(float dem)
    {

        float real_H = dem * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        return real_H + 0.05f + 0.1f-0.009f;
    }

    //画线
    public void CreateLine()
    {
        lineRenderer_file.positionCount = ScenesPoints_file.Length;  //得到点的个数

        for (int i = 0; i < ScenesPoints_file.Length; i++)
        {
            lineRenderer_file.SetPosition(i, ScenesPoints_file[i].transform.position);
            lineRenderer_file.startWidth = lineWidth;
            lineRenderer_file.endWidth = lineWidth;
        }

    }

    //清除导入数据
    public void CleanLine()
    {
        lineRenderer_file.positionCount = 0;
        for(int i=0;i< ScenesPoints_file.Length;i++)
        {
            Destroy(ScenesPoints_file[i]);
        }
    }
}
