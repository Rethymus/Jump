using UnityEngine;

public enum PlatformVariant
{
    Normal,      // ��ͨƽ̨
    Fragile      // ����ƽ̨
}

public class PlatformType : MonoBehaviour
{
    [Header("ƽ̨��������")]
    public PlatformVariant variant = PlatformVariant.Normal;

    [Header("����ƽ̨����")]
    public float standDuration = 3f;  // ��ҿ�վ��ʱ��
    public Color fragilePlatformColor = Color.yellow;  // ����ƽ̨��ɫ��ʾ

    private Renderer platformRenderer;
    private bool isPlayerOn = false;
    private float standTimer = 0f;
    private bool isPreparedToDestroy = false;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();

        // ����Ǵ���ƽ̨���޸���ɫ
        if (variant == PlatformVariant.Fragile)
        {
            platformRenderer.material.color = fragilePlatformColor;
        }
    }

    void Update()
    {
        // ������ƽ̨�����ʱ����ʧ�߼�
        if (variant == PlatformVariant.Fragile && isPlayerOn)
        {
            standTimer += Time.deltaTime;

            // 3���׼������
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
            // ����Ӷ�����Ӿ�Ч�����綶������ɫ�仯��
            StartCoroutine(DestroyPlatform());
        }
    }

    System.Collections.IEnumerator DestroyPlatform()
    {
        // ��ѡ���������ǰ�Ķ���Ч��
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
        // �������Ƿ�վ��ƽ̨��
        if (collision.gameObject.CompareTag("Player") && variant == PlatformVariant.Fragile)
        {
            isPlayerOn = true;
            standTimer = 0f;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // ����뿪ƽ̨
        if (collision.gameObject.CompareTag("Player") && variant == PlatformVariant.Fragile)
        {
            isPlayerOn = false;
        }
    }
}