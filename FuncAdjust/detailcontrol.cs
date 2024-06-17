using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detailcontrol : MonoBehaviour
{
    public Toggle controlHead;

    public Button environmentBtn, visionBtn;  //��ð�ť

    public GameObject visionpanel;
    public GameObject environmentpanel;
    // Start is called before the first frame update
    void Start()
    {
        controlHead.isOn = false; //��ѡ���ʼδ��ѡ

        environmentBtn.gameObject.SetActive(false);
        visionBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlHead.isOn)  //����ѡ��ѡ
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
