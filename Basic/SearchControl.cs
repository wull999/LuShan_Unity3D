using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchControl : MonoBehaviour
{
    public GameObject searchControlObject;   //����
    // Start is called before the first frame update
    void Start()
    {
        searchControlObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///������ƹ���
    public void Btn_SearchControl()
    {
        if (searchControlObject.activeInHierarchy == false)
        {
            searchControlObject.SetActive(true);  //��ʾ
        }
        else if (searchControlObject.activeInHierarchy == true)
        {
            searchControlObject.SetActive(false);  //����
        }
    }
}
