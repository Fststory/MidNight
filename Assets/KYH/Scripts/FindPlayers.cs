using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayers : MonoBehaviour
{
    public List<GameObject> playerObjects = new List<GameObject>();

    void Start()
    {
        FindAllPlayerObjects();
    }

    void FindAllPlayerObjects()
    {
        // ���� �ִ� ��� ���� ������Ʈ�� �����ɴϴ�.
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // �÷��̾� �±׸� ���� ������Ʈ�� ����Ʈ�� �߰��մϴ�.
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Player"))
            {
                playerObjects.Add(obj);
            }
        }
    }
}
