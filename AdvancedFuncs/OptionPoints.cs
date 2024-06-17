using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class OptionPoints : MonoBehaviour
{
    public Dropdown PointDropdown;  //获取选择景点的下拉框

    public Dropdown LineDropdown;   //获取路线的下拉框

    public GameObject spherePrefab;  //原本球体
    public GameObject fbxPrefab;   //fbx模型

    public Material targetMaterial; // 设置之前设置好的材质球

    public DrawLines pointsInform;//  得到某路线的点信息

    // 更改球体的样式为 FBX 格式的模型
    //GameObject[] fbxModelInstance;
    GameObject fbxModelInstance;

    //public List<GameObject> spheres; // 放置好的球体列表
    // 用于存储生成的物体的List数组
    private List<GameObject> generatedObjects = new List<GameObject>();

    public Tooltip tooltip; // 提示组件
    public MonoScript scriptGaoliang; // 要添加的脚本文件，高亮显示的脚本

    string selectedText;  //待改球体的名字

    /// <summary>
    /// 为景点框增加内容
    /// </summary>
    public void LineValueChange()
    {
        PointDropdown.ClearOptions();  //清除景点框之前的内容

        // 创建一个新的下拉选项列表，并设置默认选项
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("请选择景点"));

        // 设置默认选中项
        PointDropdown.value = 0;

        //根据路线来添加景点框内容

        switch (LineDropdown.value)
        {
            case 1:
                // 添加新的下拉选项 
                options.Add(new Dropdown.OptionData("起点"));
                options.Add(new Dropdown.OptionData("飞来石"));
                options.Add(new Dropdown.OptionData("汉口峡"));
                options.Add(new Dropdown.OptionData("大月山"));
                options.Add(new Dropdown.OptionData("七里冲路"));
                options.Add(new Dropdown.OptionData("终点"));

                break;

            case 2:
                options.Add(new Dropdown.OptionData("起点"));
                options.Add(new Dropdown.OptionData("剪刀峡"));
                options.Add(new Dropdown.OptionData("碧龙潭"));
                options.Add(new Dropdown.OptionData("电站大坝"));
                options.Add(new Dropdown.OptionData("终点"));
                break;

            case 3:
                options.Add(new Dropdown.OptionData("起点"));
                options.Add(new Dropdown.OptionData("美庐"));
                options.Add(new Dropdown.OptionData("庐山石刻博物馆"));
                options.Add(new Dropdown.OptionData("庐山会议旧址"));
                options.Add(new Dropdown.OptionData("芦林湖"));
                options.Add(new Dropdown.OptionData("终点"));
                break;

            case 4:
                options.Add(new Dropdown.OptionData("起点"));
                options.Add(new Dropdown.OptionData("太乙峰"));
                options.Add(new Dropdown.OptionData("五老峰"));
                options.Add(new Dropdown.OptionData("三叠泉"));
                options.Add(new Dropdown.OptionData("终点"));
                break;

           
        }

        //将内容添加到景点框 
        PointDropdown.AddOptions(options);

    }


    /// <summary>
    /// 模型更改法
    /// </summary>
    public void styleChanged()
    {
        selectedText = PointDropdown.options[PointDropdown.value].text;

        //Debug.Log("selectedText:" + selectedText);

        GameObject selectedSphere = null; 
        foreach (GameObject sphere in pointsInform.visibleBalls)  //pointsInform.ScenesPoints球体列表   找球
        {
            Debug.Log("sphere的name:" + sphere.name);
            if (sphere.name == selectedText)
            {
                selectedSphere = sphere;
                break;
            }
        }

        if (selectedSphere != null)   //改球
        {
            Renderer renderer = selectedSphere.GetComponent<Renderer>();
            if (renderer != null)
            {
                    // 更改球体的样式为 FBX 格式的模型
                    fbxModelInstance = Instantiate(fbxPrefab, selectedSphere.transform.position, selectedSphere.transform.rotation);
                    fbxModelInstance.transform.localScale = selectedSphere.transform.localScale;
                    fbxModelInstance.transform.localScale = new Vector3(5, 5, 5);
                    fbxModelInstance.name = selectedText;

                    selectedSphere.SetActive(false);  //原来的球不可见

                //把点放到加载到父对象下面   放到drawlines中各景点的父对象下
                // 查找代码文件DrawLines中生成的父对象B
                GameObject parentObjectB = GameObject.Find("ParentObject");

                // 将物体A添加到父对象B当中
                fbxModelInstance.transform.SetParent(parentObjectB.transform);

                tooltip.AddTooltip(fbxModelInstance);   //添加tag:Scene
                fbxModelInstance.AddComponent(scriptGaoliang.GetClass());  //为每一个点添加高亮脚本

                //添加box clooider
                BoxCollider boxCollider = fbxModelInstance.AddComponent<BoxCollider>();
                boxCollider.size = new Vector3(0.01f, 0.08f, 0.03f);

                //添加到list中
                generatedObjects.Add(fbxModelInstance);
                //Debug.Log("添加了物体之后，list内有几个：" + generatedObjects.Count);
                
                changeMaterial();//更改材质球  进行双面显示

                //为每个点信息展示的Pannel的脚本
                fbxModelInstance.AddComponent<OBJInputText>();

            }
            else
            {
                Debug.LogError("找不到 Renderer 组件！");
            }
        }
        else
        {
            Debug.LogError("找不到符合名字的球体！");
        }

    }


    /// <summary>
    /// 进行双面渲染
    /// </summary>
    private void changeMaterial()
    {
        Debug.Log("在双面渲染中，list有几个:" + generatedObjects.Count) ;
        // 遍历List中的每个GameObject对象
        foreach (GameObject obj in generatedObjects)
        {
            Debug.Log("调用函数，开始遍历");
            // 获取对象上的Renderer组件
            Renderer renderer = obj.GetComponent<Renderer>();

            // 检查对象上是否有Renderer组件
            if (renderer != null)
            {
                // 设置对象的材质球为新的材质球
                renderer.material = targetMaterial;
            }
            else
            {
                Debug.LogWarning("GameObject " + obj.name + " does not have a Renderer component.");
            }
        }


        //// 获取 FBX 模型
        //GameObject fbxModel = GameObject.Find(selectedText); // 替换为您的 FBX 模型的名称

        //if (fbxModel != null)
        //{
        //    // 获取模型的渲染器组件
        //    Renderer renderer = fbxModel.GetComponent<Renderer>();

        //    if (renderer != null)
        //    {
        //        // 更改模型的材质球
        //        renderer.material = targetMaterial;
        //    }
        //    else
        //    {
        //        Debug.LogError("模型没有 Renderer 组件！");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("找不到 FBX 模型！");
        //}

    }


    /// <summary>
    /// 路线发生改变时，销毁之前的旗帜
    /// </summary>
    public  void fbxDestory()
    {
        //Debug.Log("LineDropdown.value:" + LineDropdown.value);
        //Debug.Log("LineID:" + previousValue);

        if (LineDropdown.value != 0)   //若选择的路线发生改变
        {
            if (generatedObjects.Count > 0)   //若list中有物体
            {
                foreach (GameObject obj in generatedObjects)
                {
                    Destroy(obj);  //则销毁
                }

                // 清空List数组
                generatedObjects.Clear();
            }
        }
    }
}
