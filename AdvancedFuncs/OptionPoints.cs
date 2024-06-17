using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class OptionPoints : MonoBehaviour
{
    public Dropdown PointDropdown;  //��ȡѡ�񾰵��������

    public Dropdown LineDropdown;   //��ȡ·�ߵ�������

    public GameObject spherePrefab;  //ԭ������
    public GameObject fbxPrefab;   //fbxģ��

    public Material targetMaterial; // ����֮ǰ���úõĲ�����

    public DrawLines pointsInform;//  �õ�ĳ·�ߵĵ���Ϣ

    // �����������ʽΪ FBX ��ʽ��ģ��
    //GameObject[] fbxModelInstance;
    GameObject fbxModelInstance;

    //public List<GameObject> spheres; // ���úõ������б�
    // ���ڴ洢���ɵ������List����
    private List<GameObject> generatedObjects = new List<GameObject>();

    public Tooltip tooltip; // ��ʾ���
    public MonoScript scriptGaoliang; // Ҫ��ӵĽű��ļ���������ʾ�Ľű�

    string selectedText;  //�������������

    /// <summary>
    /// Ϊ�������������
    /// </summary>
    public void LineValueChange()
    {
        PointDropdown.ClearOptions();  //��������֮ǰ������

        // ����һ���µ�����ѡ���б�������Ĭ��ѡ��
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("��ѡ�񾰵�"));

        // ����Ĭ��ѡ����
        PointDropdown.value = 0;

        //����·������Ӿ��������

        switch (LineDropdown.value)
        {
            case 1:
                // ����µ�����ѡ�� 
                options.Add(new Dropdown.OptionData("���"));
                options.Add(new Dropdown.OptionData("����ʯ"));
                options.Add(new Dropdown.OptionData("����Ͽ"));
                options.Add(new Dropdown.OptionData("����ɽ"));
                options.Add(new Dropdown.OptionData("�����·"));
                options.Add(new Dropdown.OptionData("�յ�"));

                break;

            case 2:
                options.Add(new Dropdown.OptionData("���"));
                options.Add(new Dropdown.OptionData("����Ͽ"));
                options.Add(new Dropdown.OptionData("����̶"));
                options.Add(new Dropdown.OptionData("��վ���"));
                options.Add(new Dropdown.OptionData("�յ�"));
                break;

            case 3:
                options.Add(new Dropdown.OptionData("���"));
                options.Add(new Dropdown.OptionData("��®"));
                options.Add(new Dropdown.OptionData("®ɽʯ�̲����"));
                options.Add(new Dropdown.OptionData("®ɽ�����ַ"));
                options.Add(new Dropdown.OptionData("«�ֺ�"));
                options.Add(new Dropdown.OptionData("�յ�"));
                break;

            case 4:
                options.Add(new Dropdown.OptionData("���"));
                options.Add(new Dropdown.OptionData("̫�ҷ�"));
                options.Add(new Dropdown.OptionData("���Ϸ�"));
                options.Add(new Dropdown.OptionData("����Ȫ"));
                options.Add(new Dropdown.OptionData("�յ�"));
                break;

           
        }

        //��������ӵ������ 
        PointDropdown.AddOptions(options);

    }


    /// <summary>
    /// ģ�͸��ķ�
    /// </summary>
    public void styleChanged()
    {
        selectedText = PointDropdown.options[PointDropdown.value].text;

        //Debug.Log("selectedText:" + selectedText);

        GameObject selectedSphere = null; 
        foreach (GameObject sphere in pointsInform.visibleBalls)  //pointsInform.ScenesPoints�����б�   ����
        {
            Debug.Log("sphere��name:" + sphere.name);
            if (sphere.name == selectedText)
            {
                selectedSphere = sphere;
                break;
            }
        }

        if (selectedSphere != null)   //����
        {
            Renderer renderer = selectedSphere.GetComponent<Renderer>();
            if (renderer != null)
            {
                    // �����������ʽΪ FBX ��ʽ��ģ��
                    fbxModelInstance = Instantiate(fbxPrefab, selectedSphere.transform.position, selectedSphere.transform.rotation);
                    fbxModelInstance.transform.localScale = selectedSphere.transform.localScale;
                    fbxModelInstance.transform.localScale = new Vector3(5, 5, 5);
                    fbxModelInstance.name = selectedText;

                    selectedSphere.SetActive(false);  //ԭ�����򲻿ɼ�

                //�ѵ�ŵ����ص�����������   �ŵ�drawlines�и�����ĸ�������
                // ���Ҵ����ļ�DrawLines�����ɵĸ�����B
                GameObject parentObjectB = GameObject.Find("ParentObject");

                // ������A��ӵ�������B����
                fbxModelInstance.transform.SetParent(parentObjectB.transform);

                tooltip.AddTooltip(fbxModelInstance);   //���tag:Scene
                fbxModelInstance.AddComponent(scriptGaoliang.GetClass());  //Ϊÿһ������Ӹ����ű�

                //���box clooider
                BoxCollider boxCollider = fbxModelInstance.AddComponent<BoxCollider>();
                boxCollider.size = new Vector3(0.01f, 0.08f, 0.03f);

                //��ӵ�list��
                generatedObjects.Add(fbxModelInstance);
                //Debug.Log("���������֮��list���м�����" + generatedObjects.Count);
                
                changeMaterial();//���Ĳ�����  ����˫����ʾ

                //Ϊÿ������Ϣչʾ��Pannel�Ľű�
                fbxModelInstance.AddComponent<OBJInputText>();

            }
            else
            {
                Debug.LogError("�Ҳ��� Renderer �����");
            }
        }
        else
        {
            Debug.LogError("�Ҳ����������ֵ����壡");
        }

    }


    /// <summary>
    /// ����˫����Ⱦ
    /// </summary>
    private void changeMaterial()
    {
        Debug.Log("��˫����Ⱦ�У�list�м���:" + generatedObjects.Count) ;
        // ����List�е�ÿ��GameObject����
        foreach (GameObject obj in generatedObjects)
        {
            Debug.Log("���ú�������ʼ����");
            // ��ȡ�����ϵ�Renderer���
            Renderer renderer = obj.GetComponent<Renderer>();

            // ���������Ƿ���Renderer���
            if (renderer != null)
            {
                // ���ö���Ĳ�����Ϊ�µĲ�����
                renderer.material = targetMaterial;
            }
            else
            {
                Debug.LogWarning("GameObject " + obj.name + " does not have a Renderer component.");
            }
        }


        //// ��ȡ FBX ģ��
        //GameObject fbxModel = GameObject.Find(selectedText); // �滻Ϊ���� FBX ģ�͵�����

        //if (fbxModel != null)
        //{
        //    // ��ȡģ�͵���Ⱦ�����
        //    Renderer renderer = fbxModel.GetComponent<Renderer>();

        //    if (renderer != null)
        //    {
        //        // ����ģ�͵Ĳ�����
        //        renderer.material = targetMaterial;
        //    }
        //    else
        //    {
        //        Debug.LogError("ģ��û�� Renderer �����");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("�Ҳ��� FBX ģ�ͣ�");
        //}

    }


    /// <summary>
    /// ·�߷����ı�ʱ������֮ǰ������
    /// </summary>
    public  void fbxDestory()
    {
        //Debug.Log("LineDropdown.value:" + LineDropdown.value);
        //Debug.Log("LineID:" + previousValue);

        if (LineDropdown.value != 0)   //��ѡ���·�߷����ı�
        {
            if (generatedObjects.Count > 0)   //��list��������
            {
                foreach (GameObject obj in generatedObjects)
                {
                    Destroy(obj);  //������
                }

                // ���List����
                generatedObjects.Clear();
            }
        }
    }
}
