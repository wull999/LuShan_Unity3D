using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlAll : MonoBehaviour
{
    public Toggle controlHead;

    public GameObject panelControl;  //���pannel����

    public Button DayLight, DayNight;  //��ð�ť
    public Dropdown Lightchose, Nightchose;  //���������

    // Start is called before the first frame update
    void Start()
    {  //�տ�ʼʱ�������ɼ�

        controlHead.isOn = false; //��ѡ���ʼδ��ѡ

        panelControl.SetActive(false);

        //DayLight.gameObject.SetActive(false);
        //DayNight.gameObject.SetActive(false);
        Lightchose.gameObject.SetActive(false);
        Nightchose.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
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
            Lightchose.gameObject.SetActive(false);
            Nightchose.gameObject.SetActive(false);

        }
    }
}
