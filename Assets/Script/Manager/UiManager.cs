using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject CallgameInObj = null;


    [SerializeField] private Canvas canvas = null;

    private void SetCanvasRotation()
    {
        canvas.transform.LookAt(Camera.main.transform.position);
    }

    public void ActiveObj(bool activeSelf = true)
    {
        CallgameInObj.SetActive(activeSelf);
    }


    public void OnClick_GameIn()
    {
        InGame.IsAppInstalled(GameManager.Inst.GetGameNames[0]);
        InGame.openApp(GameManager.Inst.GetGameNames[0]);
    }


}
