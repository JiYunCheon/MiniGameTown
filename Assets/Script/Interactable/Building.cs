using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : Interactable
{
    private Material defaultMaterial = null;
    private Material lightMaterial = null;




    private void Awake()
    {
        defaultMaterial = Resources.Load<Material>("Material & Texture/Colors_Default");
        lightMaterial = Resources.Load<Material>("Material & Texture/Colors_WindowBright");

        //CompleteEffect();
    }

 

    public override void Select_InteractableObj()
    {
        renderer.material = lightMaterial;
    }

    public override void DeSelect_InteractableObj()
    {
        renderer.material = defaultMaterial;
    }
}
