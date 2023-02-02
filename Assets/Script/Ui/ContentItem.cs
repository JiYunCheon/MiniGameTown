using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContentItem : Item
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI gameNameText = null;
    [SerializeField] private TextMeshProUGUI priceText = null;

    [Header("Button")]
    [SerializeField] private Button priceBtn = null;

    private InventoryItem inven_itemPrefab = null;

    private Transform contentTr = null;

    private InventoryItem item;

    public InventoryItem GetItem { get { return item; } set { } }


    protected override void Initialized()
    {
        gameNameText.text = GetMyData.GameInfo;
        priceText.text = $"{GetMyData.Price}";
        picture.sprite = Resources.Load<Sprite>($"Shop&Inventory_Image/{GetMyData.SpriteName}");
    }


    public void CompareSoldOutCheck()
    {

        if (GetMyData.MaxCount <= 0)
        {
            priceText.text = $"Already this";
            priceBtn.interactable = false;
            return;
        }
        GetMyData.MaxCount--;

    }

    public void OnClick_Price()
    {
        if (GetMyData.Price < GameManager.Inst.GetPlayerData.GameMoney)
        {
            GameManager.Inst.GetUiManager.Active_S_Window();
            GameManager.Inst.GetUiManager.Set_Content_Item(this);
        }
        else
            GameManager.Inst.GetUiManager.Active_F_Window();

        
    }

    public void GenerateContent()
    {
        inven_itemPrefab = Resources.Load<InventoryItem>("Prefabs/Menu_Item/Item_Inven");

        contentTr = GameManager.Inst.GetUiManager.GetInvenContentTr;
        
        item = Instantiate<InventoryItem>(inven_itemPrefab, contentTr);
        item.SetMyData(GetMyData);
    }

  
   
}
