using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSky : MonoBehaviour
{
    public Dropdown skychangeDropdown;
    public Material[] skyboxes;

    // Start is called before the first frame update
    void Start()
    {
        skychangeDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void OnDropdownValueChanged(int value)
    {
        // ȷ��ѡ��ֵ�ں���Χ��
        if (value >= 0 && value < skyboxes.Length)
        {
            // �л���պ�
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox; // ���û�������ģʽΪSkybox
            RenderSettings.skybox = skyboxes[value];
            DynamicGI.UpdateEnvironment();
        }
    }
}
