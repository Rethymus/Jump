using UnityEngine;

public class PlayerSquashEffect : MonoBehaviour
{
    private Vector3 originalScale;
    public float maxSquashAmount = 0.5f; // 最大压缩比例
    public float squashSpeed = 2f; // 压缩速度
    public float returnSpeed = 8f; // 恢复速度
    public float jumpStretchAmount = 1.3f; // 跳跃时的拉伸量

    private PlayerController playerController; // 引用PlayerController
    private bool isSquashing = false;
    private float currentSquashTime = 0f;

    void Start()
    {
        originalScale = transform.localScale;
        playerController = GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on the same object!");
        }
    }

    void Update()
    {
        // 检测蓄力状态
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            isSquashing = true;
            currentSquashTime += Time.deltaTime * squashSpeed;

            // 计算压缩比例
            float squashRatio = Mathf.Clamp01(currentSquashTime);

            // 应用压缩效果
            Vector3 newScale = originalScale;
            newScale.y = Mathf.Lerp(originalScale.y, originalScale.y * (1f - maxSquashAmount), squashRatio);
            // 保持体积近似不变，略微增加X和Z
            float xzScale = Mathf.Sqrt(1f / (1f - maxSquashAmount * squashRatio));
            newScale.x = originalScale.x * xzScale;
            newScale.z = originalScale.z * xzScale;

            transform.localScale = newScale;
        }
        // 释放按键时
        else if (isSquashing)
        {
            isSquashing = false;
            currentSquashTime = 0f;

            // 跳跃时的拉伸效果
            StartCoroutine(JumpStretchEffect());
        }
        // 没有按键时，逐渐恢复原始大小
        else if (transform.localScale != originalScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * returnSpeed);
        }
    }

    private System.Collections.IEnumerator JumpStretchEffect()
    {
        // 计算跳跃方向的拉伸
        Vector3 stretchScale = originalScale;
        stretchScale.y *= jumpStretchAmount;
        stretchScale.x /= Mathf.Sqrt(jumpStretchAmount);
        stretchScale.z /= Mathf.Sqrt(jumpStretchAmount);

        // 快速应用拉伸效果
        transform.localScale = stretchScale;

        // 等待一小段时间
        yield return new WaitForSeconds(0.1f);

        // 在之后的几帧中逐渐恢复正常大小
        float elapsed = 0f;
        float duration = 0.3f;
        Vector3 startScale = transform.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            transform.localScale = Vector3.Lerp(startScale, originalScale, progress);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}