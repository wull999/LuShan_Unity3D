using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class morefunc : MonoBehaviour
{
    public Toggle controlHead;

    public GameObject panelControl;  //获得pannel对象

    public Button InformSearch, FileExport;  //获得按钮
    public Dropdown InformSearchdropdown, FileExportdropdown;  //获得下拉框

    // Start is called before the first frame update
    void Start()
    {  //刚开始时，均不可见
        controlHead.isOn = false; //复选框最开始未勾选

        panelControl.SetActive(false);

        InformSearchdropdown.gameObject.SetActive(false);
        FileExportdropdown.gameObject.SetActive(false);

    }

 
    public void check_change()
    {
        if (controlHead.isOn)  //若复选框勾选
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
