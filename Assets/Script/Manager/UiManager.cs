using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject CallgameInObj = null;

    private bool selecCheck = false;
    public bool GetSeleccCheck { get { return selecCheck;} private set { } }
    
    //건물이 선택이 되었는지 확인
    public void ChangeCheckValue(bool check)
    {
        selecCheck = check;
    }

    //버튼 액티브 
    public void ActiveObj(bool activeSelf = true)
    {
        CallgameInObj.SetActive(activeSelf);
    }

    #region ButtonEvent

    public void OnClick_GameIn()
    {
        Debug.Log(GameManager.Inst.curGameName);
        OnClick_Exit();
        InGame.openApp(GameManager.Inst.curGameName);
    }

    public void OnClick_Exit()
    {
        GameManager.Inst.GetClickManager.GetBeforeHit.GetEntrance.ActiveCollider(false);
        GameManager.Inst.GetClickManager.Refresh();

        ChangeCheckValue(false);
        ActiveObj(false);

    }

    #endregion
}
