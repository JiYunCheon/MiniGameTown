using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_Item: MonoBehaviour
{
    Scriptable_Item data;
    Button btn;

    public void setButton(Button btn)
    {
        this.btn = btn;

        btn.onClick.AddListener(() => Click());
    }
    public void setData(Scriptable_Item data)
    {
        this.data = data;
    }
    public void Click()
    {
        //Debug.Log(data.itemName);
        ShopManager.Inst.openBuyPanel(data);
    }

    public ITEMTYPE getType()
    {
        return data.type;
    }

}
