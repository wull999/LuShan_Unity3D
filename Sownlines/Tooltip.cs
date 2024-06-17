using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text tooltipText; // ��ʾ��ʾ��Ϣ���ı����

    private void Start()
    {
        tooltipText.enabled = false;  //�տ�ʼ���ɼ�
        //ShowTip();  //����չʾtip����
    }

    /// <summary>
    /// û�õģ������
    /// </summary>
   public void ShowTip()
    {
        Debug.Log("�տ�ʼ����ShowTip��");
        // ��ȡ���λ��
        Vector2 mousePosition = Input.mousePosition;
        Vector2 localMousePosition;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), mousePosition, null, out localMousePosition);

        //��ֹ����
        if (transform.parent != null)
        {
            Debug.LogError("Parent object ��Ϊ null.");
            RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
            if (parentRectTransform != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, mousePosition, null, out localMousePosition);
                // �ж�����Ƿ���ͣ��������
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    GameObject hoveredObject = EventSystem.current.currentSelectedGameObject;
                    // ��������ͣ��������
                    if (hoveredObject != null && hoveredObject.CompareTag("Scene"))
                    {
                        // ��ʾ��������
                        tooltipText.text = "�õص�������ǣ�: " + hoveredObject.name;
                        tooltipText.enabled = true;
                        transform.position = transform.parent.TransformPoint(localMousePosition);
                    }
                    else
                    {
                        tooltipText.enabled = false;
                    }
                }
                else
                {
                    tooltipText.enabled = false;
                }
            }
            else
            {
                Debug.LogError("Parent object does not have a RectTransform component.");
            }
        }
        else
        {
            Debug.LogError("Parent object is null.");
        }

    }

    // ������嵽��ʾ���
    public void AddTooltip(GameObject sphere)
    {
        sphere.tag = "Scene";
    }

}
