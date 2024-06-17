using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class calculateCon : MonoBehaviour
{
    public GameObject panel;
    public Button btn_calculate;

    private bool isPanelActive = false;

    public GameObject colorpanel;


    void Start()
    {
        panel.SetActive(false);
        colorpanel.SetActive(false);

    }

   public  void btn_click()
    {
        isPanelActive = !isPanelActive;
        panel.SetActive(isPanelActive);
    }
}
