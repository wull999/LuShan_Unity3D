using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;

public class chartControl : MonoBehaviour
{
    public GameObject lineChartObj; // ÕÛÏßÍ¼µÄ GameObject


    // Start is called before the first frame update
    void Start()
    {
        lineChartObj.SetActive(false);
    }

    // Update is called once per frame
    public void btn_chartClick()
    {
        if (lineChartObj.activeSelf)
        {
            lineChartObj.SetActive(false);
        }
        else
        {
            lineChartObj.SetActive(true);
        }
    }
}
