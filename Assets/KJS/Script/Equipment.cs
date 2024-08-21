using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private GameObject currentActivePrefab;  // 현재 활성화된 프리팹을 추적하는 변수

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
            // 이전에 활성화된 프리팹이 "hat" 태그를 가지고 있다면 비활성화
            if (currentActivePrefab != null && currentActivePrefab.CompareTag("Hat"))
            {
                currentActivePrefab.SetActive(false);
            }

            // 새로운 프리팹 가져오기
            GameObject newPrefab = GetPrefabByName(itemName);

            // "hat" 태그를 가진 프리팹만 활성화
            if (newPrefab != null && newPrefab.CompareTag("Hat"))
            {
                newPrefab.SetActive(true);
                currentActivePrefab = newPrefab;  // 현재 활성화된 프리팹을 갱신
            }
        }
    }

    // 아이템 해제
    public void UnequipItem(string itemName)
    {
        if (inventoryManager != null)
        {
            // 현재 활성화된 프리팹이 "Hat" 태그를 가지고 있는지 확인하고 비활성화
            if (currentActivePrefab != null && currentActivePrefab.name == itemName && currentActivePrefab.CompareTag("Hat"))
            {
                currentActivePrefab.SetActive(false);
                currentActivePrefab = null; // 현재 활성화된 프리팹 추적 해제
            }

            // "hat" 태그를 가진 프리팹만 비활성화
            GameObject prefab = GetPrefabByName(itemName);
            if (prefab != null && prefab.CompareTag("Hat"))
            {
                prefab.SetActive(false);
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
