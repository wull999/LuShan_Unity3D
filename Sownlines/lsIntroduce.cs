using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lsIntroduce : MonoBehaviour
{
    private string introduce;  //�洢������Ϣ

    public GameObject lsInformPannel;  //�������
    public Text infoText;  //��ʾ���� 
    // Start is called before the first frame update
    void Start()
    {
        introduce = "        ®ɽ��������ɽ����®��λ�ڽ���ʡ�Ž���®ɽ�о��ڡ��䱱������������۶�������Ͽ��ϲ����������ھ�����·�������ڳ���������ƽԭ��۶�����ϡ�\r\n        ®ɽ����������������Ȼ��������̬���������һ�ӵ�зḻ�����ľ��ۣ���®ɽ��ʵϰ�����У�ͬѧ�ǲ����ܹ�ѧϰ�����ʹ��졢����Ӱ���ˮ�ĵ�֪ʶ���˽�®ɽ���ڽ��Ļ��������Ļ������ľ��ۡ�����ʵϰ������·�ߣ�ÿ��·�߶�����಻ͬ�����֪ʶ��ͨ����ͬ·�ߵ�ʵϰ��ͬѧ�ǲ����ܹ�ѧ�����򡢵��ʡ�ˮ�ĺ�ֲ����֪ʶ�����ܹ��˽⵽®ɽ����ʷ�Ļ�֪ʶ��\r\n     �������Ϳ�ʼ�˽�®ɽ��ʵϰ·�߰�~";
        lsInformPannel.SetActive(false);  //�ʼ���ɼ�

    }

    // Update is called once per frame
    

    public void showIntro()
    {
            infoText.text = introduce; // ������Ϣ�ı�
            lsInformPannel.SetActive(true); // ��ʾ��Ϣ���
    }

    public void ShowExit()
    {
        lsInformPannel.SetActive(false); // ��ʾ��Ϣ���
    }
}
