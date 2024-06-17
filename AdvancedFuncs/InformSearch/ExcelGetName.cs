using ExcelDataReader;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Data;
using UnityEngine.UI;
using UnityEditor;


public class ExcelGetName : MonoBehaviour
{

    private string filePath;


    public List<string> namesList = new List<string>();  //存储景点名字

    public Dropdown FileControlDropdown;


    // 从Excel文件中读取数据并将其转换为Vector3数组，存放到vectorList
    public void ReadExcelData()
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
    /// 打开文件  一样的
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
            ReadExcelData();

            Debug.Log("ExcelTest中");

            // 输出namesList中的每个元素
            foreach (string name in namesList)
            {
                Debug.Log(name);
            }
        }

    }

    /// <summary>
    /// 打开文件夹 的按钮
    /// </summary>
 
    void OnOpenFile(Dropdown dropdown)
    {
        if (dropdown.value == 1)
        {
            string path = EditorUtility.OpenFilePanel("Open Excel File", "", "xlsx");
            if (!string.IsNullOrEmpty(path))
            {
                filePath = path;
                Debug.Log("Selected file path: " + filePath);

                // 读取Excel数据
                //ReadExcelData();
            }
        }

    }

}