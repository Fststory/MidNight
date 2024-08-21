using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{
    public Text playerPointsText;
    public GameObject shopUICanvas;  // 상점 UI 전체를 담고 있는 Canvas
    public InventoryManager inventoryManager;

    private int playerPoints = 200;

    void Start()
    {
        UpdatePlayerPointsText();

        // 게임 시작 시 모든 Panel을 비활성화
        if (shopUICanvas != null)
        {
            SetActiveAllPanels(false);
        }
        else
        {
            Debug.LogError("Shop UI Canvas is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // K 키를 눌렀을 때 상점 UI의 모든 패널을 활성화/비활성화 상태로 전환
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleShopUI();
        }
    }

    private void ToggleShopUI()
    {
        if (shopUICanvas != null)
        {
            bool isAnyPanelActive = IsAnyPanelActive();
            SetActiveAllPanels(!isAnyPanelActive); // 모든 패널의 현재 상태를 반대로 전환
            Debug.Log("Shop UI toggled. Now active: " + !isAnyPanelActive);
        }
    }

    private void SetActiveAllPanels(bool isActive)
    {
        foreach (Transform child in shopUICanvas.transform)
        {
            if (child.gameObject.CompareTag("Panel"))
            {
                child.gameObject.SetActive(isActive);
            }
        }
    }

    private bool IsAnyPanelActive()
    {
        foreach (Transform child in shopUICanvas.transform)
        {
            if (child.gameObject.activeSelf && child.gameObject.CompareTag("Panel"))
            {
                return true;
            }
        }
        return false;
    }

    public bool OnBuyItem(ShopItem item)
    {
        // 이미 구매한 아이템인지 확인
        Button itemButton = item.GetComponent<Button>();
        if (itemButton != null && !itemButton.interactable)
        {
            Debug.LogWarning("This item has already been purchased.");
            return false; // 구매 실패
        }

        // 플레이어의 포인트가 아이템 가격보다 적은 경우 구매 불가
        if (playerPoints < item.price)
        {
            Debug.LogWarning("Not enough points to purchase this item.");
            return false; // 구매 실패
        }

        // 구매 가능할 때만 처리
        playerPoints -= item.price;
        UpdatePlayerPointsText();

        // 인벤토리에 아이템 추가
        inventoryManager.AddItem(item);

        // 아이템 아이콘의 투명도 조정 (구매된 것을 시각적으로 표시)
        Image itemIcon = item.GetComponent<Image>();
        if (itemIcon != null)
        {
            Color iconColor = itemIcon.color;
            iconColor.a = 0.5f; // 투명도 조정
            itemIcon.color = iconColor;
        }

        // 아이템 버튼 비활성화 (재구매 방지)
        if (itemButton != null)
        {
            itemButton.interactable = false;
        }

        return true; // 구매 성공
    }

    private void UpdatePlayerPointsText()
    {
        playerPointsText.text = $"Points: {playerPoints}";
    }
}