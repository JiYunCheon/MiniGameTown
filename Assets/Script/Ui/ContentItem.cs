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
    [SerializeField] private InventoryItem inven_itemPrefab = null;

    private InventoryItem item;
    public InventoryItem GetItem { get { return item; } set { } }


    //������ �̹����� �ʱ�ȭ 
    protected override void Initialized()
    {
        gameNameText.text = GetMyData.Info;
        priceText.text = $"{GetMyData.price}";
        picture.sprite = Resources.Load<Sprite>($"Shop&Inventory_Image/Item_Image/{GetMyData.spriteName}");
    }

    //���ݹ�ư�� ������ �� ����Ǵ� �Լ�
    public void OnClick_Price()
    {
        if (GetMyData.price < DatabaseAccess.Inst.loginUser.gamemoney)
        {
            InstantiatePurchaseReslut(this);
        }
        else
        {
            GameManager.Inst.GetEffectManager.Inst_SpriteUiEffect(Input.mousePosition, "EffectImage/PurchaseFailed_Image");
        }
    }
    
    //�κ�Ʈ�� ����Ʈ ����
    public void GenerateContent(Transform parent)
    {
        item = Instantiate<InventoryItem>(inven_itemPrefab, parent);
        item.SetMyData(GetMyData);
        item.countIndex = this.countIndex;

    }

    //�� �ȸ��� �Ǵ�
    public void ComparerMaxCount()
    {
        if (GetMyData.myType == OBJECT_TYPE.BUILDING && GetMyData.maxCount > 0)
        {
            GetMyData.maxCount--;
            priceBtn.interactable = false;
        }
    }

    //���â ����
    private void InstantiatePurchaseReslut(ContentItem contentItem)
    {
        GameManager.Inst.GetUiManager.GetSuccessWindow.gameObject.SetActive(true);
        GameManager.Inst.GetUiManager.GetSuccessWindow.SetMyContent(contentItem);

    }
   

}
