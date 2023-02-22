using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonObject : Interactable
{
    public override void Select_InteractableObj()
    {
        GameManager.Inst.GetPlayer.Interaction();
        GameManager.Inst.GetInteractionManager.GetSpriteSpawner.SpawnStart();
        SetSelectCheck(false);
        GameManager.Inst.GetClickManager.BuildingRefresh();
    }
    

}
