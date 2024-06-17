using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManyouControl : MonoBehaviour
{
    public GameObject camControlObject;   //相机组
    // Start is called before the first frame update
    void Start()
    {
        camControlObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///相机控制功能
    public void Btn_CamControl()
    {
        if (camControlObject.activeInHierarchy==false)
        {
            camControlObject.SetActive(true);  //显示
        }
        else if  (camControlObject.activeInHierarchy == true)
        {
            camControlObject.SetActive(false);  //隐藏
        }
    }

}
