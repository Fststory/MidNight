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

    private int playerPoints = 100;

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
        // S 키를 눌렀을 때 상점 UI의 모든 패널을 활성화/비활성화 상태로 전환
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

    public void OnBuyItem(ShopItem item)
    {
        if (playerPoints >= item.price)
        {
            playerPoints -= item.price;
            UpdatePlayerPointsText();

             // 인벤토리에 아이템 추가
             inventoryManager.AddItem(item);

        }
    }

    private void UpdatePlayerPointsText()
    {
        playerPointsText.text = $"Points: {playerPoints}";
    }
}