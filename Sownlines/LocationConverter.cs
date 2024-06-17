using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LocationConverter : MonoBehaviour
{
    public Terrain terrain; // ����

    Vector2 latLngTopLeft = new Vector2(115.83727f, 29.69411f); // ���Ͻǿ��Ƶ�ľ�γ��
    Vector2 latLngBottomRight = new Vector2(116.13963f, 29.39175f);// ���½ǿ��Ƶ�ľ�γ��

    public Transform p1, p2;  //�������Ƶ�

    //����
    Vector3 testLatLng = new Vector3(115.94649f, 573.5f, 29.55544f);


    //public string modelName = "������"; // ģ���ļ���
    private string modelName0 = "����3"; // ģ���ļ���   ֻ����һ�Σ��״�������

    public Vector3 position;
    public Vector3 scale = new Vector3(10, 10, 10); // ģ������

    private float demUnityHeight;   //���θ߶�
    private float lsRealHeight = 1469.255f;


    GameObject modelInstance;
    // Start is called before the first frame update
    void Start()
    {
        //���Ƶ� ���辭γ��
        Vector3 topLeft = new Vector3(latLngTopLeft.x, 0, latLngTopLeft.y);
        Vector3 bottomRight = new Vector3(latLngBottomRight.x, 0, latLngBottomRight.y);

        // ��ȡ���εĴ�С
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        //terrain_width = terrainWidth;
        demUnityHeight = terrain.terrainData.size.y;

        // ���Ե��ڵ����е�λ��
        Vector3 testPos = ConvertLatLngToTerrain(new Vector3(testLatLng.x, testLatLng.y, testLatLng.z), topLeft, bottomRight, terrainWidth, terrainLength);

        //// ���ز�ʵ����ģ��
        GameObject model = Resources.Load<GameObject>(modelName0);   //+40
        position = new Vector3(testPos.x, testPos.y, testPos.z);

        //GameObject modelInstance = Instantiate(model);
         modelInstance = Instantiate(model, position, Quaternion.identity);
        modelInstance.name = modelName0; // ����ʵ�������������
        modelInstance.transform.localScale = scale;
        //modelInstance.name = "���Ե�";

        OnDisable();   //���ٵ�


        ////��Unity����ʾ���Ե�
        //GameObject testPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //testPoint.name = "���Ե�";
        //testPoint.transform.localScale = new Vector3(6, 6, 6);
        //testPoint.transform.position = new Vector3(testPos.x, testPos.y, testPos.z);


    }
    Vector3 ConvertLatLngToTerrain(Vector3 p, Vector3 topLeft, Vector3 bottomRight, float terrainWidth, float terrainLength)
    {
        //���㾭��
        float point_topleftLong = Mathf.Abs(p.x - topLeft.x);
        float topleft_botrightLong = Mathf.Abs(bottomRight.x - topLeft.x);
        float x = point_topleftLong / topleft_botrightLong * terrainWidth;
       // Debug.Log("x:"+x);

        //����γ��
        float point_botrightLat = Mathf.Abs(p.z - bottomRight.z);
        float topleft_botrightLat = Mathf.Abs(topLeft.z - bottomRight.z);
        float z = (point_botrightLat / topleft_botrightLat) * 1.0f * terrainLength;
        Debug.Log("point_botrightLat1111:" + point_botrightLat);
        Debug.Log("topleft_botrightLat1111:" + topleft_botrightLat);
        Debug.Log("terrainLength1111:" + terrainLength);
        Debug.Log("z1111:" + z);

        //����߶�
        float real_height = GetRealHeight(p.y);

        return new Vector3(x, real_height, z);
    }

    float GetRealHeight(float dem)
    {
      
        float real_H = dem * demUnityHeight / lsRealHeight;
        //Debug.Log("real_H:" + real_H);
        return real_H + 5 + 10 + 40;
    }

    // Update is called once per frame

    public void OnDisable()
    {

        DestroyImmediate(modelInstance);

        //Destroy(gameObject, 5.0f); // 5�������

        Debug.Log("��������Ŷ");

        //point.SetActive(false);  //������
        //Debug.Log("����Ϊ��������");

        //// ˢ�³�����ͼ
        //EditorApplication.QueuePlayerLoopUpdate();
        //    Debug.Log("����ˢ�º���");
        
    }

    void Update()
    {
        
    }
}
