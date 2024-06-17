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

    public List<Vector3> vectorList = new List<Vector3>();  //获取excel中的数据

    public bool flag = true;

    public float demUnityHeight = 0.5f;
    private float lsRealHeight = 1469.255f;
    //更大地形
    Vector2 latLngTopLeft = new Vector2(115.667314f, 29.832046f); // 左上角控制点的经纬度
    Vector2 latLngBottomRight = new Vector2(116.268867f, 29.230493f);// 右下角控制点的经纬度
   //创建数组存储景点位置，并存储每个实例化点；
    public Vector3[] Scenecs; 
    public GameObject[] ScenesPoints;
    private GameObject parentObject;
  

    public List<GameObject> visibleBalls = new List<GameObject>(); // 定义一个存放可见球体的 List

    public MonoScript scriptToAttach; // 要添加的脚本文件，高亮显示的脚本

    public Tooltip tooltip; // 提示组件

    public LineRenderer lineRenderer; // 引用LineRenderer组件

    public float lineWidth = 0.01f;  //控制线宽

    // 存储到Vector3数组中
    public Vector3[] ReadExcelDataVector3()
    {

        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet dataSet = excelReader.AsDataSet();
                DataTable dataTable = dataSet.Tables[0];

                // 获取DataTable中的行数
                int rowCount = dataTable.Rows.Count;
                if (vectorList.Count <= rowCount)
                {
                    // 遍历每一行数据
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // 跳过第一行（索引位置为0）
                        if (dataTable.Rows.IndexOf(row) == 0)
                        {
                            continue;
                        }

                        // 读取第二至第四列，并转换为Vector3类型
                        float x = float.Parse(row[1].ToString());
                        float y = float.Parse(row[2].ToString());
                        float z = float.Parse(row[3].ToString());

                        // 判断是否有内容，若某一列为空则停止添加内容
                        if (x == 0 && y == 0 && z == 0)
                        {
                            break;
                        }

                        Vector3 vector = new Vector3(x, y, z);

                        // 将Vector3添加到数组中
                        vectorList.Add(vector);

                    }

                }

            }


        }
        flag = false;
        return vectorList.ToArray();
    }


    //判断是否下拉框选择打开文件
    void Start()
    {
        //openFileButton.onClick.AddListener(OnOpenFile);
        FileControlDropdown.onValueChanged.AddListener(delegate {
            OnOpenFile(FileControlDropdown);
        });

    }

    //输出文件
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
        //    ////// 输出Vector3数组中的每个元素
        //    //foreach (Vector3 vector in vectorArray)
        //    //{
        //    //    Debug.Log("ExcelTest中读取经纬度");
        //    //    Debug.Log(vector);
        //    //}
        //    PointShowLine3();
        //}
    }

    //打开文件
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
        // 获取地形的大小  手动赋值
        float terrainWidth = 10;
        float terrainLength = 10;
        int length = vectorList.Count;

        //控制点 仅需经纬度
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        //并对数组进行初始化
        Scenecs = new Vector3[vectorList.Count];
        ScenesPoints = new GameObject[vectorList.Count];

        //创建父对象 便于统一管理
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
            // 测试点在地形中的位置
            Scenecs[i] = ConvertLatLngToTerrainLine3(new Vector3(vectorList[i].x, vectorList[i].y, vectorList[i].z), topLeft, bottomRight, terrainWidth, terrainLength);

            //把各点存储起来
            ScenesPoints[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //点的大小和位置
            ScenesPoints[i].transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
            ScenesPoints[i].transform.position = new Vector3(Scenecs[i].x, Scenecs[i].y, Scenecs[i].z);

            if (i == 0 || i == length - 1)  //仅可见首位点
            {
                ScenesPoints[i].SetActive(true);  
                if(i==0)
                {
                    ScenesPoints[i].name = "起点";
                }
                else if(i==length-1)
                {
                    ScenesPoints[i].name = "终点";
                }
            }
           else
            {
                ScenesPoints[i].SetActive(false);  //使原物体不可见
            }

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

        CreateLine();

    }

    /// <summary>
    /// 画线法一，简单连接
    /// </summary>
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
        return real_Y + 0.05f + 0.1f - 0.009f - 0.04f - 0.0173f;
    }


    //清除导入数据
    public void CleanLine()
    {
        lineRenderer.positionCount = 0;
        for (int i = 0; i < ScenesPoints.Length; i++)
        {
            Destroy(ScenesPoints[i]);
        }
        flag = true;

        // 将数组置空
        ScenesPoints = null;
        Scenecs = null;
        vectorList.Clear();
       

    }
}
