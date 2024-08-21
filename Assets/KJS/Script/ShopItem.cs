using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ShopItem : MonoBehaviour
{
    public string itemName;      // 아이템 이름
    public int price;            // 아이템 가격
    public Sprite itemIcon;      // 아이템 아이콘
    public Text itemNameText;    // UI에 표시할 아이템 이름 텍스트
    public Text itemPriceText;   // UI에 표시할 아이템 가격 텍스트
    public Button buyButton;     // 구매 버튼

    private ShopManager shopManager;

    private void Start()
    {
        // ShopManager 인스턴스 참조
        shopManager = FindObjectOfType<ShopManager>();

        // 텍스트 UI에 아이템 정보 설정
        itemNameText.text = itemName;
        itemPriceText.text = price.ToString() + " Points";

        // 버튼 클릭 이벤트에 ShopManager에 현재 아이템 정보를 전달하도록 설정
        buyButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // ShopManager를 통해 아이템 구매 요청
        shopManager.OnBuyItem(this);
    }
}