using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionChange : MonoBehaviour
{
    public GameObject ParentCamera;  //父物体
    public Camera MainCamera;  //子物体

    public Dropdown ShowLineDropDown; // 引用下拉框组件

  

    private void Start()
    {
       
    }
    public void SwitchToTopView()
    {
        // 父物体
        ParentCamera.transform.position = new Vector3(5, 7, 5);
        ParentCamera.transform.rotation = Quaternion.Euler(90, 0, 0);

        //子物体
        MainCamera.transform.localPosition = new Vector3(0, 0, 0);
        MainCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// 还原
    /// </summary>
    public void Original()
    {
        // 父物体
        ParentCamera.transform.position = new Vector3(5, 2, 4);
        ParentCamera.transform.rotation = Quaternion.Euler(9, 0, 0);

        //子物体
        MainCamera.transform.localPosition = new Vector3(0, 0, -10);
        MainCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// 放大
    /// </summary>
    public void AmplifyTo()
    {
        //ParentCamera.transform.rotation = original_rotation_parent;  //角度也还原

        ParentCamera.transform.position = new Vector3(5.5f, -3.5f, 8.5f);
        ParentCamera.transform.rotation = Quaternion.Euler(53.5f, 0, 0);

      
    }
}
