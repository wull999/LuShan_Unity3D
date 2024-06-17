using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJInputText : MonoBehaviour
{
    public string pointName;  //物体名字

    GameObject childObject;  //子物体

    public static OBJInputText example;

    //public Transform Main_camera;  //主摄像机
    //public Transform Player_camera;  //玩家视角

    private void Awake()
    {
        example = this;

    }

    public void Update()
    {
        // 创建一条从摄像机发出的射线，方向是鼠标在屏幕上的位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 声明一个 RaycastHit 变量来存储射线碰撞的信息
        RaycastHit hit;

        // 检测射线是否与碰撞器相交
        if (Physics.Raycast(ray, out hit))
        {
            // 获取射线碰撞到的物体
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.transform.parent != null && hitObject.transform.parent.name == "newParent")
            {
                // 取得该子物体
                childObject = hitObject;
                pointName = childObject.name;
            }
        }

        // 打印子物体的名称
        //Debug.Log("鼠标当前触碰的子物体是：" + pointName);
    }
    private void OnMouseEnter()
    {
        UIController.instance_.uitextobj.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 25, 0);
        UIController.instance_.uitextobj.gameObject.SetActive(true);
        UIController.instance_.text.text = this.name;

    }
    private void OnMouseExit()
    {
        UIController.instance_.uitextobj.gameObject.SetActive(false);
    }
    private void OnMouseOver()
    {
        UIController.instance_.uitextobj.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 25, 0);
        UIController.instance_.uitextobj.gameObject.SetActive(true);
        UIController.instance_.text.text = this.name;
    }
}

