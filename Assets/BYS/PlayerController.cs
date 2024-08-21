using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20.0f; // 플레이어의 이동 속도
    public Transform cameraTransform; // 메인 카메라의 Transform
    public float horizontal;
    public float vertical;
    public Animator anim;
    private Rigidbody rb; // Rigidbody 컴포넌트를 참조

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
    }

    public void Update()
    {
        // 입력 값을 받음
        horizontal = Input.GetAxis("Horizontal"); // A/D 또는 좌/우 화살표
        vertical = Input.GetAxis("Vertical"); // W/S 또는 상/하 화살표

        // 카메라의 전방 및 우측 방향 계산
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // 수평면에서의 이동을 위해 y 값을 0으로 만듦
        forward.y = 0f;
        right.y = 0f;

        // 입력 값을 사용하여 이동 방향 계산
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // 플레이어를 이동시킴 (Rigidbody 사용)
        Vector3 moveVelocity = moveDirection * moveSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        // 플레이어가 움직이는 방향으로 회전하도록 설정
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
        AnimtionControll();
    }
    
    public void AnimtionControll()
    {
        if (horizontal >= 0.1f || vertical >= 0.1f)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isBool" , false);
        }
    }
}