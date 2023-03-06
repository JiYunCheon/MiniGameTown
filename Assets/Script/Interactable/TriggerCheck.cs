using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{

    //��ũ��Ʈ ���ְ� distance �� Ȯ��?
    [HideInInspector] public bool triggerCheck = false; 

    private new BoxCollider collider = null;

    private void Awake()
    {
        collider=GetComponent<BoxCollider>();
    }

    //�ݶ��̴� �Ѱ� ��
    public void ActiveCollider(bool active)
    {
        collider.enabled = active;
    }

    //�÷��̾ ������ ������ ����
    private void OnTriggerEnter(Collider other)
    {
        triggerCheck = true;

        if (GameManager.Inst.GetUiManager == null) return;

        if(GameManager.Inst.GetClickManager.GetCurHitObject.GetUiCheck)
        {
            //����� �ױ׷� ����
            if (other.gameObject.TryGetComponent<PlayerMove>(out PlayerMove player))
            {
                GameManager.Inst.GetUiManager.Active_GameInBtn();

                GameManager.Inst.GetUiManager.Active_HomeUi(false);

                GameManager.Inst.GetCameraMove.CameraPosMove(GameManager.Inst.GetClickManager.GetCurHitObject);

                GameManager.Inst.GetClickManager.selectCheck = true;
            }
        }
        
    }
}
