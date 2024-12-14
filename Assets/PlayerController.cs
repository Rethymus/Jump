using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isCharging = false;
    private float chargeTime = 0f;
    public float jumpForceMultiplier = 10f; // 蓄力倍数
    public float maxChargeTime = 2f;        // 最大蓄力时间
    private PlatformManager platformManager;
    private int score = 0; // 得分变量
    public Text scoreText; // 得分文本引用
    private bool hasLeftStartPlatform = false; // 玩家是否已经离开原平台
    private PowerUpVisual powerUpVisual; // 蓄力可视化引用

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        platformManager = (PlatformManager)Object.FindFirstObjectByType<PlatformManager>();
        if (platformManager == null)
        {
            Debug.LogError("PlatformManager not found!");
            return; // 退出方法，避免进一步操作
        }
        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned!");
            return; // 退出方法，避免进一步操作
        }
        powerUpVisual = (PowerUpVisual)Object.FindFirstObjectByType<PowerUpVisual>();
        if (powerUpVisual == null)
        {
            Debug.LogError("PowerUpVisual script instance not found!");
        }
        string s = "Score: ";
        scoreText.text = s + score.ToString(); // 初始分数显示
    }

    void Update()
    {
        // 按下空格开始蓄力
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            chargeTime = 0f;
        }

        // 蓄力
        if (isCharging)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime); // 限制蓄力时间
        }

        // 松开空格跳跃
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isCharging = false;
            Jump();
        }
    }

    void Jump()
    {
        if (rb == null || platformManager == null) return;

        // 获取目标平台位置
        Vector3 targetPosition = platformManager.GetTargetPlatformPosition();

        // 计算跳跃方向（仅锁定 X 或 Z 轴）
        Vector3 jumpDirection = Vector3.zero;
        if (Mathf.Abs(targetPosition.x - transform.position.x) > Mathf.Abs(targetPosition.z - transform.position.z))
        {
            // 跳跃方向沿 X 轴
            jumpDirection = new Vector3(Mathf.Sign(targetPosition.x - transform.position.x), 1, 0);
        }
        else
        {
            // 跳跃方向沿 Z 轴
            jumpDirection = new Vector3(0, 1, Mathf.Sign(targetPosition.z - transform.position.z));
        }

        // 应用跳跃力
        float jumpForce = chargeTime * jumpForceMultiplier;
        rb.linearVelocity = Vector3.zero; // 清除当前速度
        rb.AddForce(jumpDirection.normalized * jumpForce, ForceMode.Impulse);

        // 同步蓄力进度
        if (powerUpVisual != null)
        {
            powerUpVisual.chargeTime = chargeTime; // 更新蓄力时间
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform") && rb != null)
        {
            Debug.Log("Landed on platform!");
            rb.linearVelocity = Vector3.zero; // 清除所有速度，防止滑动

            // 如果玩家已经离开过原平台
            if (hasLeftStartPlatform)
            {
                AddScore(1); // 增加分数
            }
            else
            {
                // 如果是第一次着陆，标记为已经离开原平台，但不增加分数
                hasLeftStartPlatform = true; // 玩家已经离开原平台
            }
        }
        else
        {
            Debug.Log("Game Over!");
            hasLeftStartPlatform = false; // 重置离开原平台标志
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    // 增加分数的方法
    public void AddScore(int amount)
    {
        score += amount; // 增加分数
        if (scoreText != null)
        {
            string s = "Score: ";
            scoreText.text = s + score.ToString();
        }
        else
        {
            Debug.LogError("Score Text is not assigned!");
        }
    }
}