using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShowControl : MonoBehaviour
{
    public GameObject functionShowLine;   //������
    // Start is called before the first frame update
    void Start()
    {
        functionShowLine.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///������ƹ���
    public void Btn_FuncShowControlLine()
    {
        if (functionShowLine.activeInHierarchy == false)
        {
            functionShowLine.SetActive(true);  //��ʾ
        }
        else if (functionShowLine.activeInHierarchy == true)
        {
            functionShowLine.SetActive(false);  //����
        }

        //toggle1.gameObject.SetActive(!toggle1.gameObject.activeSelf);
        //toggle2.gameObject.SetActive(!toggle2.gameObject.activeSelf);
    }
}
