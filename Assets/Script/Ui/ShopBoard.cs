using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopBoard : MonoBehaviour
{
    [Header("Scroll View")]
    [SerializeField] private ScrollRect Building_Scroll = null;
    [SerializeField] private ScrollRect Object_Scroll = null;


    private GAMETYPE type;

    private void OnEnable()
    {
        OnClick_Toggle_Building();
    }

    public void ActiveControll(bool active = true)
    {
        this.gameObject.SetActive(active);
    }

    public void TypeChange(GAMETYPE type)
    {
        this.type = type;
    }


    #region Button Event

    public void OnClick_Exit()
    {
        GameManager.Inst.GetUiManager.GetUiCheck = false;
        GameManager.Inst.GetUiManager.Active_ShopBtn();
        GameManager.Inst.GetUiManager.OnClick_Cancel();
        ActiveControll(false);
    }

    public void OnClick_Toggle_Building()
    {
        Scroll_OnOff(true);
    }

    public void OnClick_Toggle_Object()
    {
        Scroll_OnOff(false);
    }

   

    public void OnClick_WatingMode()
    {
        GameManager.Inst.GetUiManager.On_Click_WatingMode();
    }

    #endregion

    private void Scroll_OnOff(bool active)
    {
        Building_Scroll.gameObject.SetActive(active);
        Object_Scroll.gameObject.SetActive(!active);
    }

    public void InstInvenItem()
    {
        foreach (Transform item in Building_Scroll.content.transform)
        {
            if(item.TryGetComponent<ContentItem>(out ContentItem content))
            {
                content.GenerateContent();
            }
        }

        foreach (Transform item in Object_Scroll.content.transform)
        {
            if (item.TryGetComponent<ContentItem>(out ContentItem content))
            {
                content.GenerateContent();
            }
        }

    }
  

}
