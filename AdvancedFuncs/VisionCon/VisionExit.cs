using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionExit : MonoBehaviour
{
    public GameObject VisionControl;  //�����ӽǱ仯���
    // Start is called before the first frame update
    void Start()
    {
        ClickBtn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickBtn()
    {
        VisionControl.SetActive(false);
    }
}
