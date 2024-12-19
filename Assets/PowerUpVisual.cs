using UnityEngine;
using UnityEngine.UI;

public class PowerUpVisual : MonoBehaviour
{
    [Header("����������")]
    public Image chargeFillImage;     // ���������ͼ��
    public RectTransform chargeBarBG; // ����������
    public Text percentageText;       // �ٷֱ��ı�

    [Header("��������")]
    public float maxChargeTime = 2f;  // �������ʱ��

    [Header("��ɫ����")]
    public Color startColor = Color.green;    // ��ʼ��ɫ
    public Color endColor = Color.red;        // ������ɫ

    [HideInInspector]
    public float chargeTime = 0f;     // ��ǰ����ʱ��

    private void Update()
    {
        UpdateChargeBar();
    }

    private void UpdateChargeBar()
    {
        // ���������ٷֱ�
        float chargePercentage = Mathf.Clamp01(chargeTime / maxChargeTime);

        // �������������
        if (chargeFillImage != null)
        {
            // ����������
            chargeFillImage.fillAmount = chargePercentage;

            // ��ɫ����
            chargeFillImage.color = Color.Lerp(startColor, endColor, chargePercentage);

            Debug.Log($"Charge Fill Percentage: {chargePercentage}, Color: {chargeFillImage.color}");
        }

        // ���°ٷֱ��ı�
        if (percentageText != null)
        {
            percentageText.text = $"{Mathf.RoundToInt(chargePercentage * 100)}%";
        }

        // ����������������ʾ
        if (chargeBarBG != null)
        {
            chargeBarBG.gameObject.SetActive(chargeTime > 0);
        }

        // ��̬����Ч��
        if (chargeFillImage != null)
        {
            float scale = 1f + (chargePercentage * 0.2f);
            chargeFillImage.transform.localScale = Vector3.one * scale;
        }
    }


    // ����������
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
