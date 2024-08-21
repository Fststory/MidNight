using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private GameObject currentActiveHat;  // ���� Ȱ��ȭ�� "Hat" �±׸� ���� �������� �����ϴ� ����
    private GameObject currentActiveRibbon;  // ���� Ȱ��ȭ�� "ribbon" �±׸� ���� �������� �����ϴ� ����
    private GameObject currentActiveWing;
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
            GameObject newPrefab = GetPrefabByName(itemName);

            // "Hat" �±׸� ���� ������ ó��
            if (newPrefab != null && newPrefab.CompareTag("Hat"))
            {
                // ������ Ȱ��ȭ�� "Hat" �±� �������� �ִٸ� ��Ȱ��ȭ
                if (currentActiveHat != null)
                {
                    currentActiveHat.SetActive(false);
                }

                // ���ο� "Hat" �±� �������� Ȱ��ȭ
                newPrefab.SetActive(true);
                currentActiveHat = newPrefab;  // ���� Ȱ��ȭ�� �������� ����
            }

            // "ribbon" �±׸� ���� ������ ó��
            if (newPrefab != null && newPrefab.CompareTag("Ribbon"))
            {
                // ������ Ȱ��ȭ�� "ribbon" �±� �������� �ִٸ� ��Ȱ��ȭ
                if (currentActiveRibbon != null)
                {
                    currentActiveRibbon.SetActive(false);
                }

                // ���ο� "ribbon" �±� �������� Ȱ��ȭ
                newPrefab.SetActive(true);
                currentActiveRibbon = newPrefab;  // ���� Ȱ��ȭ�� �������� ����
            }

            // "ribbon" �±׸� ���� ������ ó��
            if (newPrefab != null && newPrefab.CompareTag("Wing"))
            {
                // ������ Ȱ��ȭ�� "ribbon" �±� �������� �ִٸ� ��Ȱ��ȭ
                if (currentActiveRibbon != null)
                {
                    currentActiveRibbon.SetActive(false);
                }

                // ���ο� "ribbon" �±� �������� Ȱ��ȭ
                newPrefab.SetActive(true);
                currentActiveRibbon = newPrefab;  // ���� Ȱ��ȭ�� �������� ����
            }
        }
    }

    // ������ ����
    public void UnequipItem(string itemName)
    {
        if (inventoryManager != null)
        {
            GameObject prefab = GetPrefabByName(itemName);

            // ���� Ȱ��ȭ�� "Hat" �±� ������ ��Ȱ��ȭ
            if (prefab != null && prefab.CompareTag("Hat") && currentActiveHat == prefab)
            {
                currentActiveHat.SetActive(false);
                currentActiveHat = null;
            }

            // ���� Ȱ��ȭ�� "ribbon" �±� ������ ��Ȱ��ȭ
            if (prefab != null && prefab.CompareTag("Ribbon") && currentActiveRibbon == prefab)
            {
                currentActiveRibbon.SetActive(false);
                currentActiveRibbon = null;
            }

            // ���� Ȱ��ȭ�� "ribbon" �±� ������ ��Ȱ��ȭ
            if (prefab != null && prefab.CompareTag("Wing") && currentActiveRibbon == prefab)
            {
                currentActiveRibbon.SetActive(false);
                currentActiveRibbon = null;
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