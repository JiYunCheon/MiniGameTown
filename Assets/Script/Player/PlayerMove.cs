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
            //클릭한 객체가 건물일 경우
            if (hit.transform.gameObject.TryGetComponent<Building>(out Building obj))
            {
                //건물의 입구로 이동
                myAgent.destination = obj.GetEntrance.transform.position;
            }
            else
            {
                //오브젝트위이면 펄스 유아이 위이면 트루 
                if (EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true)
                {
                    //유아이 위일경우 리턴
                    return;
                }

                //클릭한 객체가 빌딩이 아닐 때
                if (GameManager.Inst.GetClickManager.GetCurHitObject != null)
                    GameManager.Inst.GetClickManager.GetCurHitObject.DeSelect_InteractableObj();

                
                myAgent.destination = hit.point;
            }

        }
    }

}
