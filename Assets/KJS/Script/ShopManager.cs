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
    public Text insufficientPointsText; // ����Ʈ ���� �޽����� ǥ���� �ؽ�Ʈ UI
    public GameObject shopUICanvas;  // ���� UI ��ü�� ��� �ִ� Canvas
    public InventoryManager inventoryManager;

    public int playerPoints = 0;

    private float fadeDuration = 2f; // ������ ������� �ð�
    private float displayDuration = 1f; // ���ڰ� ������ ���̴� �ð�

    private Coroutine fadeOutCoroutine; // ���� ���� ���� �ڷ�ƾ�� ����

    void Start()
    {
        UpdatePlayerPointsText();

        // ����Ʈ ���� �ؽ�Ʈ�� ��Ȱ��ȭ (�ʱ� ����)
        if (insufficientPointsText != null)
        {
            insufficientPointsText.gameObject.SetActive(false);
        }

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
        // K Ű�� ������ �� ���� UI�� ��� �г��� Ȱ��ȭ/��Ȱ��ȭ ���·� ��ȯ
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

    public bool OnBuyItem(ShopItem item)
    {
        // �̹� ������ ���������� Ȯ��
        Button itemButton = item.GetComponent<Button>();
        if (itemButton != null && !itemButton.interactable)
        {
            Debug.LogWarning("This item has already been purchased.");
            return false; // ���� ����
        }

        // �÷��̾��� ����Ʈ�� ������ ���ݺ��� ���� ��� ���� �Ұ�
        if (playerPoints < item.price)
        {
            Debug.LogWarning("Not enough points to purchase this item.");

            // ����Ʈ ���� �ؽ�Ʈ ǥ��
            if (insufficientPointsText != null)
            {
                insufficientPointsText.gameObject.SetActive(true);
                insufficientPointsText.color = new Color(insufficientPointsText.color.r, insufficientPointsText.color.g, insufficientPointsText.color.b, 1f);

                // ������ ���� ���� �ڷ�ƾ�� ������ �����ϰ� ���ο� �ڷ�ƾ ����
                if (fadeOutCoroutine != null)
                {
                    StopCoroutine(fadeOutCoroutine);
                }
                fadeOutCoroutine = StartCoroutine(FadeOutText());
            }

            return false; // ���� ����
        }

        // ���� ������ ���� ó��
        playerPoints -= item.price;
        UpdatePlayerPointsText();

        // �κ��丮�� ������ �߰�
        inventoryManager.AddItem(item);

        // ������ �������� ���� ���� (���ŵ� ���� �ð������� ǥ��)
        Image itemIcon = item.GetComponent<Image>();
        if (itemIcon != null)
        {
            Color iconColor = itemIcon.color;
            iconColor.a = 0.5f; // ���� ����
            itemIcon.color = iconColor;
        }

        // ������ ��ư ��Ȱ��ȭ (�籸�� ����)
        if (itemButton != null)
        {
            itemButton.interactable = false;
        }

        return true; // ���� ����
    }

    private IEnumerator FadeOutText()
    {
        // 1�� ���� �ؽ�Ʈ�� ������ ���̵��� ����
        yield return new WaitForSeconds(displayDuration);

        float startAlpha = insufficientPointsText.color.a;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        // �ؽ�Ʈ�� ������ ������� ����ϴ�.
        while (progress < 1.0f)
        {
            Color tempColor = insufficientPointsText.color;
            insufficientPointsText.color = new Color(tempColor.r, tempColor.g, tempColor.b, Mathf.Lerp(startAlpha, 0, progress));
            progress += rate * Time.deltaTime;
            yield return null;
        }

        insufficientPointsText.gameObject.SetActive(false);
        fadeOutCoroutine = null; // �ڷ�ƾ�� ������ null�� ����
    }

    private void UpdatePlayerPointsText()
    {
        playerPointsText.text = $"Points: {playerPoints}";
    }
}