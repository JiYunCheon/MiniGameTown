using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public NavMeshAgent myAgent;
    Animator anim;
    Vector3 des = Vector3.zero;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        myAgent.speed = 3;
        myAgent.angularSpeed = 300;
    }

    void Update()
    {
        if (des!=Vector3.zero && Vector3.Distance(transform.position, des) < 0.5f)
        {
            anim.SetBool("move", false);
            des = Vector3.zero;
        }
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
                anim.SetBool("move", true);

                myAgent.destination = obj.GetEntrance.transform.position;
                des = obj.GetEntrance.transform.position;
            }
            else
            {
                //오브젝트위이면 펄스 Ui 위이면 트루 
                if (EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true)
                {
                    //Ui 위일경우 리턴
                    return;
                }

                //클릭한 객체가 빌딩이 아닐 때
                if (GameManager.Inst.GetClickManager.GetCurHitObject != null)
                    GameManager.Inst.GetClickManager.GetCurHitObject.DeSelect_InteractableObj();

                anim.SetBool("move", true);
                myAgent.destination = hit.point;

                des = hit.point;
            }
        }
    }
   

}
