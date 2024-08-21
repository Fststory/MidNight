using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ShopItem : MonoBehaviour
{
    public string itemName;      // ������ �̸�
    public int price;            // ������ ����
    public Sprite itemIcon;      // ������ ������
    public Text itemNameText;    // UI�� ǥ���� ������ �̸� �ؽ�Ʈ
    public Text itemPriceText;   // UI�� ǥ���� ������ ���� �ؽ�Ʈ
    public Button buyButton;     // ���� ��ư

    private ShopManager shopManager;

    private void Start()
    {
        // ShopManager �ν��Ͻ� ����
        shopManager = FindObjectOfType<ShopManager>();

        // �ؽ�Ʈ UI�� ������ ���� ����
        itemNameText.text = itemName;
        itemPriceText.text = price.ToString() + " Points";

        // ��ư Ŭ�� �̺�Ʈ�� ShopManager�� ���� ������ ������ �����ϵ��� ����
        buyButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // ShopManager�� ���� ������ ���� ��û
        shopManager.OnBuyItem(this);
    }
}