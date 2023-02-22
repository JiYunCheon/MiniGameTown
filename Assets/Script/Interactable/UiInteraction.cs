using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInteraction : Interactable
{
    [SerializeField] private GameObject uiObject = null;


    private void Active_Ui(bool check = true)
    {
        uiObject.SetActive(check);
    }

    public override void Select_InteractableObj()
    {
        Active_Ui();
    }



}
