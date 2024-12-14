using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间以访问Text组件

public class SetUIPosition : MonoBehaviour
{
    public RectTransform scoreTextRectTransform;
    public float topPadding = 0; // 顶部留白
    public Text scoreText; // 引用Text组件来设置字体大小

    void Start()
    {
        if (scoreTextRectTransform != null)
        {
            // 设置 Pivot 为左上角
            scoreTextRectTransform.pivot = new Vector2(0, 1);

            // 设置 Anchors 为左上角
            scoreTextRectTransform.anchorMin = new Vector2(0, 1);
            scoreTextRectTransform.anchorMax = new Vector2(0, 1);

            // 设置绝对位置为屏幕左上角，考虑顶部留白
            scoreTextRectTransform.anchoredPosition = new Vector2(0, -topPadding);

            // 设置字体大小为50
            if (scoreText != null)
            {
                scoreText.fontSize = 50;
            }
        }
    }
}
