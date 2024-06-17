using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionChange : MonoBehaviour
{
    public GameObject ParentCamera;  //������
    public Camera MainCamera;  //������

    public Dropdown ShowLineDropDown; // �������������

  

    private void Start()
    {
       
    }
    public void SwitchToTopView()
    {
        // ������
        ParentCamera.transform.position = new Vector3(5, 7, 5);
        ParentCamera.transform.rotation = Quaternion.Euler(90, 0, 0);

        //������
        MainCamera.transform.localPosition = new Vector3(0, 0, 0);
        MainCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// ��ԭ
    /// </summary>
    public void Original()
    {
        // ������
        ParentCamera.transform.position = new Vector3(5, 2, 4);
        ParentCamera.transform.rotation = Quaternion.Euler(9, 0, 0);

        //������
        MainCamera.transform.localPosition = new Vector3(0, 0, -10);
        MainCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// �Ŵ�
    /// </summary>
    public void AmplifyTo()
    {
        //ParentCamera.transform.rotation = original_rotation_parent;  //�Ƕ�Ҳ��ԭ

        ParentCamera.transform.position = new Vector3(5.5f, -3.5f, 8.5f);
        ParentCamera.transform.rotation = Quaternion.Euler(53.5f, 0, 0);

      
    }
}
