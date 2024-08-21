using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private InventoryManager inventoryManager;

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
            inventoryManager.SetPrefabActive(itemName, true);  // 아이템 프리팹 활성화
        }
    }

    // 아이템 해제
    public void UnequipItem(string itemName)
    {
        if (inventoryManager != null)
        {
            inventoryManager.SetPrefabActive(itemName, false);  // 아이템 프리팹 비활성화
        }
    }
}
