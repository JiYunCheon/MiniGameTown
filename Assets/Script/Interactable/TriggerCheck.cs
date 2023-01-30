using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private BoxCollider collider = null;

    private void Awake()
    {
        collider=GetComponent<BoxCollider>();
    }

    public void ActiveCollider(bool active)
    {
        collider.enabled = active;
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerMove player = null;

        //����� �ױ׷� ����
        if (other.gameObject.TryGetComponent<PlayerMove>(out player))
        {
            GameManager.Inst.GetUiManager.Active_ShopBtn(false);

            //ī�޶� ���� ����



            GameManager.Inst.GetUiManager.Active_GameInBtn();

        }
    }
}