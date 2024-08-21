using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private InventoryManager inventoryManager;

    void Start()
    {
        // InventoryManager ��ũ��Ʈ ���� ��������
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found in the scene!");
        }
    }

    // ������ ����
    public void EquipItem(string itemName)
    {
        if (inventoryManager != null)
        {
            inventoryManager.SetPrefabActive(itemName, true);  // ������ ������ Ȱ��ȭ
        }
    }

    // ������ ����
    public void UnequipItem(string itemName)
    {
        if (inventoryManager != null)
        {
            inventoryManager.SetPrefabActive(itemName, false);  // ������ ������ ��Ȱ��ȭ
        }
    }
}
