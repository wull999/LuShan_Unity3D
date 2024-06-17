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
    public Dropdown LineDropdown;   //��ȡ·�ߵ�������

    public Dropdown P1Dropdown, P2Dropdown;  //��ȡѡ�񾰵��������

    public DrawLines getCalculatePoint;
    //public List<Vector3> calculatepoints ;
    public List<int> indexPoint;

    //public UnityEngine.Vector3 p1, p2;

    public Text resulttext;

    //��ǰ·��
   public float currentDistance ;

    //�ܳ�
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
    /// Ϊ�������������
    /// </summary>
    public void LineValueChange()
    {
        indexPoint.AddRange(getCalculatePoint.calculateIndex);
        Debug.Log("indexPoint���ȣ�" + indexPoint.Count);
      

        foreach (int vector in indexPoint)
            {
                Debug.Log(vector);
            }


            P1Dropdown.ClearOptions();  //��������֮ǰ������
        P2Dropdown.ClearOptions(); 

        // ����һ���µ�����ѡ���б�������Ĭ��ѡ��
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("��ѡ�񾰵�"));

        // ����Ĭ��ѡ����
        P1Dropdown.value = 0;
        P2Dropdown.value = 0;

        //����·������Ӿ��������
        switch (LineDropdown.value)
        {
            case 1:
                // ����µ�����ѡ�� 
                options.Add(new Dropdown.OptionData("���"));
                options.Add(new Dropdown.OptionData("����ʯ"));
                options.Add(new Dropdown.OptionData("����Ͽ"));
                options.Add(new Dropdown.OptionData("����ɽ"));
                options.Add(new Dropdown.OptionData("�����·"));
                options.Add(new Dropdown.OptionData("�յ�"));

                break;

            case 2:
                options.Add(new Dropdown.OptionData("���"));
                options.Add(new Dropdown.OptionData("����Ͽ"));
                options.Add(new Dropdown.OptionData("����̶"));
                options.Add(new Dropdown.OptionData("��վ���"));
                options.Add(new Dropdown.OptionData("�յ�"));
                break;

            case 3:
                options.Add(new Dropdown.OptionData("���"));
                options.Add(new Dropdown.OptionData("��®"));
                options.Add(new Dropdown.OptionData("®ɽʯ�̲����"));
                options.Add(new Dropdown.OptionData("®ɽ�����ַ"));
                options.Add(new Dropdown.OptionData("«�ֺ�"));
                options.Add(new Dropdown.OptionData("�յ�"));
                break;

            case 4:
                options.Add(new Dropdown.OptionData("���"));
                options.Add(new Dropdown.OptionData("̫�ҷ�"));
                options.Add(new Dropdown.OptionData("���Ϸ�"));
                options.Add(new Dropdown.OptionData("����Ȫ"));
                options.Add(new Dropdown.OptionData("�յ�"));
                break;


        }

        //��������ӵ������ 
        P1Dropdown.AddOptions(options);
        P2Dropdown.AddOptions(options);

        // ���� Dropdown ��ʾ�ı�
        P1Dropdown.RefreshShownValue();
        P2Dropdown.RefreshShownValue();

    }

    //�������
    public void ArrayTotalLength()
    {
        int i1 = P1Dropdown.value - 1;
        int i2 = P2Dropdown.value - 1;

        index1 = indexPoint[i1];
        index2 = indexPoint[i2];
        //Debug.Log("i2:" + i2);
        Debug.Log("addPoints��index1:" + index1);
        Debug.Log("addPoints��index2:" + index2);
        Debug.Log("addPoints��getCalculatePoint.Scenecs.Length:" + getCalculatePoint.Scenecs.Length );

        //����������
        int length = index2 - index1 + 1; // ������ĳ���
        subArray = new UnityEngine.Vector3[length]; // ����������

        // �� List ת��Ϊ����
        Array.Copy(getCalculatePoint.vector3Array, index1, subArray, 0, length);  //��������subarray



        //������·��
        for (int i = 1; i < getCalculatePoint.vector3Array.Length; i++)
        {
            float distance = CalculateDistanceBetweenPoints(getCalculatePoint.vector3Array[i - 1], getCalculatePoint.vector3Array[i]);
            //Debug.Log("��" + i + "�εľ��룺" + distance);

            totalRoalLength += distance;
        }

        //���������
        foreach (UnityEngine.Vector3 vector in subArray)
        {
            Debug.Log(vector);
        }

        for (int i = 1; i < subArray.Length; i++)
        {
            float distance = CalculateDistanceBetweenPoints(subArray[i - 1], subArray[i]);
            Debug.Log("��" + i + "�εľ��룺" + distance);

            currentDistance += distance;
        }
        string text = currentDistance.ToString();
        resulttext.text = text;

        //Debug.Log("Total distance: " + totalRoalLength);
    }

    public void drawNewLines()
    {
        colorpanel.SetActive(true);

        //��ԭ����������Ϊ���ɼ�
        getCalculatePoint.lineRenderer.positionCount = 0;

        //������
        slopeAnalys.slope.CreateLineRenderer(0, index1, getCalculatePoint.Scenecs, Color.green);
        slopeAnalys.slope.CreateLineRenderer(index2, getCalculatePoint.Scenecs.Length - 1, getCalculatePoint.Scenecs, Color.green);
        slopeAnalys.slope.CreateLineRenderer(index1, index2, getCalculatePoint.Scenecs, Color.red);
    }

    //������������
    float  CalculateDistanceBetweenPoints(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2)
    {
        Debug.Log("���þ�����㺯��");
        const float Earth_r = 6378137;

        // ����γ��ת��Ϊ����
        float radLat1 = convert(p1.z);
        float radLon1 = convert(p1.x);
        float radLat2 = convert(p2.z);
        float radLon2 = convert(p2.x);

        // �������������εĸ��߳�
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
        //P1Dropdown.value = 0; // �������� P1Dropdown ��ֵ����ΪĬ��ֵ�����Ĭ��ֵ���ǵ�һ��ѡ����Ը���ʵ���������Ϊָ����ֵ
        //P2Dropdown.value = 0; // �������� P2Dropdown ��ֵ����ΪĬ��ֵ

    }

    public void btn_exit()
    {
        calculatepanel.SetActive(false);
    }

}
