using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // 플레이어 이동 속도

    void Update()
    {
        // 입력 처리
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 이동 벡터 계산
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Transform을 사용하여 위치 이동
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
