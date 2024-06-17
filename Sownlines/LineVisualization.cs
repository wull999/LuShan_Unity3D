using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LineVisualization : MonoBehaviour
{

    public Dropdown ShowLineDropDown; // �������������
    public Text promptText; // ����������ʾ��ʾ���ݵ��ı����

    //���ڿ��������������
    public GameObject functionShowLine;   //������

    public DrawLines ShowLine;  //�������ú���

    public GameObject VisionControl;  //�����ӽǱ仯���


    // Start is called before the first frame update
    void Start()
    {
        // ��Ӽ��������������ֵ�����仯ʱ����DropdownValueChanged����
        //ShowLineDropDown.onValueChanged.AddListener(delegate {
        //    DropdownValueChanged(ShowLineDropDown);
        //});
        DropdownValueChanged(ShowLineDropDown);   //ִ�к���
        VisionControl.SetActive(false);  //�ʼ��չʾ�ӽ����

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropdownValueChanged(Dropdown dropdown)
    {
        // ����������ǰѡ�е�ֵ��������Ӧ����ʾ����
        //��Ӧÿ��case��������Ӧ·�ߵĿ��ӻ�
        switch (dropdown.value)
        {
            case 0:
                Debug.Log("�����ѡ��");
                break;

            case 1:
             
                //ShowLine.OnDisable( );  //���֮ǰ�ĵ�
                ShowLine.CleanLine();  //���֮ǰ����
                Debug.Log("��ѡ����·��һ");
                ShowLine.PointShowLine1(); //������ʾ·��1�ĺ���
                ShowLine.CreateLine();  //��ʼ����

                break;

            case 2:
                ShowLine.CleanLine();
                //ShowLine.OnDisable( );
               
                Debug.Log("��ѡ����·�߶�");
                ShowLine.PointShowLine2();
                ShowLine.CreateLine();
                break;

            case 3:
              
                //ShowLine.OnDisable();
                ShowLine.CleanLine();
                Debug.Log("��ѡ����·����");
                ShowLine.PointShowLine3();
                ShowLine.CreateLine();
                break;

            case 4:
               
                //ShowLine.OnDisable();
                ShowLine.CleanLine();
                Debug.Log("��ѡ����·����");
                ShowLine.PointShowLine4();
                ShowLine.CreateLine();
                break;


            case 5:
                //ShowLine.OnDisable();
                ShowLine.CleanLine();
             
                break;


            default:
                promptText.text = "";
                break;
        }
        functionShowLine.SetActive(false);
    }
}
