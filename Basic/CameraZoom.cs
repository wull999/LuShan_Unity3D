using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 10.0f;
    public float minZoom = 0.01f;
    public float maxZoom = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(scroll!=0)
            {
                float zoom = scroll * zoomSpeed;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - zoom, minZoom, maxZoom);
            }

        }


    }

    }
