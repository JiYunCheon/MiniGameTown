using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent myAgent;

    private void Awake()
    {
        myAgent=GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        //유아이가 켜져있으면 리턴
        if (GameManager.Inst.GetUiManager.GetSeleccCheck || GameManager.Inst.GetBuildingMode) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            InteractionObject obj = null;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                if(hit.transform.gameObject.TryGetComponent<InteractionObject>(out obj))
                {
                    myAgent.destination = obj.GetEntrance.transform.position;
                }
                else
                {
                    if(GameManager.Inst.GetClickManager.GetBeforeHit!=null)
                        GameManager.Inst.GetClickManager.GetBeforeHit.SetDefaultShader();

                    myAgent.destination = hit.point;
                }

            }
        }
                
    }
}
