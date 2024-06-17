using System;
using System.Collections.Generic;
using UnityEngine;
using XCharts;

public class chartTest : MonoBehaviour
{
    [Header("Chart��������")]
    public GameObject chartObj;

    [Header("Y������"), Min(1)]
    public int yMinValue = 200;
    public int yMaxValue = 1400;
    public int ySplitValue = 300;

    [Header("����")]
    public float lsRealHeight = 1469.255f; //���ڼ�����ʵ�߶�
    public float demUnityHeight = 0.5f;

    private GameObject _parentObject;  //��ȡ���㸸����
    private LineChart _lineChart;

    private class AltitudeData
    {
        public string PlaceName;
        public Vector3 Pos;
        public float Altitude;

        public override string ToString()
        {
            return $"���ƣ�{PlaceName}   ���Σ�{Altitude}";
        }
    }

    private Dictionary<string, AltitudeData> _altitudeDatas = new();

    private void Start()
    {
        CreateLineChart();  //ͼ�Ĵ���
    }

    //����ͼ��
    private void CreateLineChart()
    {
        //���붯̬���ͼ����Ҫ���óߴ�
        _lineChart = chartObj.GetComponent<LineChart>();
        if (_lineChart == null)
        {
            _lineChart = chartObj.AddComponent<LineChart>();
        }

        InitChart();  //��ʼ��ͼ��
        RefreshChart();
        HideChart();
    }

    /// <summary>
    /// ��ʼ��chart��
    /// ���ڳ�ʼʱ����һ�Σ�֮���ٸı������
    /// </summary>
    private void InitChart()
    {
        //���붯̬���ͼ����Ҫ���óߴ�
        _lineChart.SetSize(580, 300);

        //���ñ��⣺
        _lineChart.title.show = true;
        _lineChart.title.text = "�����㺣�α仯ʾ��ͼ";

        //������ʾ���ͼ���Ƿ���ʾ
        _lineChart.tooltip.show = true;
        _lineChart.legend.show = false;

        //�����Ƿ�ʹ��˫�����������������
        _lineChart.xAxes[0].show = true;
        _lineChart.xAxes[1].show = false;
        _lineChart.yAxes[0].show = true;
        _lineChart.yAxes[1].show = false;
        _lineChart.xAxes[0].type = Axis.AxisType.Category;
        _lineChart.yAxes[0].type = Axis.AxisType.Value;

        //����������ָ���
        _lineChart.xAxes[0].boundaryGap = true;

        //������ݣ����`Line`���͵�`Serie`���ڽ�������
        _lineChart.RemoveData();
        _lineChart.AddSerie(SerieType.Line);

        var yAxis = _lineChart.yAxis0;
        yAxis.minMaxType = Axis.AxisMinMaxType.Custom;
        yAxis.min = yMinValue;  //������Сֵ
        yAxis.max = yMaxValue;  //�������ֵ
        var dValue = yAxis.max - yAxis.min;
        if (dValue < ySplitValue)
        {
            dValue = ySplitValue; // ������ַ�ΧС�ڼ����������ֶ�������ֵͬ�����������splitNumberΪ1
        }
        yAxis.splitNumber = Mathf.CeilToInt(dValue / ySplitValue); // ����ָ���Ŀ,С������ֱ����ȥ�������������ֽ�1
        yAxis.SetComponentDirty(); // ��������ݣ�������Ҫ��ˢ��
    }

    /// <summary>
    /// ˢ��chart��
    /// </summary>
    private void RefreshChart()
    {
        if (_altitudeDatas == null || _altitudeDatas.Count <= 0)
        {
            return;
        }

        var xAxis = _lineChart.xAxis0;
        xAxis.ClearData();
        xAxis.splitNumber = _altitudeDatas.Count; // ����Ϊ����ĳ���
        xAxis.SetComponentDirty();  // ��������ݣ�������Ҫ��ˢ��

        var serie0 = _lineChart.series.GetSerie(0);

        var dataDValue = serie0.data.Count - _altitudeDatas.Count;
        if (dataDValue > 0) // ��ǰchart�����ݶ��ڵ�ǰ����
        {
            serie0.data.RemoveRange(serie0.data.Count - dataDValue, dataDValue);
        }

        //���yֵ����
        var index = 0;
        foreach (var altitudeData in _altitudeDatas)
        {
            var xData = altitudeData.Value.PlaceName;
            var yData = altitudeData.Value.Altitude;

            xAxis.AddData(xData);

            if (index < serie0.data.Count) // ��������ŵ�x����������
            {
                var value = serie0.data[index].data[1]; // ��ȡyֵ
                if (Math.Abs(value - yData) > 0.001f) // ����͵�ǰ��ֵ��һ��
                {
                    serie0.UpdateYData(index, yData); // ����yֵ����,�����
                }
            }
            else
            {
                serie0.AddYData(yData); // ��������ŵ�x����û�����ݣ����
            }

            index++;
        }

        _lineChart.RefreshChart();
    }

    /// <summary>
    /// ��ʾ���
    /// </summary>
    public void ShowChart()
    {
        if (_lineChart != null)
        {
            _lineChart.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ���ر��
    /// </summary>
    public void HideChart()
    {
        if (_lineChart != null)
        {
            _lineChart.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ��ʾ���ر��
    /// </summary>
    public void ShowOrHideChart()
    {
        if (_lineChart != null)
        {
            _lineChart.gameObject.SetActive(!_lineChart.gameObject.activeSelf);
        }
    }

    //childPositions�洢x�����֣�childNames�洢y���ֵ
    public void Update()
    {
        //��ȡ����
        _parentObject = GameObject.Find("newParent");

        if (_parentObject != null)
        {
            var childCount = _parentObject.transform.childCount;
            _altitudeDatas.Clear();

            // �����Ӷ��󣬽����ֺ�λ�ô�����Ӧ����
            for (var i = 0; i < childCount; i++)
            {
                var child = _parentObject.transform.GetChild(i);
                var childName = child.name;
                var childPos = child.position;
                if (!_altitudeDatas.TryGetValue(childName, out var altitudeData))
                {
                    altitudeData = new AltitudeData();
                    _altitudeDatas.Add(childName, altitudeData);
                }

                altitudeData.PlaceName = childName;
                altitudeData.Pos = childPos;
                altitudeData.Altitude = GetRealHeight1(childPos.y);

                //Debug.Log(altitudeData.ToString());
            }

            RefreshChart();
        }
        else
        {
            //Debug.Log("û���ҵ�������");
        }
    }

    //���㺣��
    private float GetRealHeight1(float unityheight)
    {
        float rectify_Y = unityheight -0.05f - 0.1f + 0.009f + 0.08f;
        float real_H = rectify_Y * lsRealHeight / demUnityHeight;
        //Debug.Log("real_H:" + real_H);
        return real_H;
    }
}
