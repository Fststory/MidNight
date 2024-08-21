using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private GameObject currentActivePrefab;  // ���� Ȱ��ȭ�� �������� �����ϴ� ����

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
            // ������ Ȱ��ȭ�� �������� "hat" �±׸� ������ �ִٸ� ��Ȱ��ȭ
            if (currentActivePrefab != null && currentActivePrefab.CompareTag("Hat"))
            {
                currentActivePrefab.SetActive(false);
            }

            // ���ο� ������ ��������
            GameObject newPrefab = GetPrefabByName(itemName);

            // "hat" �±׸� ���� �����ո� Ȱ��ȭ
            if (newPrefab != null && newPrefab.CompareTag("Hat"))
            {
                newPrefab.SetActive(true);
                currentActivePrefab = newPrefab;  // ���� Ȱ��ȭ�� �������� ����
            }
        }
    }

    // ������ ����
    public void UnequipItem(string itemName)
    {
        if (inventoryManager != null)
        {
            // ���� Ȱ��ȭ�� �������� "Hat" �±׸� ������ �ִ��� Ȯ���ϰ� ��Ȱ��ȭ
            if (currentActivePrefab != null && currentActivePrefab.name == itemName && currentActivePrefab.CompareTag("Hat"))
            {
                currentActivePrefab.SetActive(false);
                currentActivePrefab = null; // ���� Ȱ��ȭ�� ������ ���� ����
            }

            // "hat" �±׸� ���� �����ո� ��Ȱ��ȭ
            GameObject prefab = GetPrefabByName(itemName);
            if (prefab != null && prefab.CompareTag("Hat"))
            {
                prefab.SetActive(false);
            }
        }
    }

    // ������ �̸����� ������ ��������
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
