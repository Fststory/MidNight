using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float distance = 5.0f; // 플레이어와 카메라 사이의 거리
    public float height = 2.0f; // 카메라의 높이
    public float rotationSpeed = 5.0f; // 마우스로 회전하는 속도
    public LayerMask cameraCollision; // 카메라가 통과하지 못할 오브젝트의 레이어

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private Vector3 offset; // 플레이어와 카메라 사이의 거리와 방향

    void Start()
    {
        // 초기 오프셋 설정
        offset = new Vector3(0, height, -distance);
    }

    void Update()
    {
        // 마우스 입력에 따른 회전 값 업데이트
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, -20f, 80f); // 상하 회전 각도 제한
    }

    void LateUpdate()
    {
        HandleCameraPosition();
        HandleCameraCollision();
    }

    void HandleCameraPosition()
    {
        // 회전 및 위치 계산
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = rotation * offset;

        // 플레이어 위치에 기반한 카메라 위치 설정
        transform.position = player.position + direction;
        transform.LookAt(player.position + Vector3.up * height);
    }

    void HandleCameraCollision()
    {
        // 플레이어에서 카메라까지의 방향 벡터 계산
        Vector3 rayDir = transform.position - player.position;

        // 플레이어에서 카메라 방향으로 Ray 발사
        if (Physics.Raycast(player.position, rayDir, out RaycastHit hit, offset.magnitude, cameraCollision))
        {
            // 충돌 발생 시 카메라를 충돌 지점으로 이동
            transform.position = hit.point - rayDir.normalized * 0.5f;
        }
        
    }
}