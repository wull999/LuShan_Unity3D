using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginRember : MonoBehaviour
{
    public InputField UserInput;
    public InputField PasswordInput;

    public GameObject waringPanel;
    public Text waringText;

    string username;
    string passwordrname;


    private void Start()
    {
        waringPanel.SetActive(false);
    }

    private void Update()
    {
        username = UserInput.text;
        passwordrname = PasswordInput.text;

    }

    //�����������ת
    public  void sceneChange()
    {
            SceneManager.LoadScene("SampleScene");
       
    }

    //��ʱ��ת
    public void OnLoginButtonClicked()
    {
        if (username == "wll999" && passwordrname == "628")
        {
        // �ӳ�2����ú���������ת
            Invoke("sceneChange", 2f);
        }


    }


    //��ƥ�����
    public void CheckInformation()
    {
        if (username == "wll999" && passwordrname == "628")
        {
            waringText.text = "�������г�����ת��";

        }

       else if (username != "wll999" || passwordrname != "628")
        {
            waringText.text = "��������ȷ���û��������룡";
            

        }

        waringPanel.SetActive(true);
    }

    public void warringExit()
    {
        waringPanel.SetActive(false);
        UserInput.text = "";
        PasswordInput.text = "";
    }
   
}
