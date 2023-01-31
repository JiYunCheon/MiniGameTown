using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : Item
{
    [SerializeField] private TextMeshProUGUI countText = null;

    private void Start()
    {
        picture.sprite = Resources.Load<Sprite>(GetMyData.SpriteName);
        SetCount(0);
    }
   
    protected override void Initialized()
    {
        if (picture == null)
            picture = GetComponent<Image>();

    }

    protected override void SetCount(int _value)
    {
        if (GameManager.Inst.datas.TryGetValue(GetMyData.GameName, out int value))
        {
            count = value + _value;
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

    public void CountControll(int value)
    {
        SetCount(value);
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
