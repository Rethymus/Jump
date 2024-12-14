using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // ������ֵ
    public Text scoreText; // �������UI

    private void Start()
    {
        scoreText.text = score.ToString(); // ��ʼ������ʾ
    }

    public void AddScore(int amount)
    {
        score += amount; // ���ӷ���
        scoreText.text = score.ToString(); // ����UI��ʾ
    }
}