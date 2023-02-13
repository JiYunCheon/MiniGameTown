using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : Interactable
{

    private void Awake()
    {
        CompleteEffect();
    }


    //구현을 해야함
    public override void DeSelect_InteractableObj()
    {
        base.DeSelect_InteractableObj();
    }

    public override void Select_InteractableObj()
    {
        base.Select_InteractableObj();
    }

}
