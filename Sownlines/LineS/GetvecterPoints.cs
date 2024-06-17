using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ExcelDataReader;
using System.Data;

public class GetvecterPoints : MonoBehaviour
{
    public string filePath1;

  public  List<Vector3> vectorList;

    public static GetvecterPoints instance;

    public static int length;

    public static int myVariable = 10;
    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
         filePath1 = Application.dataPath + "/line0504/line1.xlsx";

        Vector3[] vectorArray = ReadExcelDataVectorLine1();
        length = vectorList.Count;
        //Debug.Log("length" + length);

    }

    public Vector3[] ReadExcelDataVectorLine1()
    {
        vectorList = new List<Vector3>();

        using (FileStream stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet dataSet = excelReader.AsDataSet();
                DataTable dataTable = dataSet.Tables[0];

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

                    Vector3 vector = new Vector3(x, y, z);

                    // 将Vector3添加到数组中
                    vectorList.Add(vector);
                }
            }
        }

        return vectorList.ToArray();
    }
}
