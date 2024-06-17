using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJInputText : MonoBehaviour
{
    public string pointName;  //��������

    GameObject childObject;  //������

    public static OBJInputText example;

    //public Transform Main_camera;  //�������
    //public Transform Player_camera;  //����ӽ�

    private void Awake()
    {
        example = this;

    }

    public void Update()
    {
        // ����һ������������������ߣ��������������Ļ�ϵ�λ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ����һ�� RaycastHit �������洢������ײ����Ϣ
        RaycastHit hit;

        // ��������Ƿ�����ײ���ཻ
        if (Physics.Raycast(ray, out hit))
        {
            // ��ȡ������ײ��������
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.transform.parent != null && hitObject.transform.parent.name == "newParent")
            {
                // ȡ�ø�������
                childObject = hitObject;
                pointName = childObject.name;
            }
        }

        // ��ӡ�����������
        //Debug.Log("��굱ǰ�������������ǣ�" + pointName);
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

