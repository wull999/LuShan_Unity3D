using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class visionControl : MonoBehaviour
{

    public GameObject panelControl;  //���pannel����

    // Start is called before the first frame update
    void Start()
    {
       
        panelControl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_click()
    {
        if (panelControl != null)
        {
            panelControl.SetActive(!panelControl.activeSelf); // �л� Panel ����ʾ״̬
        }
    }
}
