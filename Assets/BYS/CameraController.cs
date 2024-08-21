using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, 5, -10);

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform);
    }
}