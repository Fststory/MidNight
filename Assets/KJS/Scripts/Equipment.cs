using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private GameObject currentActiveHat;  // 현재 활성화된 "Hat" 태그를 가진 프리팹을 추적하는 변수
    private GameObject currentActiveRibbon;  // 현재 활성화된 "ribbon" 태그를 가진 프리팹을 추적하는 변수
    private GameObject currentActiveWing;
    void Start()
    {
        // InventoryManager 스크립트 참조 가져오기
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found in the scene!");
        }
    }

    // 아이템 장착
    public void EquipItem(string itemName)
    {
        if (inventoryManager != null)
        {
            GameObject newPrefab = GetPrefabByName(itemName);

            // "Hat" 태그를 가진 프리팹 처리
            if (newPrefab != null && newPrefab.CompareTag("Hat"))
            {
                // 이전에 활성화된 "Hat" 태그 프리팹이 있다면 비활성화
                if (currentActiveHat != null)
                {
                    currentActiveHat.SetActive(false);
                }

                // 새로운 "Hat" 태그 프리팹을 활성화
                newPrefab.SetActive(true);
                currentActiveHat = newPrefab;  // 현재 활성화된 프리팹을 갱신
            }

            // "ribbon" 태그를 가진 프리팹 처리
            if (newPrefab != null && newPrefab.CompareTag("Ribbon"))
            {
                // 이전에 활성화된 "ribbon" 태그 프리팹이 있다면 비활성화
                if (currentActiveRibbon != null)
                {
                    currentActiveRibbon.SetActive(false);
                }

                // 새로운 "ribbon" 태그 프리팹을 활성화
                newPrefab.SetActive(true);
                currentActiveRibbon = newPrefab;  // 현재 활성화된 프리팹을 갱신
            }

            // "ribbon" 태그를 가진 프리팹 처리
            if (newPrefab != null && newPrefab.CompareTag("Wing"))
            {
                // 이전에 활성화된 "ribbon" 태그 프리팹이 있다면 비활성화
                if (currentActiveRibbon != null)
                {
                    currentActiveRibbon.SetActive(false);
                }

                // 새로운 "ribbon" 태그 프리팹을 활성화
                newPrefab.SetActive(true);
                currentActiveRibbon = newPrefab;  // 현재 활성화된 프리팹을 갱신
            }
        }
    }

    // 아이템 해제
    public void UnequipItem(string itemName)
    {
        if (inventoryManager != null)
        {
            GameObject prefab = GetPrefabByName(itemName);

            // 현재 활성화된 "Hat" 태그 프리팹 비활성화
            if (prefab != null && prefab.CompareTag("Hat") && currentActiveHat == prefab)
            {
                currentActiveHat.SetActive(false);
                currentActiveHat = null;
            }

            // 현재 활성화된 "ribbon" 태그 프리팹 비활성화
            if (prefab != null && prefab.CompareTag("Ribbon") && currentActiveRibbon == prefab)
            {
                currentActiveRibbon.SetActive(false);
                currentActiveRibbon = null;
            }

            // 현재 활성화된 "ribbon" 태그 프리팹 비활성화
            if (prefab != null && prefab.CompareTag("Wing") && currentActiveRibbon == prefab)
            {
                currentActiveRibbon.SetActive(false);
                currentActiveRibbon = null;
            }
        }
    }

    // 아이템 이름으로 프리팹 가져오기
    private GameObject GetPrefabByName(string itemName)
    {
        foreach (var slot in inventoryManager.inventorySlots)
        {
            if (slot.itemName == itemName)
            {
                return slot.linkedPrefab;
            }
        }
        return null;
    }
}