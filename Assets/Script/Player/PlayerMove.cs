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
                //오브젝트위이면 펄스 유아이 위이면 트루 
                if (EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true)
                {
                    //유아이 위일경우 리턴
                    return;
                }

                if (GameManager.Inst.GetClickManager.GetBeforeHit != null)
                    GameManager.Inst.GetClickManager.GetBeforeHit.SetDefaultShader();

                myAgent.destination = hit.point;
            }

        }
    }

}
