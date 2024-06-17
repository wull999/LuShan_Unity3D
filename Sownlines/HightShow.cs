using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightShow : MonoBehaviour
{
    private Color originalColor; // 用于存储原始颜色
    private Color highlightColor = Color.yellow; // 高亮显示的颜色
    private bool isMouseOver = false; // 鼠标是否悬停在对象上的标志
    //private Material material;

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color; // 获取对象的原始颜色
    }

    void OnMouseEnter()
    {
        isMouseOver = true; // 设置鼠标悬停标志为true
        GetComponent<Renderer>().material.color = highlightColor; // 将对象颜色设置为高亮显示的颜色

        //// 显示对象的边缘轮廓线
        //material.SetFloat("_OutlineWidth", 100.1f); // 设置轮廓线宽度
        //material.SetColor("_OutlineColor", Color.red); // 设置轮廓线颜色
    }

    void OnMouseExit()
    {
        isMouseOver = false; // 设置鼠标悬停标志为false
        GetComponent<Renderer>().material.color = originalColor; // 将对象颜色恢复为原始颜色

        //// 隐藏对象的边缘轮廓线
        //material.SetFloat("_OutlineWidth", 0); // 将轮廓线宽度设置为0时
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0)) // 当鼠标悬停在对象上并且按下鼠标左键时
        {
            Debug.Log("Mouse clicked while hovering over the object!"); // 在控制台输出消息
        }
    }
}
