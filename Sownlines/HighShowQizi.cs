using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighShowQizi : MonoBehaviour
{
    private Color originalColor; // ���ڴ洢ԭʼ��ɫ
    private Color highlightColor = Color.yellow;// ������ʾ����ɫ
    private bool isMouseOver = false; // ����Ƿ���ͣ�ڶ����ϵı�־
    //private Material material;

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color; // ��ȡ�����ԭʼ��ɫ
    }

    void OnMouseEnter()
    {
        isMouseOver = true; // ���������ͣ��־Ϊtrue
        GetComponent<Renderer>().material.color = highlightColor; // ��������ɫ����Ϊ������ʾ����ɫ

        ////// ��ʾ����ı�Ե������
        //GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 300.2f); // ���������߿��
        //GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red); // ������������ɫ
    }

    void OnMouseExit()
    {
        isMouseOver = false; // ���������ͣ��־Ϊfalse
        GetComponent<Renderer>().material.color = originalColor; // ��������ɫ�ָ�Ϊԭʼ��ɫ

        //// ���ض���ı�Ե������
        //material.SetFloat("_OutlineWidth", 0); // �������߿������Ϊ0ʱ
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0)) // �������ͣ�ڶ����ϲ��Ұ���������ʱ
        {
            Debug.Log("Mouse clicked while hovering over the object!"); // �ڿ���̨�����Ϣ
        }
    }
}
