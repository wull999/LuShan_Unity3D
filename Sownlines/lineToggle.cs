using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lineToggle : MonoBehaviour
{
    public GameObject panelControl;  //���pannel����

    public Dropdown LineChose;  //���������

    public Toggle controlHead;

    // Start is called before the first frame update
    void Start()
    {
        controlHead.isOn = false; //��ѡ���ʼδ��ѡ

        panelControl.SetActive(false);
    }
    public void check_change()
    {
        if (controlHead.isOn)  //����ѡ��ѡ
        {
            panelControl.SetActive(true);

        }
        else
        {
            panelControl.SetActive(false);

            LineChose.gameObject.SetActive(false);


        }
    }
}
