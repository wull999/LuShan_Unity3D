using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LineVisualization : MonoBehaviour
{

    public Dropdown ShowLineDropDown; // 引用下拉框组件
    public Text promptText; // 引用用于显示提示内容的文本组件

    //用于控制下拉框的隐藏
    public GameObject functionShowLine;   //下拉框

    public DrawLines ShowLine;  //用于引用函数

    public GameObject VisionControl;  //控制视角变化面板


    // Start is called before the first frame update
    void Start()
    {
        // 添加监听，当下拉框的值发生变化时调用DropdownValueChanged方法
        //ShowLineDropDown.onValueChanged.AddListener(delegate {
        //    DropdownValueChanged(ShowLineDropDown);
        //});
        DropdownValueChanged(ShowLineDropDown);   //执行函数
        VisionControl.SetActive(false);  //最开始不展示视角面板

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropdownValueChanged(Dropdown dropdown)
    {
        // 根据下拉框当前选中的值，设置相应的提示内容
        //对应每个case，进行相应路线的可视化
        switch (dropdown.value)
        {
            case 0:
                Debug.Log("请进行选择");
                break;

            case 1:
             
                //ShowLine.OnDisable( );  //清空之前的点
                ShowLine.CleanLine();  //清除之前的线
                Debug.Log("您选择了路线一");
                ShowLine.PointShowLine1(); //调用显示路线1的函数
                ShowLine.CreateLine();  //开始画线

                break;

            case 2:
                ShowLine.CleanLine();
                //ShowLine.OnDisable( );
               
                Debug.Log("您选择了路线二");
                ShowLine.PointShowLine2();
                ShowLine.CreateLine();
                break;

            case 3:
              
                //ShowLine.OnDisable();
                ShowLine.CleanLine();
                Debug.Log("您选择了路线三");
                ShowLine.PointShowLine3();
                ShowLine.CreateLine();
                break;

            case 4:
               
                //ShowLine.OnDisable();
                ShowLine.CleanLine();
                Debug.Log("您选择了路线四");
                ShowLine.PointShowLine4();
                ShowLine.CreateLine();
                break;


            case 5:
                //ShowLine.OnDisable();
                ShowLine.CleanLine();
             
                break;


            default:
                promptText.text = "";
                break;
        }
        functionShowLine.SetActive(false);
    }
}
