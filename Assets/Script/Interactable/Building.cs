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
    [SerializeField] private Material material = null;
    [SerializeField] private Shader outlineShader = null;
    [SerializeField] private Shader defaultShader = null;



    private bool selectCheck = false;
   
    public bool GetSelecCheck { get { return selectCheck; } private set { } }

    public TriggerCheck GetEntrance { get { return entrance; } private set { } }


    //아웃라인 쉐이더 적용
    public void SetOutLineShader()
    {
        renderer.material.shader = outlineShader;
        renderer.sharedMaterial.mainTexture = texture;
    }

    //디폴트 쉐이더 적용
    public void SetDefaultShader()
    {
        renderer.material = material;
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

}
