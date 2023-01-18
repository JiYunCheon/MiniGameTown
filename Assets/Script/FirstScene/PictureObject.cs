using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureObject : MonoBehaviour
{
    [SerializeField] private string packageName = null;

    public string GetPackageName { get { return packageName; } private set { } }

    public void ChangeScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void GameSelect()
    {
        GameManager.Inst.curGameName = packageName;
        GameManager.Inst.GetUiFirstSceneUiController.ActiveButton(true);
    }


}
