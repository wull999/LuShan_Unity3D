using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class Photographer : MonoBehaviour
{
    public float Pitch { get; private set; }   //̧��   ����
    public float Yaw { get; private set; }  //ˮƽ   ����

    public float mouseSensitivity = 0.3f;   //���������
    public float cameraRotatingSpeed = 3;

    //������۳��仯
    [SerializeField]
    //private AnimationCurve _armLengthCurve;   //�۳�����
    private Transform _camera;

    private Vector3 lastMousePosition;  //��¼���λ�ã������ж�����Ƿ����ƶ�

    Transform myTransform;  //��ø����
    Vector3 rotation;  //��ø������rotation

    private void Awake()
    {
        _camera = transform.GetChild(0);  //��õ�һ��������
    }

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ����� Transform ���
         myTransform = gameObject.transform;

        // ��ȡ�������ת��Ϣ
         rotation = myTransform.rotation.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        //�Ƿ��¼�
        if (Input.GetMouseButtonDown(1)) // 1��ʾ����Ҽ�
        {
            lastMousePosition = Input.mousePosition;
        }

        if (!EventSystem.current.IsPointerOverGameObject())  //�ų�panel
            {

            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 movement = currentMousePosition - lastMousePosition;

            lastMousePosition = currentMousePosition;

                //�����Ҽ�������ƶ�
                if ( Input.GetMouseButton(1)&& movement.magnitude != 0)
                {
                //Debug.Log("�����Ҽ����ƶ���");

                UpdateRotation();  //�ӽǱ仯

                //UpdateArmLength();  //�۳��仯

                 }
            }

    }

    private void UpdateRotation()
    {
       Yaw += Input.GetAxis("Mouse X") * mouseSensitivity;  //���

        Pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;   //�ĳ�-= �� setting��MouseY�Ͳ��ù�ѡInvert

        Pitch = Mathf.Clamp(value: Pitch, min: -90, max: 90);  //���Ƹ����ǽǶ�

        //Debug.Log("Pitch:" + Pitch);
        //Debug.Log("Yaw:" + Yaw);

        //��ԭ���ǶȵĻ������ƶ�
        //transform.rotation = Quaternion.Euler(x:rotation.x+ Pitch, y: rotation.y+ Yaw, z: 0);
        transform.rotation = Quaternion.Euler(x: rotation.x + Pitch, y: rotation.y + Yaw, z: 0);

    }

   
    private void UpdateArmLength()
    {
        //_camera.localPosition = new Vector3(x: 0, y: 0, z: _armLengthCurve.Evaluate(Pitch) * -1);
    }
}
