using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class addPoints : MonoBehaviour
{
    public Dropdown LineDropdown;   //获取路线的下拉框

    public Dropdown P1Dropdown, P2Dropdown;  //获取选择景点的下拉框

    public DrawLines getCalculatePoint;
    //public List<Vector3> calculatepoints ;
    public List<int> indexPoint;

    //public UnityEngine.Vector3 p1, p2;

    public Text resulttext;

    //当前路长
   public float currentDistance ;

    //总长
    public float totalRoalLength;

    public GameObject calculatepanel;

    public UnityEngine.Vector3[] subArray;

    int index1;
    int index2;

    public GameObject colorpanel;

    //public playerMove ConvertPoint;

    private void Start()
    {
        //calculatepoints = new List<Vector3>();
        colorpanel.SetActive(false);
    }

    /// <summary>
    /// 为景点框增加内容
    /// </summary>
    public void LineValueChange()
    {
        indexPoint.AddRange(getCalculatePoint.calculateIndex);
        Debug.Log("indexPoint长度：" + indexPoint.Count);
      

        foreach (int vector in indexPoint)
            {
                Debug.Log(vector);
            }


            P1Dropdown.ClearOptions();  //清除景点框之前的内容
        P2Dropdown.ClearOptions(); 

        // 创建一个新的下拉选项列表，并设置默认选项
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("请选择景点"));

        // 设置默认选中项
        P1Dropdown.value = 0;
        P2Dropdown.value = 0;

        //根据路线来添加景点框内容
        switch (LineDropdown.value)
        {
            case 1:
                // 添加新的下拉选项 
                options.Add(new Dropdown.OptionData("起点"));
                options.Add(new Dropdown.OptionData("飞来石"));
                options.Add(new Dropdown.OptionData("汉口峡"));
                options.Add(new Dropdown.OptionData("大月山"));
                options.Add(new Dropdown.OptionData("七里冲路"));
                options.Add(new Dropdown.OptionData("终点"));

                break;

            case 2:
                options.Add(new Dropdown.OptionData("起点"));
                options.Add(new Dropdown.OptionData("剪刀峡"));
                options.Add(new Dropdown.OptionData("碧龙潭"));
                options.Add(new Dropdown.OptionData("电站大坝"));
                options.Add(new Dropdown.OptionData("终点"));
                break;

            case 3:
                options.Add(new Dropdown.OptionData("起点"));
                options.Add(new Dropdown.OptionData("美庐"));
                options.Add(new Dropdown.OptionData("庐山石刻博物馆"));
                options.Add(new Dropdown.OptionData("庐山会议旧址"));
                options.Add(new Dropdown.OptionData("芦林湖"));
                options.Add(new Dropdown.OptionData("终点"));
                break;

            case 4:
                options.Add(new Dropdown.OptionData("起点"));
                options.Add(new Dropdown.OptionData("太乙峰"));
                options.Add(new Dropdown.OptionData("五老峰"));
                options.Add(new Dropdown.OptionData("三叠泉"));
                options.Add(new Dropdown.OptionData("终点"));
                break;


        }

        //将内容添加到景点框 
        P1Dropdown.AddOptions(options);
        P2Dropdown.AddOptions(options);

        // 更新 Dropdown 显示文本
        P1Dropdown.RefreshShownValue();
        P2Dropdown.RefreshShownValue();

    }

    //数组距离
    public void ArrayTotalLength()
    {
        int i1 = P1Dropdown.value - 1;
        int i2 = P2Dropdown.value - 1;

        index1 = indexPoint[i1];
        index2 = indexPoint[i2];
        //Debug.Log("i2:" + i2);
        Debug.Log("addPoints中index1:" + index1);
        Debug.Log("addPoints中index2:" + index2);
        Debug.Log("addPoints中getCalculatePoint.Scenecs.Length:" + getCalculatePoint.Scenecs.Length );

        //生成新数组
        int length = index2 - index1 + 1; // 新数组的长度
        subArray = new UnityEngine.Vector3[length]; // 创建新数组

        // 将 List 转换为数组
        Array.Copy(getCalculatePoint.vector3Array, index1, subArray, 0, length);  //新数组是subarray



        //计算总路长
        for (int i = 1; i < getCalculatePoint.vector3Array.Length; i++)
        {
            float distance = CalculateDistanceBetweenPoints(getCalculatePoint.vector3Array[i - 1], getCalculatePoint.vector3Array[i]);
            //Debug.Log("第" + i + "次的距离：" + distance);

            totalRoalLength += distance;
        }

        //输出新数组
        foreach (UnityEngine.Vector3 vector in subArray)
        {
            Debug.Log(vector);
        }

        for (int i = 1; i < subArray.Length; i++)
        {
            float distance = CalculateDistanceBetweenPoints(subArray[i - 1], subArray[i]);
            Debug.Log("第" + i + "次的距离：" + distance);

            currentDistance += distance;
        }
        string text = currentDistance.ToString();
        resulttext.text = text;

        //Debug.Log("Total distance: " + totalRoalLength);
    }

    public void drawNewLines()
    {
        colorpanel.SetActive(true);

        //把原来的线设置为不可见
        getCalculatePoint.lineRenderer.positionCount = 0;

        //画新线
        slopeAnalys.slope.CreateLineRenderer(0, index1, getCalculatePoint.Scenecs, Color.green);
        slopeAnalys.slope.CreateLineRenderer(index2, getCalculatePoint.Scenecs.Length - 1, getCalculatePoint.Scenecs, Color.green);
        slopeAnalys.slope.CreateLineRenderer(index1, index2, getCalculatePoint.Scenecs, Color.red);
    }

    //计算两点间距离
    float  CalculateDistanceBetweenPoints(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2)
    {
        Debug.Log("调用距离计算函数");
        const float Earth_r = 6378137;

        // 将经纬度转换为弧度
        float radLat1 = convert(p1.z);
        float radLon1 = convert(p1.x);
        float radLat2 = convert(p2.z);
        float radLon2 = convert(p2.x);

        // 计算球面三角形的各边长
       float result= (float)(Math.Acos(Math.Sin(radLat1) * Math.Sin(radLat2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Cos(radLon2 - radLon1)) * Earth_r);
        Debug.Log("result"+ result);
        return result;
    }

    public static float convert(float d)
    {
        return (float)d * Mathf.PI / 180f;
    }

    private void Update()
    {
        if(P1Dropdown.value!=0&&P2Dropdown.value!=0)
        {
            int i1 = P1Dropdown.value - 1;
            int i2 = P2Dropdown.value - 1;
            Debug.Log("i1:" + i1);
            Debug.Log("i2:" + i2);


            int index1 = indexPoint[i1];
            int index2 = indexPoint[i2];
            Debug.Log("index1:" + index1);
            Debug.Log("index2:" + index2);
        }
       
    }

    public void reCalculate()
    {
        //totalRoalLength = 0f;
        //currentDistance = 0f;
        //resulttext.text = "";
        //P1Dropdown.value = 0; // 将下拉框 P1Dropdown 的值重置为默认值，如果默认值不是第一个选项，可以根据实际情况设置为指定的值
        //P2Dropdown.value = 0; // 将下拉框 P2Dropdown 的值重置为默认值

    }

    public void btn_exit()
    {
        calculatepanel.SetActive(false);
    }

}
