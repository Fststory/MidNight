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
        public string itemName;    // 아이템 이름
        public Image icon;         // 아이템 아이콘을 표시하는 UI Image
        public Button button;      // 아이템을 클릭할 때 사용할 버튼
        public GameObject linkedPrefab;  // 활성화/비활성화할 게임 오브젝트 (프리팹)
    }

    public GameObject inventoryPanel;  // 인벤토리 UI 패널
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();  // 인벤토리 슬롯 리스트
    private Equipment equipment;  // Equipment 스크립트 참조

    void Start()
    {
        // 게임 시작 시 모든 슬롯의 아이콘과 버튼의 이미지 비활성화
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.icon.enabled = false;  // 아이콘 비활성화
            slot.button.interactable = false;  // 버튼 비활성화

            // 버튼의 이미지 컴포넌트 비활성화
            Image buttonImage = slot.button.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.enabled = false;
            }

            slot.button.onClick.AddListener(() => OnSlotClick(slot));  // 클릭 이벤트 추가
        }

        // 인벤토리 패널도 비활성화
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Inventory Panel is not assigned in the inspector!");
        }

        // Equipment 스크립트 참조 가져오기
        equipment = FindObjectOfType<Equipment>();
        if (equipment == null)
        {
            Debug.LogError("Equipment script not found!");
        }
    }

    void Update()
    {
        // I 키를 눌렀을 때 인벤토리 패널의 활성화/비활성화 상태를 전환
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

    // 인벤토리에 아이템 추가 (아이콘과 버튼 이미지 활성화)
    public void AddItem(ShopItem shopItem)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemName == shopItem.itemName)
            {
                // 아이콘 활성화
                if (!slot.icon.enabled)
                {
                    slot.icon.enabled = true;
                    slot.button.interactable = true;  // 버튼 활성화

                    // 버튼의 이미지 컴포넌트 활성화
                    Image buttonImage = slot.button.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        buttonImage.enabled = true;
                    }
                }

                // 스프라이트 설정
                slot.icon.sprite = shopItem.itemIcon;

                // 디버그 메시지
                Debug.Log($"{shopItem.itemName} was added to the inventory. Sprite: {slot.icon.sprite.name}");

                return;
            }
        }

        Debug.LogWarning($"No slot found for {shopItem.itemName}");
    }

    // 특정 아이템의 프리팹을 활성화/비활성화하는 메서드
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

    // 슬롯 클릭 시 아이템 장착 처리 및 프리팹 활성화/비활성화
    private void OnSlotClick(InventorySlot slot)
    {
        if (slot.linkedPrefab != null)
        {
            // 프리팹의 활성화 상태를 전환
            bool isActive = slot.linkedPrefab.activeSelf;
            slot.linkedPrefab.SetActive(!isActive);
            Debug.Log($"{slot.itemName} has been {(isActive ? "deactivated" : "activated")}.");

            // Equipment 스크립트에서 프리팹의 활성화 상태에 따라 장착 처리 (선택적)
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