using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FuncTranslate : MonoBehaviour
{
    //public GameObject CheckControl;  //��ѡ�����
    public UnityEngine.UI.Toggle CheckControl;
    public GameObject pannerControl;  //���pannel����

    public Dropdown pointDrowdown;  //�����
    // Start is called before the first frame update
    void Start()
    {
        //CheckControl.SetActive(true);    //�ʼ��ѡ��\
        CheckControl.isOn = false; //��ѡ���ʼδ��ѡ
        pannerControl.SetActive(false);  //�ʼ��ѡ�򲻹�ѡ&��岻��ʾ
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///������ƹ���
    public void Check_Change()
    {
        if (CheckControl.isOn)  //����ѡ��ѡ
        {
            pannerControl.SetActive(true);  //����pannel
        }
        else    //����ѡ��ѡ
        {
            pannerControl.SetActive(false);  //��ʾpannel
            pointDrowdown.ClearOptions();
            
        }
    }
}
