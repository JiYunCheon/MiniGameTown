using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBoard : MonoBehaviour
{
    [Header("Scroll View")]
    [SerializeField] private GameObject Building_Scroll = null;
    [SerializeField] private GameObject Object_Scroll = null;

    [Header("Result Ui")]
    [SerializeField] private GameObject successWindow = null;
    [SerializeField] private GameObject failedWindow = null;

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
        OnClick_Cancel();
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

    public void OnClick_PurchaseSuccess()
    {
        GameManager.Inst.GetUiManager.On_Click_WatingMode();

        Active_S_Window(false);

    }

    public void OnClick_Cancel()
    {
        Active_S_Window(false);
        Active_F_Window(false);
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

    public void Active_S_Window(bool active = true)
    {
        successWindow.SetActive(active);
    }

    public void Active_F_Window(bool active = true)
    {
        failedWindow.SetActive(active);
    }

}
