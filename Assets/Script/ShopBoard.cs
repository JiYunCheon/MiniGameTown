using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopBoard : MonoBehaviour
{
    [SerializeField] private GameObject Building_Scroll = null;
    [SerializeField] private GameObject Object_Scroll = null;
    [SerializeField] private Material floorMaterial = null;

    [SerializeField] private GameObject successWindow = null;
    [SerializeField] private GameObject failedWindow = null;
    private Color color;

    private void OnEnable()
    {
        On_Click_Toggle_Building();
    }


    public void ActiveControll(bool active = true)
    {
        this.gameObject.SetActive(active);
    }

    public void On_Click_Exit()
    {
        ActiveControll(false);
    }

    public void On_Click_Toggle_Building()
    {
        Scroll_OnOff(true);
    }

    public void On_Click_Toggle_Object()
    {
        Scroll_OnOff(false);
    }

    public void Onclick_PurchaseSuccess()
    {

    }

    public void Onclick_PurchaseCancel()
    {
        successWindow.SetActive(false);
        failedWindow.SetActive(false);
    }

    public void Active_S_Window(bool active = true)
    {
        successWindow.SetActive(active);
    }
    public void Active_F_Window(bool active = true)
    {
        failedWindow.SetActive(active);
    }

    public void On_Click_BuildingMode()
    {
        ActiveControll(false);
        color = floorMaterial.color;
        floorMaterial.color = new Color(113f/255f, 113f/255f, 113f / 255f);
        Debug.Log(floorMaterial.color);
        GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode,true);
    }


    private void Scroll_OnOff(bool active)
    {
        Building_Scroll.gameObject.SetActive(active);
        Object_Scroll.gameObject.SetActive(!active);
    }

    

}
