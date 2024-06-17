using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class morefunc : MonoBehaviour
{
    public Toggle controlHead;

    public GameObject panelControl;  //���pannel����

    public Button InformSearch, FileExport;  //��ð�ť
    public Dropdown InformSearchdropdown, FileExportdropdown;  //���������

    // Start is called before the first frame update
    void Start()
    {  //�տ�ʼʱ�������ɼ�
        controlHead.isOn = false; //��ѡ���ʼδ��ѡ

        panelControl.SetActive(false);

        InformSearchdropdown.gameObject.SetActive(false);
        FileExportdropdown.gameObject.SetActive(false);

    }

 
    public void check_change()
    {
        if (controlHead.isOn)  //����ѡ��ѡ
        {
            panelControl.SetActive(true);
            //DayLight.gameObject.SetActive(true);
            //DayNight.gameObject.SetActive(true);
        }
        else
        {
            panelControl.SetActive(false);
            //DayLight.gameObject.SetActive(false);
            //DayNight.gameObject.SetActive(false);
            InformSearchdropdown.gameObject.SetActive(false);
            FileExportdropdown.gameObject.SetActive(false);

        }
    }
}
