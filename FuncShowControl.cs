using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncShowControl : MonoBehaviour
{
    public GameObject functionShowObject;   //����
    // Start is called before the first frame update
    void Start()
    {
        functionShowObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///������ƹ���
    public void Btn_FuncShowControl()
    {
        if (functionShowObject.activeInHierarchy == false)
        {
            functionShowObject.SetActive(true);  //��ʾ
        }
        else if (functionShowObject.activeInHierarchy == true)
        {
            functionShowObject.SetActive(false);  //����
        }
    }
}
