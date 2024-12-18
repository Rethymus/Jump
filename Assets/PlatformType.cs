using UnityEngine;

public enum PlatformVariant
{
    Normal,      // 普通平台
    Fragile      // 脆弱平台
}

public class PlatformType : MonoBehaviour
{
    [Header("平台类型配置")]
    public PlatformVariant variant = PlatformVariant.Normal;

    [Header("脆弱平台参数")]
    public float standDuration = 3f;  // 玩家可站立时间
    public Color fragilePlatformColor = Color.yellow;  // 脆弱平台颜色提示

    private Renderer platformRenderer;
    private bool isPlayerOn = false;
    private float standTimer = 0f;
    private bool isPreparedToDestroy = false;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();

        // 如果是脆弱平台，修改颜色
        if (variant == PlatformVariant.Fragile)
        {
            platformRenderer.material.color = fragilePlatformColor;
        }
    }

    void Update()
    {
        // 仅脆弱平台处理计时和消失逻辑
        if (variant == PlatformVariant.Fragile && isPlayerOn)
        {
            standTimer += Time.deltaTime;

            // 3秒后准备销毁
            if (standTimer >= standDuration)
            {
                PrepareDestroy();
            }
        }
    }

    void PrepareDestroy()
    {
        if (!isPreparedToDestroy)
        {
            isPreparedToDestroy = true;
            // 可添加额外的视觉效果，如抖动、颜色变化等
            StartCoroutine(DestroyPlatform());
        }
    }

    System.Collections.IEnumerator DestroyPlatform()
    {
        // 可选：添加销毁前的动画效果
        float destroyTime = 0.5f;
        float elapsedTime = 0;

        Vector3 originalScale = transform.localScale;
        while (elapsedTime < destroyTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / destroyTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 检测玩家是否站在平台上
        if (collision.gameObject.CompareTag("Player") && variant == PlatformVariant.Fragile)
        {
            isPlayerOn = true;
            standTimer = 0f;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // 玩家离开平台
        if (collision.gameObject.CompareTag("Player") && variant == PlatformVariant.Fragile)
        {
            isPlayerOn = false;
        }
    }
}