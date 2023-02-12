using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class InventoryItem : Item
{
    [SerializeField] private TextMeshProUGUI countText = null;

    //이미지를 데이터에 따라 바꿈
    protected override void Initialized()
    {
        picture.sprite = Resources.Load<Sprite>($"Shop&Inventory_Image/Item_Image/{GetMyData.spriteName}");
    }

    //데이터에 카운트를 깍거나 더하고 개수에따라 표시하고 텍스트에 표시
    public override void SetByCount(int _value)
    {

        int count = GameManager.Inst.SetCount(GetMyData.name, _value);

        if (count <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        countText.text = count.ToString();
    }


    #region Button Event

    //아이템을 눌렀을 때 실행
    public void OnClick_Item()
    {
        GameManager.Inst.GetClickManager.SetInfo(this,GetMyData);

        GameManager.Inst.GetUiManager.On_Click_BuildingMode();

        GameManager.Inst.GetClickManager.GetCur_Inven_Item.SetByCount(-1);
    }

    #endregion

}
