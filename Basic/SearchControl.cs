using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchControl : MonoBehaviour
{
    public GameObject searchControlObject;   //搜索
    // Start is called before the first frame update
    void Start()
    {
        searchControlObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///相机控制功能
    public void Btn_SearchControl()
    {
        if (searchControlObject.activeInHierarchy == false)
        {
            searchControlObject.SetActive(true);  //显示
        }
        else if (searchControlObject.activeInHierarchy == true)
        {
            searchControlObject.SetActive(false);  //隐藏
        }
    }
}
