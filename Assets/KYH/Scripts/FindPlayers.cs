using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayers : MonoBehaviour
{
    // 맵에 있는 플레이어 태그를 가진 오브젝트를 리스트에 담는다.
    // Put Count()의 players를 세기 위한 클래스                          GPT 이용

    public List<GameObject> playerObjects = new List<GameObject>();

    void Start()
    {
        FindAllPlayerObjects();
    }

    void FindAllPlayerObjects()
    {
        // 씬에 있는 모든 게임 오브젝트를 가져옵니다.
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // 플레이어 태그를 가진 오브젝트만 리스트에 추가합니다.
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Player"))
            {
                playerObjects.Add(obj);
            }
        }
    }
}
