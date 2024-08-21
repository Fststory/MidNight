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
    public GameObject shopUICanvas;  // ���� UI ��ü�� ��� �ִ� Canvas
    public InventoryManager inventoryManager;

    private int playerPoints = 100;

    void Start()
    {
        UpdatePlayerPointsText();

        // ���� ���� �� ��� Panel�� ��Ȱ��ȭ
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
        // S Ű�� ������ �� ���� UI�� ��� �г��� Ȱ��ȭ/��Ȱ��ȭ ���·� ��ȯ
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
            SetActiveAllPanels(!isAnyPanelActive); // ��� �г��� ���� ���¸� �ݴ�� ��ȯ
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

             // �κ��丮�� ������ �߰�
             inventoryManager.AddItem(item);

        }
    }

    private void UpdatePlayerPointsText()
    {
        playerPointsText.text = $"Points: {playerPoints}";
    }
}