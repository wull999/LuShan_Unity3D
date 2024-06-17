using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncShowControl : MonoBehaviour
{
    public GameObject functionShowObject;   //搜索
    // Start is called before the first frame update
    void Start()
    {
        functionShowObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///相机控制功能
    public void Btn_FuncShowControl()
    {
        if (functionShowObject.activeInHierarchy == false)
        {
            functionShowObject.SetActive(true);  //显示
        }
        else if (functionShowObject.activeInHierarchy == true)
        {
            functionShowObject.SetActive(false);  //隐藏
        }
    }
}
