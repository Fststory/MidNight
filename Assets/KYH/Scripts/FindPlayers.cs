using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayers : MonoBehaviour
{
    // �ʿ� �ִ� �÷��̾� �±׸� ���� ������Ʈ�� ����Ʈ�� ��´�.
    // Put Count()�� players�� ���� ���� Ŭ����                          GPT �̿�

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
