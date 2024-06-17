using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShowControl : MonoBehaviour
{
    public GameObject functionShowLine;   //下拉框
    // Start is called before the first frame update
    void Start()
    {
        functionShowLine.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///相机控制功能
    public void Btn_FuncShowControlLine()
    {
        if (functionShowLine.activeInHierarchy == false)
        {
            functionShowLine.SetActive(true);  //显示
        }
        else if (functionShowLine.activeInHierarchy == true)
        {
            functionShowLine.SetActive(false);  //隐藏
        }

        //toggle1.gameObject.SetActive(!toggle1.gameObject.activeSelf);
        //toggle2.gameObject.SetActive(!toggle2.gameObject.activeSelf);
    }
}
