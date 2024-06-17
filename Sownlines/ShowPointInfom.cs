using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPointInfom : MonoBehaviour
{
    public Text infoText;  //显示内容 
    public GameObject infoPanel;  //内容面板
    string pointInfo = "";    //物体简介

    private string pointName;  //物体名字

    //获得鼠标触碰物体的名字
    //public OBJInputText pointNameGet;   //调用函数OBJInputText

    void Start()
    {
        infoPanel.SetActive(false); // 初始时隐藏信息面板
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  //左键按下
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Scene")
                {
                    pointName = OBJInputText.example.pointName;   //调用OBJInputText函数里的变量

                    if (pointName== "东谷柏树路")
                    {
                        pointInfo = "东谷柏树路位于庐山东南部的东谷景区内，全长约5公里，沿途种植了数百棵古老的柏树，树干高大笔直，枝繁叶茂，是庐山景区著名的赏秋胜地之一。";
                    }

                    else if(pointName == "汉口峡")
                    {
                        pointInfo = "汉口峡是由河流袭夺形成的。当庐山上升时，山体切割了原来的河谷，使得一些小河切穿插女儿城山岭，袭夺了大校场河上游的河道，导致汉口峡的形成。";
                    }
                    else if (pointName == "大月山")
                    {
                        pointInfo = "大月山位于我国江西省九江市庐山市庐山，为庐山第二高峰，仅次于大汉阳峰（1474米）。大月山水系发达,山顶建有水库,是庐山居民饮用水源地之一。";
                    }
                    else if (pointName == "七里冲路")
                    {
                        pointInfo = "七里冲路是通往大月山的必经之路。这条小路是山路，其中植物较茂盛，地面起伏较大，走这条路的时候需要注意哦~";
                    }
                    else if (pointName == "飞来石")
                    {
                        pointInfo = "飞来石位于庐山的东北部，是一块巨大的花岗岩石头，形状独特，好像一块从天而降的大石头。飞来石是庐山的标志性景点之一。";
                    }

                    else if (pointName == "剪刀峡")
                    {
                        pointInfo = "剪刀峡，位于江西省九江市庐山腹地，云中山城牯岭北侧东林大峡谷内，是生态文化旅游休闲度假区，是世界文化遗产、世界地质公园、世界唯一优秀生态景区、国家5A级风景区——庐山的主要构成部分。剪刀峡呈弯曲状槽谷，两侧山仪重叠夹峙，宛如把把剪刀。";
                    }
                    else if (pointName == "碧龙潭")
                    {
                        pointInfo = "碧龙潭位于江西九江市庐山小天池东北面，九江市南郊。该景区全长2000余米的山谷，有自然景点30多处，风景奇特，原始、幽静、群峰峥嵘，峭壁悬崖，云雾飘渺，水清甜爽，山绿诱人，石怪有形，双瀑似蛟龙出岫，潭中二龙戏珠。";
                    }
                    else if (pointName == "电站大坝")
                    {
                        pointInfo = "庐山的电站大坝位于山脚下，是庐山的一个重要设施。大坝是庐山上的水电站建设的一部分，通过蓄水发电，为周边地区提供电力。游客可以前往电站大坝参观，了解水电站的建设和发电原理。";
                    }

                    else if (pointName == "美庐")
                    {
                        pointInfo = "美庐位于江西省九江市庐山风景区内，是庐山所特有的一处著名的人文景观，它展示了风云变幻的中国现代史的一个侧面。“美庐”曾作为蒋介石的夏都官邸，“主席行辕”，是当年“第一夫人”宋美龄生活的“美的房子”。";
                    }
                    else if (pointName == "庐山石刻博物馆")
                    {
                        pointInfo = "庐山石刻博物馆位于河西路29号，占地面积1800平方米，建筑面积731.5平方米。其内设有通介和志地、记事、化景、骋怀、弘道、明誓六个主题，通过对庐山石刻精品以图文、部分实物、多媒体等形式集中展示，充分展示了庐山的石刻文化内涵。";
                    }
                    else if (pointName == "庐山会议旧址")
                    {
                        pointInfo = "庐山会议旧址是中国革命的重要场所之一，许多重要的会议和事件都在庐山上举行。会议旧址是这些历史事件的见证地，游客可以前往参观，了解中国革命历史。";
                    }
                    else if (pointName == "芦林湖")
                    {
                        pointInfo = "芦林湖位于江西省九江市庐山区海拔1040米的东谷芦林盆地，故又称东湖。芦林湖四周群山环抱，苍松翠柏，湖水如镜，似发光的碧玉镶嵌在林荫秀谷之中，在缥缈的云烟衬托下，犹如天上神湖。";
                    }

                    else if (pointName == "太乙峰")
                    {
                        pointInfo = "太乙峰拔地而起，卓尔不群，特立高标。整座山峰全由嶙峋的怪石累叠而成，绝少草木，远看犹如一座巨大的石雕。既像埃及的金字塔，塔尖直刺苍穹，更像一柄倒竖的犁耙，锋利的犁头在阳光下闪烁着凛冽的寒光，故又得名“犁头尖”。";
                    }
                    else if (pointName == "五老峰")
                    {
                        pointInfo = "五老峰是庐山的五座主要峰之一，分别为东五老、西五老、南五老、北五老和中五老。五老峰是庐山的标志性地标之一，每座峰顶都有不同的景色和特色，是徒步爱好者和摄影爱好者喜欢前往的地方。";
                    }
                    else if (pointName == "三叠泉")
                    {
                        pointInfo = "三叠泉位于庐山的南部，是庐山最著名的泉水之一。三叠泉的水流分三级，形成了瀑布景观，水质清澈甘甜，被誉为“天下第一泉”，是庐山的景点之一，也是游客常去的地方之一。去往三叠泉的路上有非常多的楼梯！！但是慢慢走也能够完成~";
                    }
                    else if (pointName == "望江亭")
                    {
                        pointInfo = "望江亭是庐山的一处观景台，位于山顶，可俯瞰庐山风光和长江美景。望江亭是庐山上一处观赏日出、云海的绝佳地点，是庐山著名的观景点之一。";
                    }
                    else if (pointName == "小天池")
                    {
                        pointInfo = "小天池是庐山上的一个天然湖泊，湖水清澈碧蓝，四周群山环绕，风景优美。小天池是庐山著名的景点之一，也是游客喜欢前往的地方。";
                    }
                    else if (pointName == "王家坡谷地")
                    {
                        pointInfo = "王家坡谷地位于庐山西南部，是庐山的一个幽静而美丽的谷地。谷地内有茂密的森林、清澈的溪流、古老的民居等，是一个适合徒步和休闲的地方。但是这条路很难走，要有心理准备哦~";
                    }
                    else if (pointName == "锦绣谷")
                    {
                        pointInfo = "锦绣谷位于庐山的西南部，是一个自然风光优美的谷地。锦绣谷内有茂密的森林、清澈的溪流和各种奇石，是一个适合徒步和观赏风景的地方。";
                    }
                    else if (pointName == "仙人洞")
                    {
                        pointInfo = "仙人洞位于庐山的南部，是一个古老的道观建筑群，相传是古代道士修炼的地方。仙人洞内有道教文化遗迹和壁画，是庐山的历史文化景点之一。在仙人洞处，能够俯瞰整个庐山，眼界非常开阔~";
                    }
                    else if (pointName == "大天池")
                    {
                        pointInfo = "大天池是庐山上最大的天然湖泊，湖水清澈见底，四周群山环绕。大天池是庐山最著名的景点之一，也是游客喜欢前往的地方，可以体验划船、观赏湖景等活动。";
                    }
                    else if (pointName == "龙首崖")
                    {
                        pointInfo = "龙首崖位于庐山的东北部，是一个陡峭的悬崖，形状酷似一条龙头。龙首崖是庐山的景点之一，也是一个观赏奇特地质景观的地方。";
                    }
                    else if (pointName == "含鄱口")
                    {
                        pointInfo = "含鄱口位于庐山的西北部，是一个观景台，可以俯瞰庐山风景区的全景。含鄱口是庐山上最著名的观景点之一，游客可以在这里欣赏到壮丽的群山景色和云雾缭绕的风景。";
                    }
                    else if (pointName == "黄龙寺")
                    {
                        pointInfo = "黄龙寺位于庐山的西南部，是一座历史悠久的古寺。黄龙寺建于唐代，寺内供奉着观音菩萨。寺内建筑古朴典雅，周围环境清幽宁静，是庐山的一处佛教圣地，也是游客参观的热门景点之一。";
                    }
                    else if (pointName == "黄龙潭")
                    {
                        pointInfo = "黄龙潭位于庐山的西北部，是一个天然湖泊，湖水清澈见底。黄龙潭周围绿树成荫，景色宜人，是一个适合游玩和休闲的地方。游客可以在湖边漫步，欣赏湖光山色，感受大自然的清新和宁静。";
                    }
                    else if (pointName == "牯岭街")
                    {
                        pointInfo = "牯岭街位于庐山的山脚下，是一个历史悠久的老街区。牯岭街保存着许多古老的建筑和传统的民俗文化，是一个可以体验传统生活和风情的地方。游客可以在牯岭街漫步，欣赏古老建筑和购买当地特色商品。";
                    }
                    else if (pointName == "别墅群")
                    {
                        pointInfo = "庐山近代别墅群的建筑风格，产生出特有的风韵。每一座别墅，都是单体建筑，建筑的格局、式样、风格，注入了原别墅主人所在国籍的本土文化的影子，别墅主人审美趣味和爱好的影子。";
                    }
                    else if (pointName == "芦林一号")
                    {
                        pointInfo = "芦林一号是庐山上一条著名的登山步道，全长约10公里，沿途风景优美，有着许多景点和观景台。登山者可以沿着芦林一号步道徒步登山，欣赏庐山的壮丽景色。";
                    }
                    else if (pointName == "庐山博物馆")
                    {
                        pointInfo = "庐山博物馆位于庐山风景区内，是一个展示庐山历史文化和自然风光的博物馆。博物馆内陈列着各种文物和资料，介绍庐山的历史、地理和人文景观，是了解庐山的重要场所之一。";
                    }
                    else if (pointName == "西谷大林路")
                    {
                        pointInfo = "西谷大林路是庐山上一条著名的观景路线，沿途风景秀丽，有着茂密的森林和清澈的溪流。游客可以沿着西谷大林路漫步，欣赏庐山独特的自然风光。";
                    }
                    else if (pointName == "月照松林")
                    {
                        pointInfo = "月照松林是庐山上一处景点，以其壮观的松林和美丽的月光景观而闻名。夜晚月光照耀下的松林，给人一种神秘而宁静的感觉，是庐山夜晚的一大景观。";
                    }
                    else if (pointName == "如琴湖")
                    {
                        pointInfo = "如琴湖是庐山上一个美丽的湖泊，湖水清澈见底，周围环境幽静优美。如琴湖水域广阔，水质清澈，游客可以在湖边散步、欣赏湖景，感受宁静与美丽。";
                    }
                    else if (pointName == "白马寺花径")
                    {
                        pointInfo = "白马寺花径是庐山上一个著名的观赏花径，四季花香不断。春季时，花径两旁开满了各种花卉，色彩斑斓，香气扑鼻，是欣赏花卉的绝佳去处。";
                    }
                    else if (pointName == "白居易草堂")
                    {
                        pointInfo = "白居易草堂是庐山上一处古老的文化景点，这里是唐代文学家白居易曾经居住过的地方。白居易草堂保存完好，游客可以参观白居易的遗迹，了解他的生平和文学成就。";
                    }

                    ShowInfoPanel(pointInfo);
                    infoPanel.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 25, 0);

                      if (pointName == "起点")
                    {
                        infoPanel.SetActive(false);
                    }
                     if (pointName == "终点")
                    {
                        infoPanel.SetActive(false);
                    }

                }

               
            }

        }

        if (Input.GetMouseButtonDown(1))  //右键按下
        {
            CloseInfoPanel();
        }

    }

    void ShowInfoPanel(string info)
    {
        infoText.text = info; // 设置信息文本
        infoPanel.SetActive(true); // 显示信息面板


        //// 获取物体上的RectTransform组件
        //RectTransform rectTransform = GetComponent<RectTransform>();

        //// 添加Content Size Fitter组件
        //ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();

        //// 设置Content Size Fitter组件的属性
        //contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        //contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false); // 关闭信息面板
    }
}
