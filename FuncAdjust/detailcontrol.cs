using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detailcontrol : MonoBehaviour
{
    public Toggle controlHead;

    public Button environmentBtn, visionBtn;  //获得按钮

    public GameObject visionpanel;
    public GameObject environmentpanel;
    // Start is called before the first frame update
    void Start()
    {
        controlHead.isOn = false; //复选框最开始未勾选

        environmentBtn.gameObject.SetActive(false);
        visionBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlHead.isOn)  //若复选框勾选
        {
            environmentBtn.gameObject.SetActive(true);
            visionBtn.gameObject.SetActive(true);
          
        }
        else
        {
            environmentBtn.gameObject.SetActive(false);
            visionBtn.gameObject.SetActive(false);
            visionpanel.SetActive(false);
            environmentpanel.SetActive(false); 
        }
    }
}
