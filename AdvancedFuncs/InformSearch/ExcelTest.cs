using ExcelDataReader;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Data;
using UnityEngine.UI;
using UnityEditor;


public class ExcelTest : MonoBehaviour
{
    private string filePath;

    // public Button openFileButton;
    //string[] names;//存储景点名字

    public Dropdown FileControlDropdown;

    public  List<Vector3> vectorList = new List<Vector3>();  //获取excel中的数据
    public List<string> namesList = new List<string>();  //存储景点名字


    // Vector3数组
    public Vector3[] ReadExcelDataVector3()
    {
     
        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet dataSet = excelReader.AsDataSet();
                    DataTable dataTable = dataSet.Tables[0];

                        // 获取DataTable中的行数
                      int  rowCount = dataTable.Rows.Count;
                    if(vectorList.Count <= rowCount)
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

        return vectorList.ToArray();
    }

    //读数景点名字
    public void ReadExcelDataName()
    {
        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet dataSet = excelReader.AsDataSet();
                DataTable dataTable = dataSet.Tables[0];

                // 清空namesList列表
                namesList.Clear();

                // 遍历每一行数据
                foreach (DataRow row in dataTable.Rows)
                {
                    // 跳过第一行（索引位置为0）
                    if (dataTable.Rows.IndexOf(row) == 0)
                    {
                        continue;
                    }

                    // 读取第一列的文字数据并添加到namesList中
                    string name = row[0].ToString();
                    namesList.Add(name);
                }
            }
        }
    }


    /// <summary>
    /// 打开文件
    /// </summary>
    void Start()
    {
        //openFileButton.onClick.AddListener(OnOpenFile);
        FileControlDropdown.onValueChanged.AddListener(delegate {
            OnOpenFile(FileControlDropdown);
        });

    }



    /// <summary>
    /// 输出vector3数组
    /// </summary>
    private void Update()
    {
        if (filePath != null)
        {
            Vector3[] vectorArray = ReadExcelDataVector3();
            ReadExcelDataName();
            // 输出namesList中的每个元素
            foreach (string name in namesList)
            {
                Debug.Log("ExcelTest中读取名字");
                Debug.Log(name);
            }

            
            //// 输出Vector3数组中的每个元素
            foreach (Vector3 vector in vectorArray)
            {
                Debug.Log("ExcelTest中读取经纬度");
                Debug.Log(vector);
            }
        }

    }

    /// <summary>
    /// 打开文件夹 的按钮
    /// </summary>
    //void OnOpenFile()
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

}