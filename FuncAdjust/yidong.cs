using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class yidong : MonoBehaviour
{
    public Toggle controlHead;
    public GameObject yidongpanel;

    // Start is called before the first frame update
    void Start()
    {
        controlHead.isOn = false; //复选框最开始未勾选

        yidongpanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlHead.isOn)  //若复选框勾选
        {
            yidongpanel.gameObject.SetActive(true);


        }
        else
        {
            yidongpanel.gameObject.SetActive(false);

        }
    }
}
