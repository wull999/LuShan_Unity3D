using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManyouControl : MonoBehaviour
{
    public GameObject camControlObject;   //�����
    // Start is called before the first frame update
    void Start()
    {
        camControlObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///������ƹ���
    public void Btn_CamControl()
    {
        if (camControlObject.activeInHierarchy==false)
        {
            camControlObject.SetActive(true);  //��ʾ
        }
        else if  (camControlObject.activeInHierarchy == true)
        {
            camControlObject.SetActive(false);  //����
        }
    }

}
