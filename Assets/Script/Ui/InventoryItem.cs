using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : Item
{

    private void Start()
    {
        picture.sprite = Resources.Load<Sprite>(GetMyData.SpriteName);
    }
    protected override void Initialized()
    {
        if (picture == null)
            picture = GetComponent<Image>();
    }

    #region Button Event

    public void OnClick_Item()
    {
        GameManager.Inst.GetClickManager.SetInfo(GetMyData.AlphaPrefab, GetMyData.Prefab, GetMyData.OccupyPad);
        GameManager.Inst.GetUiManager.GetShopBoard.TypeChange(GetMyData.MyType);
        GameManager.Inst.GetUiManager.Set_Inven_Item(this);
        GameManager.Inst.GetUiManager.On_Click_BuildingMode();
    }

    #endregion

}
