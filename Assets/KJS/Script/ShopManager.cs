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
    public Text insufficientPointsText; // 포인트 부족 메시지를 표시할 텍스트 UI
    public GameObject shopUICanvas;  // 상점 UI 전체를 담고 있는 Canvas
    public InventoryManager inventoryManager;

    public int playerPoints = 0;

    private float fadeDuration = 2f; // 서서히 사라지는 시간
    private float displayDuration = 1f; // 글자가 완전히 보이는 시간

    private Coroutine fadeOutCoroutine; // 현재 실행 중인 코루틴을 추적

    void Start()
    {
        UpdatePlayerPointsText();

        // 포인트 부족 텍스트를 비활성화 (초기 상태)
        if (insufficientPointsText != null)
        {
            insufficientPointsText.gameObject.SetActive(false);
        }

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

            // 포인트 부족 텍스트 표시
            if (insufficientPointsText != null)
            {
                insufficientPointsText.gameObject.SetActive(true);
                insufficientPointsText.color = new Color(insufficientPointsText.color.r, insufficientPointsText.color.g, insufficientPointsText.color.b, 1f);

                // 기존에 실행 중인 코루틴이 있으면 중지하고 새로운 코루틴 시작
                if (fadeOutCoroutine != null)
                {
                    StopCoroutine(fadeOutCoroutine);
                }
                fadeOutCoroutine = StartCoroutine(FadeOutText());
            }

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

    private IEnumerator FadeOutText()
    {
        // 1초 동안 텍스트가 완전히 보이도록 유지
        yield return new WaitForSeconds(displayDuration);

        float startAlpha = insufficientPointsText.color.a;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        // 텍스트가 서서히 사라지게 만듭니다.
        while (progress < 1.0f)
        {
            Color tempColor = insufficientPointsText.color;
            insufficientPointsText.color = new Color(tempColor.r, tempColor.g, tempColor.b, Mathf.Lerp(startAlpha, 0, progress));
            progress += rate * Time.deltaTime;
            yield return null;
        }

        insufficientPointsText.gameObject.SetActive(false);
        fadeOutCoroutine = null; // 코루틴이 끝나면 null로 설정
    }

    private void UpdatePlayerPointsText()
    {
        playerPointsText.text = $"Points: {playerPoints}";
    }
}