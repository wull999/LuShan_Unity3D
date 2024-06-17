using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentControl : MonoBehaviour
{
    public GameObject panelControl;  //获得pannel对象

    // Start is called before the first frame update
    void Start()
    {
        panelControl.SetActive(false);
    }

    public void Btn_click()
    {
        if (panelControl != null)
        {
            panelControl.SetActive(!panelControl.activeSelf); // 切换 Panel 的显示状态
        }
    }
}
