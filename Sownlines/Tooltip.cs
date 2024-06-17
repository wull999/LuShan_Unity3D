using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text tooltipText; // 显示提示信息的文本组件

    private void Start()
    {
        tooltipText.enabled = false;  //刚开始不可见
        //ShowTip();  //调用展示tip功能
    }

    /// <summary>
    /// 没用的，别看这个
    /// </summary>
   public void ShowTip()
    {
        Debug.Log("刚开始调用ShowTip！");
        // 获取鼠标位置
        Vector2 mousePosition = Input.mousePosition;
        Vector2 localMousePosition;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), mousePosition, null, out localMousePosition);

        //防止报空
        if (transform.parent != null)
        {
            Debug.LogError("Parent object 不为 null.");
            RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
            if (parentRectTransform != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, mousePosition, null, out localMousePosition);
                // 判断鼠标是否悬停在物体上
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    GameObject hoveredObject = EventSystem.current.currentSelectedGameObject;
                    // 如果鼠标悬停在球体上
                    if (hoveredObject != null && hoveredObject.CompareTag("Scene"))
                    {
                        // 显示球体名称
                        tooltipText.text = "该地点的名字是：: " + hoveredObject.name;
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

    // 添加球体到提示组件
    public void AddTooltip(GameObject sphere)
    {
        sphere.tag = "Scene";
    }

}
