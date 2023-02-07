using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : Interactable
{
    [Header("===Entrance===")]
    [SerializeField] private TriggerCheck entrance = null;

    [Header("===Material===")]
    [SerializeField] private Texture texture = null;
    [SerializeField] private Shader outlineShader = null;
    [SerializeField] private Shader defaultShader = null;

    private Material defaultMaterial = null;
    private Material lightMaterial = null;

    private bool selectCheck = false;
   
    public bool GetSelecCheck { get { return selectCheck; } private set { } }

    public TriggerCheck GetEntrance { get { return entrance; } private set { } }

    [Header("Choose OutLine or Light Material")]
    [SerializeField] private bool shaderCheck=false;


    private void Awake()
    {
        defaultMaterial = Resources.Load<Material>("Material & Texture/Colors_Default");
        lightMaterial = Resources.Load<Material>("Material & Texture/Colors_Glossy");

        CompleteEffect();
    }







    public void SetSelectCheck(bool check)
    {
        selectCheck = check;

        if(selectCheck==true)
            entrance.ActiveCollider(true);
        else
        {
            entrance.ActiveCollider(false);
            GameManager.Inst.GetUiManager.Active_GameInBtn(false);
        }
    }

    //아웃라인 쉐이더 적용

    public override void Select_InteractableObj()
    {
        if (shaderCheck)
        {
            renderer.material.shader = outlineShader;
            renderer.sharedMaterial.mainTexture = texture;
        }
        else
            renderer.material = lightMaterial;
    }

    //디폴트 쉐이더 적용
    public override void DeSelect_Select_InteractableObj()
    {
        if (shaderCheck)
            renderer.material = defaultMaterial;
        else
            renderer.material = defaultMaterial;
    }
}
