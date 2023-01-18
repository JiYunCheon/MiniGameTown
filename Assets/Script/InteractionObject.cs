using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private TriggerCheck entrance = null;
    [SerializeField] private string packageName = null;

    [Header("Material")]
    [SerializeField] private Texture texture = null;
    [SerializeField] private Material material = null;
    [SerializeField] private Shader outlineShader = null;
    [SerializeField] private Shader defaultShader = null;

    private Renderer renderer = null;

    [SerializeField] private bool selectCheck = false;
    public bool GetSelecCheck { get { return selectCheck; } private set { } }
    public string GetPackageName { get { return packageName; } private set { } }

    public TriggerCheck GetEntrance { get { return entrance; } private set { } }

   

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

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
            GameManager.Inst.GetUiManager.ActiveObj(false);
        }
    }

}
