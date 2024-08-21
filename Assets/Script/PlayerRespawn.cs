using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 _playerVelocity;
    [SerializeField] private Vector3 respawnPosition = new Vector3(0, 10, 0); // 기본 리스폰 위치

    // Start is called before the first frame update
    void Start()
    {
        _playerVelocity = Vector3.zero; // 초기화
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Respawn()
    {
        transform.position = respawnPosition;
        _playerVelocity = Vector3.zero;
    }

    private IEnumerator RespawnAsync()
    {
        yield return new WaitForSeconds(0.5f);
        Respawn();
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(RespawnAsync());
        }
    }
}
