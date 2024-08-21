using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 15f;
    public float turnSpeed = 720f;
    private Vector3 movement;
    private Vector3 desiredDirection;

    [Header("Camera")]
    private Transform cameraTransform;

    private Rigidbody rb;
    private Animator anim;

    [Header("Animations")]
    private int runID;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        runID = Animator.StringToHash("isRun");
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // 카메라 기준 이동 방향 계산
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        desiredDirection = cameraForward * movement.z + cameraRight * movement.x;

        // 이동 적용
        if (desiredDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + desiredDirection * moveSpeed * Time.fixedDeltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(desiredDirection);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        }

        // 애니메이션 설정
        anim.SetBool(runID, desiredDirection != Vector3.zero);
    }
}