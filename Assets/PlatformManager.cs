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

    void Start()
    {
        // ��ʼ����һ��ƽ̨��λ�õ���ҽ���
        Vector3 initialPlatformPosition = new Vector3(player.position.x, player.position.y - 0.5f, player.position.z);
        Instantiate(platformPrefab, initialPlatformPosition, Quaternion.identity);
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

        // ȷ��ƽ̨�߶�һ��
        Vector3 nextPosition = lastPlatformPosition + offset;
        nextPosition.y = lastPlatformPosition.y;

        // ������ƽ̨������λ��
        Instantiate(platformPrefab, nextPosition, Quaternion.identity);
        lastPlatformPosition = nextPosition;        // �������һ��ƽ̨λ��
        targetPlatformPosition = nextPosition;      // ����Ŀ��ƽ̨λ��
        isPlatformGenerated = true; // ���Ϊ���ɵ���ƽ̨
    }

    public Vector3 GetTargetPlatformPosition()
    {
        return targetPlatformPosition;
    }
}
