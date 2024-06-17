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
        // 确保选项值在合理范围内
        if (value >= 0 && value < skyboxes.Length)
        {
            // 切换天空盒
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox; // 设置环境光照模式为Skybox
            RenderSettings.skybox = skyboxes[value];
            DynamicGI.UpdateEnvironment();
        }
    }
}
