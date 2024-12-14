using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab;    // 平台预制体
    public Transform player;             // 玩家对象
    private Vector3 lastPlatformPosition; // 记录最后一个生成的平台位置
    public Vector3 targetPlatformPosition; // 当前目标平台位置
    private bool isPlatformGenerated = false; // 用于控制是否生成新平台
    public float minDistance = 2.5f;        // 平台生成的最小距离
    public float maxDistance = 4.5f;        // 平台生成的最大距离

    void Start()
    {
        // 初始化第一个平台的位置到玩家脚下
        Vector3 initialPlatformPosition = new Vector3(player.position.x, player.position.y - 0.5f, player.position.z);
        Instantiate(platformPrefab, initialPlatformPosition, Quaternion.identity);
        lastPlatformPosition = initialPlatformPosition;
        targetPlatformPosition = initialPlatformPosition; // 初始目标平台
        isPlatformGenerated = true; // 标记为初始生成的平台
    }

    void Update()
    {
        // 检测玩家是否接近当前目标平台，如果接近则生成下一个平台
        if (isPlatformGenerated && Vector3.Distance(player.position, targetPlatformPosition) < 1.5f)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // 随机生成平台方向：0 表示 X 轴方向，1 表示 Z 轴方向
        int direction = Random.Range(0, 2);
        float distance = Random.Range(minDistance, maxDistance); // 随机距离，保证在 minDistance 和 maxDistance 之间

        Vector3 offset = Vector3.zero;
        if (direction == 0)
        {
            offset = new Vector3(distance, 0, 0); // 沿 X 轴生成
        }
        else
        {
            offset = new Vector3(0, 0, distance); // 沿 Z 轴生成
        }

        // 确保平台高度一致
        Vector3 nextPosition = lastPlatformPosition + offset;
        nextPosition.y = lastPlatformPosition.y;

        // 创建新平台并更新位置
        Instantiate(platformPrefab, nextPosition, Quaternion.identity);
        lastPlatformPosition = nextPosition;        // 更新最后一个平台位置
        targetPlatformPosition = nextPosition;      // 更新目标平台位置
        isPlatformGenerated = true; // 标记为生成的新平台
    }

    public Vector3 GetTargetPlatformPosition()
    {
        return targetPlatformPosition;
    }
}
