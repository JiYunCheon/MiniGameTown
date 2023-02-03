using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : Item
{
    [SerializeField] private TextMeshProUGUI countText = null;


    protected override void Initialized()
    {
        picture.sprite = Resources.Load<Sprite>($"Shop&Inventory_Image/{GetMyData.SpriteName}");
    }

    protected override void SetCount(int _value)
    {
        if (GameManager.Inst.datas.TryGetValue(GetMyData.GameName, out int value))
        {
            count = value + _value;
            if (count <= 0)
                count = 0;

            GameManager.Inst.datas[GetMyData.GameName] = count;

            if(count<=0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
            }

            countText.text = count.ToString();
        }
    }

    public void InventoryCount(int value)
    {
        SetCount(value);
    }


    #region Button Event

    public void OnClick_Item()
    {
        GameManager.Inst.GetClickManager.SetInfo(GetMyData);

        GameManager.Inst.GetUiManager.Set_Inven_Item(this);
        GameManager.Inst.GetUiManager.On_Click_BuildingMode();

        GameManager.Inst.GetUiManager.GetCur_Inven_Item.InventoryCount(-1);

    }

    #endregion

}
