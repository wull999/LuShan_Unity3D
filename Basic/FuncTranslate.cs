using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FuncTranslate : MonoBehaviour
{
    //public GameObject CheckControl;  //复选框控制
    public UnityEngine.UI.Toggle CheckControl;
    public GameObject pannerControl;  //获得pannel对象

    public Dropdown pointDrowdown;  //景点框
    // Start is called before the first frame update
    void Start()
    {
        //CheckControl.SetActive(true);    //最开始复选框\
        CheckControl.isOn = false; //复选框最开始未勾选
        pannerControl.SetActive(false);  //最开始复选框不勾选&面板不显示
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///相机控制功能
    public void Check_Change()
    {
        if (CheckControl.isOn)  //若复选框勾选
        {
            pannerControl.SetActive(true);  //隐藏pannel
        }
        else    //若复选框勾选
        {
            pannerControl.SetActive(false);  //显示pannel
            pointDrowdown.ClearOptions();
            
        }
    }
}
