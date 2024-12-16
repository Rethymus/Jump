using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // 用于协程

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

    // 音频源组件和音频剪辑数组
    private AudioSource audioSource;
    public AudioClip[] jumpAudioClips;   // 多个跳跃音效
    public AudioClip fallAudioClip;      // 掉落音效

    private int currentJumpClipIndex = 0;  // 当前播放的跳跃音效索引
    private bool isLanding = false; // 用于标记是否处于成功着陆状态

    // 新增：最小有效跳跃时间
    public float minJumpTime = 0.2f; // 最小有效跳跃时间
    private bool isJumpSuccessful = false; // 跳跃是否成功的标志

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 获取或添加音频源组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        platformManager = (PlatformManager)Object.FindFirstObjectByType<PlatformManager>();
        if (platformManager == null)
        {
            Debug.LogError("PlatformManager not found!");
            return;
        }

        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned!");
            return;
        }

        powerUpVisual = (PowerUpVisual)Object.FindFirstObjectByType<PowerUpVisual>();
        if (powerUpVisual == null)
        {
            Debug.LogError("PowerUpVisual script instance not found!");
        }

        // 检查音频剪辑数组是否已赋值
        if (jumpAudioClips == null || jumpAudioClips.Length == 0)
        {
            Debug.LogError("Jump Audio Clips are not assigned!");
        }
        if (fallAudioClip == null)
        {
            Debug.LogError("Fall Audio Clip is not assigned!");
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
            isJumpSuccessful = false; // 重置跳跃成功标志
        }

        // 蓄力
        if (isCharging)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime); // 限制蓄力时间

            if (powerUpVisual != null)
            {
                powerUpVisual.chargeTime = chargeTime; // 实时同步到可视化组件
            }
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

        // 仅当蓄力时间足够时才认为跳跃有效
        isJumpSuccessful = chargeTime >= minJumpTime;

        // 应用跳跃力
        float jumpForce = chargeTime * jumpForceMultiplier;
        rb.linearVelocity = Vector3.zero; // 清除当前速度
        rb.AddForce(jumpDirection.normalized * jumpForce, ForceMode.Impulse);

        // 重置蓄力条
        if (powerUpVisual != null)
        {
            powerUpVisual.ResetChargeBar(); // 重置蓄力条状态
        }

        // 仅当跳跃有效并且玩家已成功跳跃时才播放音效
        if (isJumpSuccessful)
        {
            isLanding = true;  // 标记玩家成功跳跃
        }
    }

    // 延迟播放跳跃音效的协程
    private IEnumerator PlayJumpAudioWithDelay()
    {
        // 延迟0.1秒播放音效
        yield return new WaitForSeconds(0.1f);

        if (audioSource != null && jumpAudioClips != null && jumpAudioClips.Length > 0)
        {
            // 播放当前音频剪辑
            audioSource.PlayOneShot(jumpAudioClips[currentJumpClipIndex]);

            // 更新索引，确保下次播放不同的音效
            currentJumpClipIndex = (currentJumpClipIndex + 1) % jumpAudioClips.Length;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform") && rb != null)
        {
            Debug.Log("Landed on platform!");
            rb.linearVelocity = Vector3.zero; // 清除所有速度，防止滑动

            // 如果玩家已经离开过原平台且跳跃成功
            if (hasLeftStartPlatform && isJumpSuccessful)
            {
                // 启动协程，延迟播放跳跃音效
                if (isLanding)
                {
                    StartCoroutine(PlayJumpAudioWithDelay());
                    isLanding = false; // 重置着陆标志
                }

                AddScore(1); // 增加分数
            }
            else
            {
                // 如果是第一次着陆，标记为已经离开原平台，但不增加分数
                hasLeftStartPlatform = true;
            }
        }
        else
        {
            Debug.Log("Game Over!");

            // 播放掉落音效，确保音效播放完成后再进行下一步操作
            if (audioSource != null && fallAudioClip != null)
            {
                audioSource.PlayOneShot(fallAudioClip);
                StartCoroutine(WaitForFallAudio());
            }

            hasLeftStartPlatform = false; // 重置离开原平台标志
        }
    }

    // 等待掉落音效播放完成后再重新加载场景
    private IEnumerator WaitForFallAudio()
    {
        // 等待音效播放完毕
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // 音效播放完成后重新加载场景
        ReloadScene();
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

    // 单独的方法用于重新加载场景
    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
