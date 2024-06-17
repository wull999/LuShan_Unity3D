using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPan : MonoBehaviour
{
    private Vector3 lastMousePosition;

    public float panSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                //Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.Auto); // 设置鼠标指针样式为小手
                Vector3 delta = Input.mousePosition - lastMousePosition;
                //Camera.main.transform.Translate(-delta * Time.deltaTime);
                transform.Translate(-delta.x * Time.deltaTime * panSpeed, -delta.y * Time.deltaTime * panSpeed, 0);
                lastMousePosition = Input.mousePosition;
            }
            else
            {
                //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // 恢复鼠标指针样式为原来的样式
            }
        }
           



    }
}
