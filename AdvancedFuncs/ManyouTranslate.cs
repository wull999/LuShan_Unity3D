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


    public float v = 0.3f;//km/h  1000/3600s=10/36  得到每秒的速度

    public List<Vector3> path = new List<Vector3>();   //存放所有点的position

    public DrawLines PointsInfor;

    private float totalLength;//路的总长度

    private float currentS;//当前已经走过的路程

    private int index = 0;

    public Dropdown LineDropdown;
    private int previousDropdownValue;

    public Transform Main_camera;  //主摄像机
    public Transform Player_camera;  //玩家视角

    public bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        FreeCheck.isOn = false; //复选框最开始未勾选
        LineCheck.isOn = false; //复选框最开始未勾选

        panel.SetActive(false);

      

        palayer.SetActive(false);  //最开始不显示

        //玩家摄像机
        Player_camera.gameObject.SetActive(false);

        // 记录下拉框初始值
        previousDropdownValue = LineDropdown.value;

        //// 添加值改变监听器
        //LineDropdown.onValueChanged.AddListener(OnDropdownValueChanged);


    }


    ///相机控制功能
    public void Btn_clicke()
    {

        //FreeCheck.gameObject.SetActive(true);
        //LineCheck.gameObject.SetActive(true);
        panel.SetActive(true);

        //FreeCheck.gameObject.SetActive(!FreeCheck.gameObject.activeSelf);
        //LineCheck.gameObject.SetActive(!LineCheck.gameObject.activeSelf);


    }


    /// <summary>
    /// 判断值是否发生变化，并重置数组
    /// </summary>
    /// <param name="value"></param>
    public void OnDropdownValueChanged(int value)
    {
        // 当下拉框的值改变时调用此方法

        Debug.Log("OnDropdownValueChanged中value: " + value);
        Debug.Log("OnDropdownValueChanged中previousValue: " + previousDropdownValue);

        //数组重置
        if (value != previousDropdownValue)
        {
            if (previousDropdownValue != 0)
            {
                // 下拉框值发生变化，重置path数组
                path.Clear();
                totalLength = 0;
                currentS = 0;
                index = 0;
                Debug.Log("重置数组");
                flag = true;
            }

            else
            {
                Debug.Log("下拉框的值未发生改变");
            }
        }

        // 检查当前值与上一次记录的值是否相同
        if (value != previousDropdownValue)
        {
            Debug.Log("Dropdown value has changed.");

            //if (value != 0 && flag == true)
            if (value != 0)
            {
                //数组传递
                for (int i = 0; i < PointsInfor.Scenecs.Length; i++)
                {
                    path.Add(PointsInfor.Scenecs[i]); // 将数组a的元素逐个添加到List<Vector3>数组b中
                    if (i == PointsInfor.Scenecs.Length - 1)
                    {
                        flag = false;
                    }
                }


                //移动
                palayer.SetActive(true);
                Player_camera.gameObject.SetActive(true);

                Player_camera.localPosition = new Vector3(-182,83,-623);
                Player_camera.localRotation = Quaternion.Euler(14.608f,14.792f,4.407f);

                GetComponent<PathMove>().SetPath(path, palayer.transform);

                ////计算路径
                for (int i = 1; i < path.Count; i++)
                {
                    totalLength += (path[i] - path[i - 1]).magnitude;
                }
            }

            // 更新记录的值为当前值
            previousDropdownValue = value;

        }

    }

    /// <summary>
    /// 进行路线漫游
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
