using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;              // 小球的位置
    public Vector3 offset = new Vector3(0, 5, -10); // 摄像机的偏移量
    public float followSpeed = 5f;        // 摄像机跟随的速度

    private PlatformManager platformManager;

    void Start()
    {
        platformManager = (PlatformManager)Object.FindFirstObjectByType<PlatformManager>();
        if (platformManager == null)
        {
            Debug.LogError("PlatformManager not found!");
        }
    }

    void LateUpdate()
    {
        if (player == null || platformManager == null) return;

        // 获取目标平台位置
        Vector3 targetPlatformPosition = platformManager.GetTargetPlatformPosition();

        // 计算摄像机的目标位置
        Vector3 cameraTargetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, cameraTargetPosition, followSpeed * Time.deltaTime);

        // 让摄像机始终朝向小球和目标平台之间的方向
        Vector3 lookAtPosition = (player.position + targetPlatformPosition) / 2;
        transform.LookAt(lookAtPosition);
    }
}