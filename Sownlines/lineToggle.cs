using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lineToggle : MonoBehaviour
{
    public GameObject panelControl;  //获得pannel对象

    public Dropdown LineChose;  //获得下拉框

    public Toggle controlHead;

    // Start is called before the first frame update
    void Start()
    {
        controlHead.isOn = false; //复选框最开始未勾选

        panelControl.SetActive(false);
    }
    public void check_change()
    {
        if (controlHead.isOn)  //若复选框勾选
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
