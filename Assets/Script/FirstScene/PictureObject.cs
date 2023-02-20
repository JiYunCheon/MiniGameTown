using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureObject : Select
{
    [SerializeField] private string packageName = null;
   
    public string GetPackageName { get { return packageName; } private set { } }

    public override void GameSelect()
    {
        GameManager.Inst.curGameName = packageName;
        GameManager.Inst.GetUiFirstSceneUiController.ActiveButton(true);
        ChangeScale(biggerScale);
    }

    
}
