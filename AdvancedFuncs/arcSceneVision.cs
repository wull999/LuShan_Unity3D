using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class arcSceneVision : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 2.0f;
    public Camera MainCamera;

    private bool isfly = false;  //�ж��Ƿ�Ҫ��ʼ����

    private float defaultScale;  //ԭʼ��������

    private Vector3 lastMousePosition; //��굱ǰ֡λ��

    public float zoomSpeed = 2.0f;  //���ű���
    public float pushSpeed = 0.3f;   //����ƽ��ٶ�

    public UnityEngine.UI.Toggle LookControl;  //������ο���


    private void Start()
    {
        defaultScale = Camera.main.fieldOfView;  //�洢���Ա���
        lastMousePosition = Input.mousePosition;  //��ǰ֡λ��

        LookControl.isOn = false; //��ѡ���ʼδ��ѡ
    }

    public void Update()
    {
        if (LookControl.isOn)  //����ѡ��ѡ
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))   // ��������Ӱ���������ʱ�Ĵ����߼�
                {
                    isfly = true;  //�����������ʼ����
                    Debug.Log("�������");
                }

                if (Input.GetMouseButtonDown(1))  // ��������Ӱ�������Ҽ�ʱ�Ĵ����߼�
                {
                    isfly = false;  //�����Ҽ���ֹͣ����
                }

                if (isfly)
                {
                    Vector3 currentMousePosition = Input.mousePosition;

                    //����ƶ�����
                    float mouseMovementMagnitude = Vector3.Distance(currentMousePosition, lastMousePosition);

                    if (currentMousePosition != lastMousePosition)  //����귢���ƶ���������仯
                    {
                        Debug.Log("������ƶ�");
                        // ����������ƶ�
                        float horizontal = Input.GetAxis("Horizontal");
                        float vertical = Input.GetAxis("Vertical");
                        MainCamera.transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime);

                        // �����������ת
                        float rotateX = Input.GetAxis("Mouse X");
                        float rotateY = Input.GetAxis("Mouse Y");
                        MainCamera.transform.Rotate(Vector3.up, rotateX * rotateSpeed);
                        MainCamera.transform.Rotate(Vector3.right, -rotateY * rotateSpeed);
                    }

                    else if (mouseMovementMagnitude < 0.1f)  //����겻�ƶ�����ʼ����
                    {
                        // �Ŵ󳡾�
                        MainCamera.transform.localScale += Vector3.one * zoomSpeed * Time.deltaTime;

                        // ���Ƴ���
                        MainCamera.transform.position += transform.forward * pushSpeed * Time.deltaTime;
                    }

                    lastMousePosition = currentMousePosition;   //���¸���ǰ֡λ�ø�ֵ

                }

            }
        }
       





   
    }
}
    
    
