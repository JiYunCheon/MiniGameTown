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
        GameManager.Inst.GetUiManager.On_Click_WatingMode();

        //게임머니를 깎음
        GameManager.Inst.SetCount(GameManager.Inst.GetPlayerData.gameMoney_Key, -curContent.GetMyData.price);

        //게임머니 유아이에 값표시
        GameManager.Inst.GetUiManager.InputGameMoney(GameManager.Inst.GetPlayerData.gameMoney.ToString());

        //현재 개수를 추가
        curContent.GetItem.SetByCount(1);

        //max카운트 확인
        curContent.ComparerMaxCount();

        //유아이 파괴
        Destroy(this.gameObject);
    }

    //구매취소 시 실행
    public void OnClick_Cancel()
    {
        Destroy(this.gameObject);
    }

}
