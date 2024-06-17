using UnityEngine;
using System.Data;
using Excel;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GetLine6 : MonoBehaviour
{
    public string filePath; // Excel 文件路径
    public List<Data> dataArray = new List<Data>(); // 存储数据的数组

    public static int length;

    public static GetLine6 instance;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ReadExcelFile();
        //PrintDataArray();

       
    }

    // 定义一个数据结构来存储经纬度、海拔和属性信息
    public class Data
    {
        public float longitude;
        public float altitude;
        public float latitude;
        public float slope;
    }

    void ReadExcelFile()
    {
        filePath = Application.dataPath + "/line0504/line6.xlsx";
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        DataTable table = result.Tables[0]; // 假设数据在第一个表中

        for (int i = 1; i < table.Rows.Count; i++) // 从第二行开始读取数据，忽略标题行
        {
            DataRow row = table.Rows[i];
            Data data = new Data();
            data.longitude = float.Parse(row[1].ToString());
            data.altitude = float.Parse(row[2].ToString());
            data.latitude = float.Parse(row[3].ToString());
            data.slope = float.Parse(row[4].ToString());

            dataArray.Add(data);
        }

        //length = dataArray.Count;
        //Debug.Log("GetLine6里面的length" + length);
        excelReader.Close();
    }

    void PrintDataArray()
    {
        foreach (Data data in dataArray)
        {
            Debug.Log("Latitude: " + data.latitude + ", Longitude: " + data.longitude + ", Altitude: " + data.altitude + ", Slope: " + data.slope);
        }
    }
}



