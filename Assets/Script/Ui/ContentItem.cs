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

    [Header("Prefab")]
    [SerializeField] private PurchaseResult successWindowPrfab = null;
    [SerializeField] private InventoryItem inven_itemPrefab = null;

    private InventoryItem item;
    public InventoryItem GetItem { get { return item; } set { } }


    //정보와 이미지를 초기화 
    protected override void Initialized()
    {
        gameNameText.text = GetMyData.Info;
        priceText.text = $"{GetMyData.price}";
        picture.sprite = Resources.Load<Sprite>($"Shop&Inventory_Image/Item_Image/{GetMyData.spriteName}");
    }

    //가격버튼을 눌렀을 때 실행되는 함수
    public void OnClick_Price()
    {
        if (GetMyData.price < GameManager.Inst.GetPlayerData.gameMoney)
        {
            InstantiatePurchaseReslut(this,GameManager.Inst.GetUiManager.GetCanvarsTr);
        }
        else
        {
            GameManager.Inst.GetEffectManager.Inst_SpriteUiEffect(GameManager.Inst.CurMousePos(), "EffectImage/PurchaseFailed_Image");
        }
    }
    
    //인벤트뢰 컨텐트 생성
    public void GenerateContent(Transform parent)
    {
        item = Instantiate<InventoryItem>(inven_itemPrefab, parent);
        item.SetMyData(GetMyData);
    }

    //다 팔린지 판단
    public void ComparerMaxCount()
    {
        if (GetMyData.myType == OBJECT_TYPE.BUILDING && GetMyData.maxCount > 0)
        {
            GetMyData.maxCount--;
            priceBtn.interactable = false;
        }
    }

    //결과창 생성
    private void InstantiatePurchaseReslut(ContentItem contentItem,Transform canvars)
    {
        PurchaseResult resultWindow = Instantiate<PurchaseResult>(successWindowPrfab, canvars);
        resultWindow.SetMyContent(contentItem);
    }
   

}
