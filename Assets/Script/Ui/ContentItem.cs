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

   

    private InventoryItem inven_itemPrefab = null;

    private Transform contentTr = null;

    private InventoryItem item;

    public InventoryItem GetItem { get { return item; } set { } }


    protected override void Initialized()
    {
        gameNameText.text = GetMyData.Info;
        priceText.text = $"{GetMyData.price}";
        picture.sprite = Resources.Load<Sprite>($"Shop&Inventory_Image/Item_Image/{GetMyData.spriteName}");
    }


    public void OnClick_Price()
    {
        if (GetMyData.price < GameManager.Inst.GetPlayerData.gameMoney)
        {
            GameManager.Inst.GetUiManager.Active_S_Window();
            GameManager.Inst.GetUiManager.Set_Content_Item(this);
        }
        else
        {
            GameManager.Inst.GetEffectManager.Inst_SpriteUiEffect(GameManager.Inst.CurMousePos(), "EffectImage/PurchaseFailed_Image");
        }
        
    }

    public void GenerateContent()
    {
        inven_itemPrefab = Resources.Load<InventoryItem>("Prefabs/Menu_Item/Item_Inven");

        contentTr = GameManager.Inst.GetUiManager.GetInvenContentTr;
        
        item = Instantiate<InventoryItem>(inven_itemPrefab, contentTr);
        item.SetMyData(GetMyData);
    }

  
   
}
