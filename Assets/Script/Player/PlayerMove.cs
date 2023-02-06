using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public NavMeshAgent myAgent;

    float time = 0;

    private void Awake()
    {
        myAgent=GetComponent<NavMeshAgent>();
    }


    public void PlayerDestination()
    {

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {

            if (hit.transform.gameObject.TryGetComponent<Building>(out Building obj))
            {

                myAgent.destination = obj.GetEntrance.transform.position;
            }
            else
            {
                //������Ʈ���̸� �޽� ������ ���̸� Ʈ�� 
                if (EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true)
                {
                    //������ ���ϰ�� ����
                    return;
                }

                if (GameManager.Inst.GetClickManager.GetCurHitObject != null)
                    GameManager.Inst.GetClickManager.GetCurHitObject.DeSelect_Select_InteractableObj();

                myAgent.destination = hit.point;
            }

        }
    }

}
