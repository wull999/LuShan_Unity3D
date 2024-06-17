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
    //string[] names;//�洢��������

    public Dropdown FileControlDropdown;

    public  List<Vector3> vectorList = new List<Vector3>();  //��ȡexcel�е�����
    public List<string> namesList = new List<string>();  //�洢��������


    // Vector3����
    public Vector3[] ReadExcelDataVector3()
    {
     
        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet dataSet = excelReader.AsDataSet();
                    DataTable dataTable = dataSet.Tables[0];

                        // ��ȡDataTable�е�����
                      int  rowCount = dataTable.Rows.Count;
                    if(vectorList.Count <= rowCount)
                {
                    // ����ÿһ������
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // ������һ�У�����λ��Ϊ0��
                        if (dataTable.Rows.IndexOf(row) == 0)
                        {
                            continue;
                        }

                        // ��ȡ�ڶ��������У���ת��ΪVector3����
                        float x = float.Parse(row[1].ToString());
                        float y = float.Parse(row[2].ToString());
                        float z = float.Parse(row[3].ToString());

                        // �ж��Ƿ������ݣ���ĳһ��Ϊ����ֹͣ�������
                        if (x == 0 && y == 0 && z == 0)
                        {
                            break;
                        }

                        Vector3 vector = new Vector3(x, y, z);

                        // ��Vector3��ӵ�������
                        vectorList.Add(vector);

                    }

                }
              
                }

            
        }

        return vectorList.ToArray();
    }

    //������������
    public void ReadExcelDataName()
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
    /// ���ļ�
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
            Vector3[] vectorArray = ReadExcelDataVector3();
            ReadExcelDataName();
            // ���namesList�е�ÿ��Ԫ��
            foreach (string name in namesList)
            {
                Debug.Log("ExcelTest�ж�ȡ����");
                Debug.Log(name);
            }

            
            //// ���Vector3�����е�ÿ��Ԫ��
            foreach (Vector3 vector in vectorArray)
            {
                Debug.Log("ExcelTest�ж�ȡ��γ��");
                Debug.Log(vector);
            }
        }

    }

    /// <summary>
    /// ���ļ��� �İ�ť
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