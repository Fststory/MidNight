using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 플레이어의 이동 속도
    public Transform cameraTransform; // 메인 카메라의 Transform

    void Update()
    {
        // 입력 값을 받음
        float horizontal = Input.GetAxis("Horizontal"); // A/D 또는 좌/우 화살표
        float vertical = Input.GetAxis("Vertical"); // W/S 또는 상/하 화살표

        // 카메라의 전방 및 우측 방향 계산
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // 수평면에서의 이동을 위해 y 값을 0으로 만듦
        forward.y = 0f;
        right.y = 0f;

        // 입력 값을 사용하여 이동 방향 계산
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // 플레이어를 이동시킴
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 플레이어가 움직이는 방향으로 회전하도록 설정
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }
} 