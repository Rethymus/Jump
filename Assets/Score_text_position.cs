using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ��Է���Text���

public class SetUIPosition : MonoBehaviour
{
    public RectTransform scoreTextRectTransform;
    public float topPadding = 0; // ��������
    public Text scoreText; // ����Text��������������С

    void Start()
    {
        if (scoreTextRectTransform != null)
        {
            // ���� Pivot Ϊ���Ͻ�
            scoreTextRectTransform.pivot = new Vector2(0, 1);

            // ���� Anchors Ϊ���Ͻ�
            scoreTextRectTransform.anchorMin = new Vector2(0, 1);
            scoreTextRectTransform.anchorMax = new Vector2(0, 1);

            // ���þ���λ��Ϊ��Ļ���Ͻǣ����Ƕ�������
            scoreTextRectTransform.anchoredPosition = new Vector2(0, -topPadding);

            // ���������СΪ50
            if (scoreText != null)
            {
                scoreText.fontSize = 50;
            }
        }
    }
}
