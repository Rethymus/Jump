using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;              // С���λ��
    public Vector3 offset = new Vector3(0, 5, -10); // �������ƫ����
    public float followSpeed = 5f;        // �����������ٶ�

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

        // ��ȡĿ��ƽ̨λ��
        Vector3 targetPlatformPosition = platformManager.GetTargetPlatformPosition();

        // �����������Ŀ��λ��
        Vector3 cameraTargetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, cameraTargetPosition, followSpeed * Time.deltaTime);

        // �������ʼ�ճ���С���Ŀ��ƽ̨֮��ķ���
        Vector3 lookAtPosition = (player.position + targetPlatformPosition) / 2;
        transform.LookAt(lookAtPosition);
    }
}