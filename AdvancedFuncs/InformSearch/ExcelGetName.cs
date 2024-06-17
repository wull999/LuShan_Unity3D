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


    public List<string> namesList = new List<string>();  //�洢��������

    public Dropdown FileControlDropdown;


    // ��Excel�ļ��ж�ȡ���ݲ�����ת��ΪVector3���飬��ŵ�vectorList
    public void ReadExcelData()
    {
        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet dataSet = excelReader.AsDataSet();
                DataTable dataTable = dataSet.Tables[0];

                // ���namesList�б�
                namesList.Clear();

                // ����ÿһ������
                foreach (DataRow row in dataTable.Rows)
                {
                    // ������һ�У�����λ��Ϊ0��
                    if (dataTable.Rows.IndexOf(row) == 0)
                    {
                        continue;
                    }

                    // ��ȡ��һ�е��������ݲ���ӵ�namesList��
                    string name = row[0].ToString();
                    namesList.Add(name);
                }
            }
        }
    }

    /// <summary>
    /// ���ļ�  һ����
    /// </summary>
    void Start()
    {
        //openFileButton.onClick.AddListener(OnOpenFile);
        FileControlDropdown.onValueChanged.AddListener(delegate {
            OnOpenFile(FileControlDropdown);
        });

    }



    /// <summary>
    /// ���vector3����
    /// </summary>
    private void Update()
    {
        if (filePath != null)
        {
            ReadExcelData();

            Debug.Log("ExcelTest��");

            // ���namesList�е�ÿ��Ԫ��
            foreach (string name in namesList)
            {
                Debug.Log(name);
            }
        }

    }

    /// <summary>
    /// ���ļ��� �İ�ť
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

                // ��ȡExcel����
                //ReadExcelData();
            }
        }

    }

}