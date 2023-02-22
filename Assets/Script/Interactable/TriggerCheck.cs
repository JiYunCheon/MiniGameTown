using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{

    //스크립트 없애고 distance 로 확인?


    private new BoxCollider collider = null;

    private void Awake()
    {
        collider=GetComponent<BoxCollider>();
    }

    //콜라이더 켜고 끔
    public void ActiveCollider(bool active)
    {
        collider.enabled = active;
    }

    //플레이어가 닿으면 유아이 켜짐
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Inst.GetUiManager == null) return;

        if(GameManager.Inst.GetClickManager.GetCurHitObject.GetUiCheck)
        {
            //켐페얼 테그로 변경
            if (other.gameObject.TryGetComponent<PlayerMove>(out PlayerMove player))
            {
                GameManager.Inst.GetUiManager.Active_ShopBtn(false);

                GameManager.Inst.GetUiManager.Active_GameInBtn();

            }
        }

      
    }
}
