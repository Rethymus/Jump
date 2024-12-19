using UnityEngine;
using UnityEngine.UI;

public class PowerUpVisual : MonoBehaviour
{
    [Header("蓄力条配置")]
    public Image chargeFillImage;     // 蓄力条填充图像
    public RectTransform chargeBarBG; // 蓄力条背景
    public Text percentageText;       // 百分比文本

    [Header("蓄力参数")]
    public float maxChargeTime = 2f;  // 最大蓄力时间

    [Header("颜色配置")]
    public Color startColor = Color.green;    // 开始颜色
    public Color endColor = Color.red;        // 结束颜色

    [HideInInspector]
    public float chargeTime = 0f;     // 当前蓄力时间

    private void Update()
    {
        UpdateChargeBar();
    }

    private void UpdateChargeBar()
    {
        // 计算蓄力百分比
        float chargePercentage = Mathf.Clamp01(chargeTime / maxChargeTime);

        // 更新蓄力条填充
        if (chargeFillImage != null)
        {
            // 设置填充比例
            chargeFillImage.fillAmount = chargePercentage;

            // 颜色渐变
            chargeFillImage.color = Color.Lerp(startColor, endColor, chargePercentage);

            Debug.Log($"Charge Fill Percentage: {chargePercentage}, Color: {chargeFillImage.color}");
        }

        // 更新百分比文本
        if (percentageText != null)
        {
            percentageText.text = $"{Mathf.RoundToInt(chargePercentage * 100)}%";
        }

        // 控制蓄力条背景显示
        if (chargeBarBG != null)
        {
            chargeBarBG.gameObject.SetActive(chargeTime > 0);
        }

        // 动态缩放效果
        if (chargeFillImage != null)
        {
            float scale = 1f + (chargePercentage * 0.2f);
            chargeFillImage.transform.localScale = Vector3.one * scale;
        }
    }


    // 重置蓄力条
    public void ResetChargeBar()
    {
        chargeTime = 0f;
        if (chargeFillImage != null)
        {
            chargeFillImage.fillAmount = 0;
            chargeFillImage.transform.localScale = Vector3.one;
        }
    }
}
