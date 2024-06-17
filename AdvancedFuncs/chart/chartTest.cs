using System;
using System.Collections.Generic;
using UnityEngine;
using XCharts;

public class chartTest : MonoBehaviour
{
    [Header("Chart挂载物体")]
    public GameObject chartObj;

    [Header("Y轴配置"), Min(1)]
    public int yMinValue = 200;
    public int yMaxValue = 1400;
    public int ySplitValue = 300;

    [Header("其它")]
    public float lsRealHeight = 1469.255f; //用于计算真实高度
    public float demUnityHeight = 0.5f;

    private GameObject _parentObject;  //获取各点父对象
    private LineChart _lineChart;

    private class AltitudeData
    {
        public string PlaceName;
        public Vector3 Pos;
        public float Altitude;

        public override string ToString()
        {
            return $"名称：{PlaceName}   海拔：{Altitude}";
        }
    }

    private Dictionary<string, AltitudeData> _altitudeDatas = new();

    private void Start()
    {
        CreateLineChart();  //图的创建
    }

    //绘制图表
    private void CreateLineChart()
    {
        //代码动态添加图表需要设置尺寸
        _lineChart = chartObj.GetComponent<LineChart>();
        if (_lineChart == null)
        {
            _lineChart = chartObj.AddComponent<LineChart>();
        }

        InitChart();  //初始化图表
        RefreshChart();
        HideChart();
    }

    /// <summary>
    /// 初始化chart表
    /// 仅在初始时设置一次，之后不再改变的设置
    /// </summary>
    private void InitChart()
    {
        //代码动态添加图表需要设置尺寸
        _lineChart.SetSize(580, 300);

        //设置标题：
        _lineChart.title.show = true;
        _lineChart.title.text = "各景点海拔变化示意图";

        //设置提示框和图例是否显示
        _lineChart.tooltip.show = true;
        _lineChart.legend.show = false;

        //设置是否使用双坐标轴和坐标轴类型
        _lineChart.xAxes[0].show = true;
        _lineChart.xAxes[1].show = false;
        _lineChart.yAxes[0].show = true;
        _lineChart.yAxes[1].show = false;
        _lineChart.xAxes[0].type = Axis.AxisType.Category;
        _lineChart.yAxes[0].type = Axis.AxisType.Value;

        //设置坐标轴分割线
        _lineChart.xAxes[0].boundaryGap = true;

        //清空数据，添加`Line`类型的`Serie`用于接收数据
        _lineChart.RemoveData();
        _lineChart.AddSerie(SerieType.Line);

        var yAxis = _lineChart.yAxis0;
        yAxis.minMaxType = Axis.AxisMinMaxType.Custom;
        yAxis.min = yMinValue;  //设置最小值
        yAxis.max = yMaxValue;  //设置最大值
        var dValue = yAxis.max - yAxis.min;
        if (dValue < ySplitValue)
        {
            dValue = ySplitValue; // 如果出现范围小于间隔的情况，手动设置相同值，即计算出的splitNumber为1
        }
        yAxis.splitNumber = Mathf.CeilToInt(dValue / ySplitValue); // 计算分割数目,小数部分直接舍去，并向正数部分进1
        yAxis.SetComponentDirty(); // 标记脏数据，代表需要被刷新
    }

    /// <summary>
    /// 刷新chart表
    /// </summary>
    private void RefreshChart()
    {
        if (_altitudeDatas == null || _altitudeDatas.Count <= 0)
        {
            return;
        }

        var xAxis = _lineChart.xAxis0;
        xAxis.ClearData();
        xAxis.splitNumber = _altitudeDatas.Count; // 设置为数组的长度
        xAxis.SetComponentDirty();  // 标记脏数据，代表需要被刷新

        var serie0 = _lineChart.series.GetSerie(0);

        var dataDValue = serie0.data.Count - _altitudeDatas.Count;
        if (dataDValue > 0) // 当前chart上数据多于当前数据
        {
            serie0.data.RemoveRange(serie0.data.Count - dataDValue, dataDValue);
        }

        //添加y值数据
        var index = 0;
        foreach (var altitudeData in _altitudeDatas)
        {
            var xData = altitudeData.Value.PlaceName;
            var yData = altitudeData.Value.Altitude;

            xAxis.AddData(xData);

            if (index < serie0.data.Count) // 如果这个序号的x轴上有数据
            {
                var value = serie0.data[index].data[1]; // 获取y值
                if (Math.Abs(value - yData) > 0.001f) // 如果和当前的值不一样
                {
                    serie0.UpdateYData(index, yData); // 更新y值数据,不添加
                }
            }
            else
            {
                serie0.AddYData(yData); // 如果这个序号的x轴上没有数据，添加
            }

            index++;
        }

        _lineChart.RefreshChart();
    }

    /// <summary>
    /// 显示表格
    /// </summary>
    public void ShowChart()
    {
        if (_lineChart != null)
        {
            _lineChart.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 隐藏表格
    /// </summary>
    public void HideChart()
    {
        if (_lineChart != null)
        {
            _lineChart.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 显示隐藏表格
    /// </summary>
    public void ShowOrHideChart()
    {
        if (_lineChart != null)
        {
            _lineChart.gameObject.SetActive(!_lineChart.gameObject.activeSelf);
        }
    }

    //childPositions存储x轴名字，childNames存储y轴的值
    public void Update()
    {
        //获取对象
        _parentObject = GameObject.Find("newParent");

        if (_parentObject != null)
        {
            var childCount = _parentObject.transform.childCount;
            _altitudeDatas.Clear();

            // 遍历子对象，将名字和位置存入相应数组
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
            //Debug.Log("没有找到父对象");
        }
    }

    //反算海拔
    private float GetRealHeight1(float unityheight)
    {
        float rectify_Y = unityheight -0.05f - 0.1f + 0.009f + 0.08f;
        float real_H = rectify_Y * lsRealHeight / demUnityHeight;
        //Debug.Log("real_H:" + real_H);
        return real_H;
    }
}
