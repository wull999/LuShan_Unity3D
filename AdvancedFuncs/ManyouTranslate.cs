using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class ManyouTranslate : MonoBehaviour
{
    public UnityEngine.UI.Toggle FreeCheck;
    public UnityEngine.UI.Toggle LineCheck;

    public GameObject panel;
    public GameObject palayer;


    public float v = 0.3f;//km/h  1000/3600s=10/36  �õ�ÿ����ٶ�

    public List<Vector3> path = new List<Vector3>();   //������е��position

    public DrawLines PointsInfor;

    private float totalLength;//·���ܳ���

    private float currentS;//��ǰ�Ѿ��߹���·��

    private int index = 0;

    public Dropdown LineDropdown;
    private int previousDropdownValue;

    public Transform Main_camera;  //�������
    public Transform Player_camera;  //����ӽ�

    public bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        FreeCheck.isOn = false; //��ѡ���ʼδ��ѡ
        LineCheck.isOn = false; //��ѡ���ʼδ��ѡ

        panel.SetActive(false);

      

        palayer.SetActive(false);  //�ʼ����ʾ

        //��������
        Player_camera.gameObject.SetActive(false);

        // ��¼�������ʼֵ
        previousDropdownValue = LineDropdown.value;

        //// ���ֵ�ı������
        //LineDropdown.onValueChanged.AddListener(OnDropdownValueChanged);


    }


    ///������ƹ���
    public void Btn_clicke()
    {

        //FreeCheck.gameObject.SetActive(true);
        //LineCheck.gameObject.SetActive(true);
        panel.SetActive(true);

        //FreeCheck.gameObject.SetActive(!FreeCheck.gameObject.activeSelf);
        //LineCheck.gameObject.SetActive(!LineCheck.gameObject.activeSelf);


    }


    /// <summary>
    /// �ж�ֵ�Ƿ����仯������������
    /// </summary>
    /// <param name="value"></param>
    public void OnDropdownValueChanged(int value)
    {
        // ���������ֵ�ı�ʱ���ô˷���

        Debug.Log("OnDropdownValueChanged��value: " + value);
        Debug.Log("OnDropdownValueChanged��previousValue: " + previousDropdownValue);

        //��������
        if (value != previousDropdownValue)
        {
            if (previousDropdownValue != 0)
            {
                // ������ֵ�����仯������path����
                path.Clear();
                totalLength = 0;
                currentS = 0;
                index = 0;
                Debug.Log("��������");
                flag = true;
            }

            else
            {
                Debug.Log("�������ֵδ�����ı�");
            }
        }

        // ��鵱ǰֵ����һ�μ�¼��ֵ�Ƿ���ͬ
        if (value != previousDropdownValue)
        {
            Debug.Log("Dropdown value has changed.");

            //if (value != 0 && flag == true)
            if (value != 0)
            {
                //���鴫��
                for (int i = 0; i < PointsInfor.Scenecs.Length; i++)
                {
                    path.Add(PointsInfor.Scenecs[i]); // ������a��Ԫ�������ӵ�List<Vector3>����b��
                    if (i == PointsInfor.Scenecs.Length - 1)
                    {
                        flag = false;
                    }
                }


                //�ƶ�
                palayer.SetActive(true);
                Player_camera.gameObject.SetActive(true);

                Player_camera.localPosition = new Vector3(-182,83,-623);
                Player_camera.localRotation = Quaternion.Euler(14.608f,14.792f,4.407f);

                GetComponent<PathMove>().SetPath(path, palayer.transform);

                ////����·��
                for (int i = 1; i < path.Count; i++)
                {
                    totalLength += (path[i] - path[i - 1]).magnitude;
                }
            }

            // ���¼�¼��ֵΪ��ǰֵ
            previousDropdownValue = value;

        }

    }

    /// <summary>
    /// ����·������
    /// </summary>
    private void Update()
    {
        if(LineCheck.isOn)
        {
            LineDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        else if(LineCheck.isOn==false)
        {
            LineDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
            palayer.SetActive(false);
        }
    }
}
