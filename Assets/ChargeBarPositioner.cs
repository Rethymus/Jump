using UnityEngine;

public class ChargeBarPositioner : MonoBehaviour
{
    [Header("����������")]
    public RectTransform chargeBar; // �������ı��� RectTransform
    public float barWidth = 300f;   // ���������
    public float barHeight = 30f;   // �������߶�
    public float bottomOffset = 50f; // ������������Ļ�ײ��ľ���

    private void Start()
    {
        PositionChargeBar();
    }

    private void Update()
    {
        // �����Ļ�ߴ���ܶ�̬�仯��������ÿ֡����λ��
        PositionChargeBar();
    }

    private void PositionChargeBar()
    {
        if (chargeBar == null)
        {
            Debug.LogError("ChargeBar RectTransform is not assigned!");
            return;
        }

        // �����������Ĵ�С
        chargeBar.sizeDelta = new Vector2(barWidth, barHeight);

        // ������������λ��Ϊ��Ļ�ײ�����
        chargeBar.anchorMin = new Vector2(0.5f, 0f); // �ײ�����
        chargeBar.anchorMax = new Vector2(0.5f, 0f); // �ײ�����
        chargeBar.pivot = new Vector2(0.5f, 0.5f);   // ���ĵ����
        chargeBar.anchoredPosition = new Vector2(0f, bottomOffset); // ����ײ���ƫ��
    }
}
