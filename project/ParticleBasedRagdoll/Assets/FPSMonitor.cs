using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class FPSMonitor : MonoBehaviour
{
    const float fpsMeasurePeriod = 0.5f;    //FPS测量间隔
    private int m_FpsAccumulator = 0;   //帧数累计的数量
    private float m_FpsNextPeriod = 0;  //FPS下一段的间隔
    private int m_CurrentFps;   //当前的帧率
    const string display = "{0} FPS";   //显示的文字
    private Text m_Text;    //UGUI中Text组件

    // Start is called before the first frame update
    void Start()
    {
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod; //Time.realtimeSinceStartup获取游戏开始到当前的时间，增加一个测量间隔，计算出下一次帧率计算是要在什么时候
        m_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // 测量每一秒的平均帧率
        m_FpsAccumulator++;
        if (Time.realtimeSinceStartup > m_FpsNextPeriod)    //当前时间超过了下一次的计算时间
        {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);   //计算
            m_FpsAccumulator = 0;   //计数器归零
            m_FpsNextPeriod += fpsMeasurePeriod;    //在增加下一次的间隔
            m_Text.text = string.Format(display, m_CurrentFps); //处理一下文字显示
        }
    }
}
