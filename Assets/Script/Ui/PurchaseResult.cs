using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseResult : MonoBehaviour
{
    private ContentItem curContent = null;

    //현재 컨탠트를 가져옴
    public virtual void SetMyContent(ContentItem content) => curContent = content;

    //구매성공시 실행됨
    public void OnClick_PurchaseSuccess()
    {
        //편집모드로 이동
        GameManager.Inst.GetUiManager.On_Click_WaitingMode();

        //게임머니를 깎음
        GameManager.Inst.TrySetGameMoney(-curContent.GetMyData.price);

        //게임머니 유아이에 값표시
        GameManager.Inst.GetUiManager.InputGameMoney(GameManager.Inst.GetPlayerData.gamemoney.ToString());

        //현재 개수를 추가
        curContent.GetItem.SetByCount(1);

        //max카운트 확인
        curContent.ComparerMaxCount();

        gameObject.SetActive(false);

    }

    //구매취소 시 실행
    public void OnClick_Cancel()
    {
        gameObject.SetActive(false);
    }

}
