using UnityEngine;
using UnityEngine.UI;

public class PowerUpVisual : MonoBehaviour
{
    public Image powerImage; // ������ʾ�������ȵ�Image
    public float chargeTime = 0f; // ����ʱ��
    public float maxChargeTime = 2f; // �������ʱ��

    void Update()
    {
        if (powerImage != null)
        {
            powerImage.fillAmount = chargeTime / maxChargeTime; // ���������
        }
    }

    public void SetChargeTime(float time)
    {
        chargeTime = time;
    }
}