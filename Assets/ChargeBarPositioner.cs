using UnityEngine;

public class ChargeBarPositioner : MonoBehaviour
{
    [Header("蓄力条设置")]
    public RectTransform chargeBar; // 蓄力条的背景 RectTransform
    public float barWidth = 300f;   // 蓄力条宽度
    public float barHeight = 30f;   // 蓄力条高度
    public float bottomOffset = 50f; // 蓄力条距离屏幕底部的距离

    private void Start()
    {
        PositionChargeBar();
    }

    private void Update()
    {
        // 如果屏幕尺寸可能动态变化，可以在每帧更新位置
        PositionChargeBar();
    }

    private void PositionChargeBar()
    {
        if (chargeBar == null)
        {
            Debug.LogError("ChargeBar RectTransform is not assigned!");
            return;
        }

        // 设置蓄力条的大小
        chargeBar.sizeDelta = new Vector2(barWidth, barHeight);

        // 设置蓄力条的位置为屏幕底部中心
        chargeBar.anchorMin = new Vector2(0.5f, 0f); // 底部中心
        chargeBar.anchorMax = new Vector2(0.5f, 0f); // 底部中心
        chargeBar.pivot = new Vector2(0.5f, 0.5f);   // 中心点对齐
        chargeBar.anchoredPosition = new Vector2(0f, bottomOffset); // 距离底部的偏移
    }
}
