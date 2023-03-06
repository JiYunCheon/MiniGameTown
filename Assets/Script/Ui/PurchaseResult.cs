using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseResult : MonoBehaviour
{
    [SerializeField] private float waitSecond = 0;
    [SerializeField] GameObject successWindow = null;
    [SerializeField] GameObject resultWindow = null;

    private ContentItem curContent = null;



    private void OnEnable()
    {
        successWindow.SetActive(true);
        resultWindow.SetActive(false);
    }


    //현재 컨탠트를 가져옴
    public virtual void SetMyContent(ContentItem content) => curContent = content;

    //구매성공시 실행됨
    public void OnClick_PurchaseSuccess()
    {
        StartCoroutine(WaitForSecond(waitSecond));
    }


    IEnumerator WaitForSecond(float waitSecond)
    {
        SoundManager.Inst.PlaySFX("SFX_PurChase");
        successWindow.SetActive(false);
        resultWindow.SetActive(true);

        yield return new WaitForSeconds(waitSecond);

        //편집모드로 이동
        GameManager.Inst.GetUiManager.On_Click_WaitingMode();

        //게임머니를 깎음
        GameManager.Inst.TrySetGameMoney(-curContent.GetMyData.price);

        //게임머니 유아이에 값표시
        GameManager.Inst.GetUiManager.InputGameMoney(DatabaseAccess.Inst.loginUser.gamemoney.ToString());

        //현재 개수를 추가
        curContent.GetItem.SetByCount(1);

        //max카운트 확인
        curContent.ComparerMaxCount();

        gameObject.SetActive(false);

    }



    //구매취소 시 실행
    public void OnClick_Cancel()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        gameObject.SetActive(false);
    }

}
