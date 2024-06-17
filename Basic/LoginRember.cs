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

    //鼠标点击，则跳转
    public  void sceneChange()
    {
            SceneManager.LoadScene("SampleScene");
       
    }

    //延时跳转
    public void OnLoginButtonClicked()
    {
        if (username == "wll999" && passwordrname == "628")
        {
        // 延迟2秒调用函数进行跳转
            Invoke("sceneChange", 2f);
        }


    }


    //不匹配情况
    public void CheckInformation()
    {
        if (username == "wll999" && passwordrname == "628")
        {
            waringText.text = "即将进行场景跳转！";

        }

       else if (username != "wll999" || passwordrname != "628")
        {
            waringText.text = "请输入正确的用户名或密码！";
            

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
