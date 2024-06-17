using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class arcSceneVision : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 2.0f;
    public Camera MainCamera;

    private bool isfly = false;  //判断是否要开始漫游

    private float defaultScale;  //原始放缩比例

    private Vector3 lastMousePosition; //鼠标当前帧位置

    public float zoomSpeed = 2.0f;  //缩放比例
    public float pushSpeed = 0.3f;   //相机推进速度

    public UnityEngine.UI.Toggle LookControl;  //获得漫游控制


    private void Start()
    {
        defaultScale = Camera.main.fieldOfView;  //存储初试比例
        lastMousePosition = Input.mousePosition;  //当前帧位置

        LookControl.isOn = false; //复选框最开始未勾选
    }

    public void Update()
    {
        if (LookControl.isOn)  //若复选框勾选
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))   // 在这里添加按下鼠标左键时的处理逻辑
                {
                    isfly = true;  //按下左键，开始漫游
                    Debug.Log("按下左键");
                }

                if (Input.GetMouseButtonDown(1))  // 在这里添加按下鼠标右键时的处理逻辑
                {
                    isfly = false;  //按下右键，停止漫游
                }

                if (isfly)
                {
                    Vector3 currentMousePosition = Input.mousePosition;

                    //鼠标移动距离
                    float mouseMovementMagnitude = Vector3.Distance(currentMousePosition, lastMousePosition);

                    if (currentMousePosition != lastMousePosition)  //若鼠标发生移动，则相机变化
                    {
                        Debug.Log("鼠标在移动");
                        // 控制相机的移动
                        float horizontal = Input.GetAxis("Horizontal");
                        float vertical = Input.GetAxis("Vertical");
                        MainCamera.transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime);

                        // 控制相机的旋转
                        float rotateX = Input.GetAxis("Mouse X");
                        float rotateY = Input.GetAxis("Mouse Y");
                        MainCamera.transform.Rotate(Vector3.up, rotateX * rotateSpeed);
                        MainCamera.transform.Rotate(Vector3.right, -rotateY * rotateSpeed);
                    }

                    else if (mouseMovementMagnitude < 0.1f)  //若鼠标不移动，则开始缩放
                    {
                        // 放大场景
                        MainCamera.transform.localScale += Vector3.one * zoomSpeed * Time.deltaTime;

                        // 推移场景
                        MainCamera.transform.position += transform.forward * pushSpeed * Time.deltaTime;
                    }

                    lastMousePosition = currentMousePosition;   //重新给当前帧位置赋值

                }

            }
        }
       





   
    }
}
    
    
