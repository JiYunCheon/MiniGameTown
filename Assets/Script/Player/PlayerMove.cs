using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public NavMeshAgent myAgent;

    private void Awake()
    {
        myAgent=GetComponent<NavMeshAgent>();
    }


    public void PlayerDestination()
    {

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            //Ŭ���� ��ü�� �ǹ��� ���
            if (hit.transform.gameObject.TryGetComponent<Building>(out Building obj))
            {
                //�ǹ��� �Ա��� �̵�
                myAgent.destination = obj.GetEntrance.transform.position;
            }
            else
            {
                //������Ʈ���̸� �޽� Ui ���̸� Ʈ�� 
                if (EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true)
                {
                    //Ui ���ϰ�� ����
                    return;
                }

                //Ŭ���� ��ü�� ������ �ƴ� ��
                if (GameManager.Inst.GetClickManager.GetCurHitObject != null)
                    GameManager.Inst.GetClickManager.GetCurHitObject.DeSelect_InteractableObj();

                Debug.Log("�̵�");
                myAgent.destination = hit.point;
            }

        }
    }

}
