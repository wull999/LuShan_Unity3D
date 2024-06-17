using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lsIntroduce : MonoBehaviour
{
    private string introduce;  //存储介绍信息

    public GameObject lsInformPannel;  //内容面板
    public Text infoText;  //显示内容 
    // Start is called before the first frame update
    void Start()
    {
        introduce = "        庐山，又名匡山、匡庐。位于江西省九江市庐山市境内。其北濒长江，东接鄱阳湖，南靠南昌滕王阁，西邻京九铁路，耸峙于长江中下游平原与鄱阳湖畔。\r\n        庐山不仅具有优美的自然环境和生态环境，而且还拥有丰富的人文景观，在庐山的实习过程中，同学们不仅能够学习到地质构造、气候影响和水文等知识，了解庐山的宗教文化、政治文化等人文景观。对于实习的七条路线，每条路线都有许多不同方面的知识。通过不同路线的实习，同学们不仅能够学到气候、地质、水文和植被的知识，还能够了解到庐山的历史文化知识。\r\n     接下来就开始了解庐山的实习路线吧~";
        lsInformPannel.SetActive(false);  //最开始不可见

    }

    // Update is called once per frame
    

    public void showIntro()
    {
            infoText.text = introduce; // 设置信息文本
            lsInformPannel.SetActive(true); // 显示信息面板
    }

    public void ShowExit()
    {
        lsInformPannel.SetActive(false); // 显示信息面板
    }
}
