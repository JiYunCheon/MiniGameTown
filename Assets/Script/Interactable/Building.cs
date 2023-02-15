using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : Interactable
{
    [Header("===Entrance===")]
    [SerializeField] private TriggerCheck entrance = null;

    private Material defaultMaterial = null;
    private Material lightMaterial = null;

    private bool selectCheck = false;

    public bool GetSelecCheck { get { return selectCheck; } private set { } }

    public TriggerCheck GetEntrance { get { return entrance; } private set { } }

    private void Awake()
    {
        defaultMaterial = Resources.Load<Material>("Material & Texture/Colors_Default");
        lightMaterial = Resources.Load<Material>("Material & Texture/Colors_Glossy");

        //CompleteEffect();
    }

    public void SetSelectCheck(bool check)
    {
        selectCheck = check;

        if (selectCheck == true)
            entrance.ActiveCollider(true);
        else
        {
            entrance.ActiveCollider(false);
            GameManager.Inst.GetUiManager.Active_GameInBtn(false);
        }
    }

    //빛나는 쉐이더 적용
    public override void Select_InteractableObj()
    {
        renderer.material = lightMaterial;
    }

    //디폴트 쉐이더 적용
    public override void DeSelect_InteractableObj()
    {
        renderer.material = defaultMaterial;
    }
}
