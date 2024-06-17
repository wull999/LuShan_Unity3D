using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionExit : MonoBehaviour
{
    public GameObject VisionControl;  //控制视角变化面板
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
