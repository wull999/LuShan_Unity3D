using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class Photographer : MonoBehaviour
{
    public float Pitch { get; private set; }   //抬升   俯仰
    public float Yaw { get; private set; }  //水平   左右

    public float mouseSensitivity = 0.3f;   //鼠标灵敏度
    public float cameraRotatingSpeed = 3;

    //摄像机臂长变化
    [SerializeField]
    //private AnimationCurve _armLengthCurve;   //臂长曲线
    private Transform _camera;

    private Vector3 lastMousePosition;  //记录鼠标位置，用于判断鼠标是否发生移动

    Transform myTransform;  //获得该组件
    Vector3 rotation;  //获得父物体的rotation

    private void Awake()
    {
        _camera = transform.GetChild(0);  //获得第一个子物体
    }

    // Start is called before the first frame update
    void Start()
    {
        // 获取物体的 Transform 组件
         myTransform = gameObject.transform;

        // 获取物体的旋转信息
         rotation = myTransform.rotation.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        //是否按下键
        if (Input.GetMouseButtonDown(1)) // 1表示鼠标右键
        {
            lastMousePosition = Input.mousePosition;
        }

        if (!EventSystem.current.IsPointerOverGameObject())  //排除panel
            {

            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 movement = currentMousePosition - lastMousePosition;

            lastMousePosition = currentMousePosition;

                //按下右键且鼠标移动
                if ( Input.GetMouseButton(1)&& movement.magnitude != 0)
                {
                //Debug.Log("按下右键且移动了");

                UpdateRotation();  //视角变化

                //UpdateArmLength();  //臂长变化

                 }
            }

    }

    private void UpdateRotation()
    {
       Yaw += Input.GetAxis("Mouse X") * mouseSensitivity;  //鼠标

        Pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;   //改成-= 后 setting中MouseY就不用勾选Invert

        Pitch = Mathf.Clamp(value: Pitch, min: -90, max: 90);  //限制俯仰角角度

        //Debug.Log("Pitch:" + Pitch);
        //Debug.Log("Yaw:" + Yaw);

        //在原来角度的基础上移动
        //transform.rotation = Quaternion.Euler(x:rotation.x+ Pitch, y: rotation.y+ Yaw, z: 0);
        transform.rotation = Quaternion.Euler(x: rotation.x + Pitch, y: rotation.y + Yaw, z: 0);

    }

   
    private void UpdateArmLength()
    {
        //_camera.localPosition = new Vector3(x: 0, y: 0, z: _armLengthCurve.Evaluate(Pitch) * -1);
    }
}
