using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class InventoryItem : Item
{
    [SerializeField] private TextMeshProUGUI countText = null;


    protected override void Initialized()
    {
        picture.sprite = Resources.Load<Sprite>($"Shop&Inventory_Image/Item_Image/{GetMyData.spriteName}");
    }

    public override void SetByCount(int _value)
    {

        count = GameManager.Inst.SetCount(GetMyData.name, _value);

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

    public void OnClick_Item()
    {
        GameManager.Inst.GetClickManager.SetInfo(this,GetMyData);

        GameManager.Inst.GetUiManager.On_Click_BuildingMode();

        GameManager.Inst.GetClickManager.GetCur_Inven_Item.SetByCount(-1);
    }

    #endregion

}
