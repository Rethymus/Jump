using UnityEngine;

public class PlayerSquashEffect : MonoBehaviour
{
    private Vector3 originalScale;
    public float maxSquashAmount = 0.5f; // ���ѹ������
    public float squashSpeed = 2f; // ѹ���ٶ�
    public float returnSpeed = 8f; // �ָ��ٶ�
    public float jumpStretchAmount = 1.3f; // ��Ծʱ��������

    private PlayerController playerController; // ����PlayerController
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
        // �������״̬
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            isSquashing = true;
            currentSquashTime += Time.deltaTime * squashSpeed;

            // ����ѹ������
            float squashRatio = Mathf.Clamp01(currentSquashTime);

            // Ӧ��ѹ��Ч��
            Vector3 newScale = originalScale;
            newScale.y = Mathf.Lerp(originalScale.y, originalScale.y * (1f - maxSquashAmount), squashRatio);
            // ����������Ʋ��䣬��΢����X��Z
            float xzScale = Mathf.Sqrt(1f / (1f - maxSquashAmount * squashRatio));
            newScale.x = originalScale.x * xzScale;
            newScale.z = originalScale.z * xzScale;

            transform.localScale = newScale;
        }
        // �ͷŰ���ʱ
        else if (isSquashing)
        {
            isSquashing = false;
            currentSquashTime = 0f;

            // ��Ծʱ������Ч��
            StartCoroutine(JumpStretchEffect());
        }
        // û�а���ʱ���𽥻ָ�ԭʼ��С
        else if (transform.localScale != originalScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * returnSpeed);
        }
    }

    private System.Collections.IEnumerator JumpStretchEffect()
    {
        // ������Ծ���������
        Vector3 stretchScale = originalScale;
        stretchScale.y *= jumpStretchAmount;
        stretchScale.x /= Mathf.Sqrt(jumpStretchAmount);
        stretchScale.z /= Mathf.Sqrt(jumpStretchAmount);

        // ����Ӧ������Ч��
        transform.localScale = stretchScale;

        // �ȴ�һС��ʱ��
        yield return new WaitForSeconds(0.1f);

        // ��֮��ļ�֡���𽥻ָ�������С
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