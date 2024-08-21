using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public string itemName;    // ������ �̸�
        public Image icon;         // ������ �������� ǥ���ϴ� UI Image
        public Button button;      // �������� Ŭ���� �� ����� ��ư
        public GameObject linkedPrefab;  // Ȱ��ȭ/��Ȱ��ȭ�� ���� ������Ʈ (������)
    }

    public GameObject inventoryPanel;  // �κ��丮 UI �г�
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();  // �κ��丮 ���� ����Ʈ
    private Equipment equipment;  // Equipment ��ũ��Ʈ ����

    void Start()
    {
        // ���� ���� �� ��� ������ �����ܰ� ��ư�� �̹��� ��Ȱ��ȭ
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.icon.enabled = false;  // ������ ��Ȱ��ȭ
            slot.button.interactable = false;  // ��ư ��Ȱ��ȭ

            // ��ư�� �̹��� ������Ʈ ��Ȱ��ȭ
            Image buttonImage = slot.button.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.enabled = false;
            }

            slot.button.onClick.AddListener(() => OnSlotClick(slot));  // Ŭ�� �̺�Ʈ �߰�
        }

        // �κ��丮 �гε� ��Ȱ��ȭ
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Inventory Panel is not assigned in the inspector!");
        }

        // Equipment ��ũ��Ʈ ���� ��������
        equipment = FindObjectOfType<Equipment>();
        if (equipment == null)
        {
            Debug.LogError("Equipment script not found!");
        }
    }

    void Update()
    {
        // I Ű�� ������ �� �κ��丮 �г��� Ȱ��ȭ/��Ȱ��ȭ ���¸� ��ȯ
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryUI();
        }
    }

    private void ToggleInventoryUI()
    {
        if (inventoryPanel != null)
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
            Debug.Log("Inventory UI toggled. Now active: " + !isActive);
        }
        else
        {
            Debug.LogError("Inventory Panel is not assigned in the inspector!");
        }
    }

    // �κ��丮�� ������ �߰� (�����ܰ� ��ư �̹��� Ȱ��ȭ)
    public void AddItem(ShopItem shopItem)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemName == shopItem.itemName)
            {
                // ������ Ȱ��ȭ
                if (!slot.icon.enabled)
                {
                    slot.icon.enabled = true;
                    slot.button.interactable = true;  // ��ư Ȱ��ȭ

                    // ��ư�� �̹��� ������Ʈ Ȱ��ȭ
                    Image buttonImage = slot.button.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        buttonImage.enabled = true;
                    }
                }

                // ��������Ʈ ����
                slot.icon.sprite = shopItem.itemIcon;

                // ����� �޽���
                Debug.Log($"{shopItem.itemName} was added to the inventory. Sprite: {slot.icon.sprite.name}");

                return;
            }
        }

        Debug.LogWarning($"No slot found for {shopItem.itemName}");
    }

    // Ư�� �������� �������� Ȱ��ȭ/��Ȱ��ȭ�ϴ� �޼���
    public void SetPrefabActive(string itemName, bool isActive)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemName == itemName)
            {
                if (slot.linkedPrefab != null)
                {
                    slot.linkedPrefab.SetActive(isActive);
                    Debug.Log($"{itemName} is now {(isActive ? "activated" : "deactivated")}");
                }
                else
                {
                    Debug.LogWarning($"No linked prefab found for the item: {itemName}");
                }
                return;
            }
        }
        Debug.LogWarning($"No slot found for item: {itemName}");
    }

    // ���� Ŭ�� �� ������ ���� ó�� �� ������ Ȱ��ȭ/��Ȱ��ȭ
    private void OnSlotClick(InventorySlot slot)
    {
        if (slot.linkedPrefab != null)
        {
            // �������� Ȱ��ȭ ���¸� ��ȯ
            bool isActive = slot.linkedPrefab.activeSelf;
            slot.linkedPrefab.SetActive(!isActive);
            Debug.Log($"{slot.itemName} has been {(isActive ? "deactivated" : "activated")}.");

            // Equipment ��ũ��Ʈ���� �������� Ȱ��ȭ ���¿� ���� ���� ó�� (������)
            if (equipment != null)
            {
                if (isActive)
                {
                    equipment.UnequipItem(slot.itemName);
                }
                else
                {
                    equipment.EquipItem(slot.itemName);
                }
            }
        }
        else
        {
            Debug.LogWarning($"No linked prefab found for the item: {slot.itemName}");
        }
    }
}