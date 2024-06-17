using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Openurl : MonoBehaviour
{
    public Dropdown dropdownLatLonDem;

    // Start is called before the first frame update
    public void LatLonDemValueChanged(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                Debug.Log("请进行选择");
                break;

            case 1:
                Application.OpenURL("https://www.toolnb.com/tools/gps.html?ivk_sa=1024320u");  //打开经纬度查询网址
                break;

            case 2:
                Application.OpenURL("https://www.advancedconverter.com/map-tools/find-altitude-by-coordinates");  //打开海拔查询网址
                break;

        }
    }
}
