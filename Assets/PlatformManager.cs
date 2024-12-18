using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab;    // ƽ̨Ԥ����
    public Transform player;             // ��Ҷ���
    private Vector3 lastPlatformPosition; // ��¼���һ�����ɵ�ƽ̨λ��
    public Vector3 targetPlatformPosition; // ��ǰĿ��ƽ̨λ��
    private bool isPlatformGenerated = false; // ���ڿ����Ƿ�������ƽ̨
    public float minDistance = 2.5f;        // ƽ̨���ɵ���С����
    public float maxDistance = 4.5f;        // ƽ̨���ɵ�������

    [Header("����ƽ̨��������")]
    [Range(0, 1)]
    public float fragilePlatformChance = 0.3f;  // ����ƽ̨���ɸ���
    public int fragilePlatformConsecutiveLimit = 2;  // �������ɴ���ƽ̨���������
    private int currentFragilePlatformCount = 0;  // ��ǰ�������ɵĴ���ƽ̨����

    void Start()
    {
        // ��ʼ����һ��ƽ̨��λ�õ���ҽ���
        Vector3 initialPlatformPosition = new Vector3(player.position.x, player.position.y - 0.5f, player.position.z);
        GameObject initialPlatform = Instantiate(platformPrefab, initialPlatformPosition, Quaternion.identity);

        // ȷ����һ��ƽ̨����ͨƽ̨
        PlatformType platformTypeComponent = initialPlatform.GetComponent<PlatformType>();
        if (platformTypeComponent != null)
        {
            platformTypeComponent.variant = PlatformVariant.Normal;
        }

        lastPlatformPosition = initialPlatformPosition;
        targetPlatformPosition = initialPlatformPosition; // ��ʼĿ��ƽ̨
        isPlatformGenerated = true; // ���Ϊ��ʼ���ɵ�ƽ̨
    }

    void Update()
    {
        // �������Ƿ�ӽ���ǰĿ��ƽ̨������ӽ���������һ��ƽ̨
        if (isPlatformGenerated && Vector3.Distance(player.position, targetPlatformPosition) < 1.5f)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // �������ƽ̨����0 ��ʾ X �᷽��1 ��ʾ Z �᷽��
        int direction = Random.Range(0, 2);
        float distance = Random.Range(minDistance, maxDistance); // ������룬��֤�� minDistance �� maxDistance ֮��
        Vector3 offset = Vector3.zero;
        if (direction == 0)
        {
            offset = new Vector3(distance, 0, 0); // �� X ������
        }
        else
        {
            offset = new Vector3(0, 0, distance); // �� Z ������
        }

        // ȷ���Ƿ����ɴ���ƽ̨
        bool isFragilePlatform = ShouldSpawnFragilePlatform();

        // ȷ��ƽ̨�߶�һ��
        Vector3 nextPosition = lastPlatformPosition + offset;
        nextPosition.y = lastPlatformPosition.y;

        // ����ƽ̨
        GameObject newPlatform = Instantiate(platformPrefab, nextPosition, Quaternion.identity);

        // ����ƽ̨����
        PlatformType platformTypeComponent = newPlatform.GetComponent<PlatformType>();
        if (platformTypeComponent != null)
        {
            platformTypeComponent.variant = isFragilePlatform
                ? PlatformVariant.Fragile
                : PlatformVariant.Normal;
        }

        // ����״̬
        lastPlatformPosition = nextPosition;
        targetPlatformPosition = nextPosition;
        isPlatformGenerated = true;
    }

    bool ShouldSpawnFragilePlatform()
    {
        // ���ݸ��ʺ������������ƾ����Ƿ����ɴ���ƽ̨
        bool shouldSpawn = Random.value <= fragilePlatformChance &&
                           currentFragilePlatformCount < fragilePlatformConsecutiveLimit;

        if (shouldSpawn)
        {
            currentFragilePlatformCount++;
        }
        else
        {
            currentFragilePlatformCount = 0;
        }

        return shouldSpawn;
    }

    public Vector3 GetTargetPlatformPosition()
    {
        return targetPlatformPosition;
    }
}