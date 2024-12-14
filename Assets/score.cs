using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // 分数初值
    public Text scoreText; // 定义分数UI

    private void Start()
    {
        scoreText.text = score.ToString(); // 初始分数显示
    }

    public void AddScore(int amount)
    {
        score += amount; // 增加分数
        scoreText.text = score.ToString(); // 更新UI显示
    }
}