using UnityEngine;
using UnityEngine.UI;

public class PowerUpVisual : MonoBehaviour
{
    public Image powerImage; // 用于显示蓄力进度的Image
    public float chargeTime = 0f; // 蓄力时间
    public float maxChargeTime = 2f; // 最大蓄力时间

    void Update()
    {
        if (powerImage != null)
        {
            powerImage.fillAmount = chargeTime / maxChargeTime; // 更新填充量
        }
    }

    public void SetChargeTime(float time)
    {
        chargeTime = time;
    }
}