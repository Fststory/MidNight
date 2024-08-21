using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTransform;  // 플레이어의 Transform
    public Vector3 offset = new Vector3(0, 5, -10);  // 카메라의 위치 오프셋
    public float followSpeed = 10f;  // 카메라가 따라가는 속도
    public float mouseSensitivity = 100f;  // 마우스 감도
    public float distanceFromPlayer = 10f;  // 플레이어로부터 카메라까지의 거리
    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        // 초기 카메라 회전 값 설정
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // 마우스 입력을 기반으로 카메라 회전값 계산
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -45f, 45f);  // 피치 값 제한 (카메라가 너무 많이 회전하지 않도록)

        // 카메라가 플레이어를 중심으로 회전하도록 설정
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        
        // 카메라의 위치를 플레이어의 위치로부터 계산
        Vector3 desiredPosition = playerTransform.position - rotation * Vector3.forward * distanceFromPlayer + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // 카메라가 플레이어를 바라보도록 설정
        transform.LookAt(playerTransform.position + Vector3.up * 2f);
    }
}