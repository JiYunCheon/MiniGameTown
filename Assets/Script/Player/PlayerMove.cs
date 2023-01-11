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
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            InteractionObject obj = null;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                if(hit.transform.gameObject.TryGetComponent<InteractionObject>(out obj))
                {
                    myAgent.destination = obj.GetEntranceTr.position;
                }
                else
                {
                    GameManager.Inst.GetClickManager.GetBeforeHit.SetDefaultShader();
                    GameManager.Inst.GetClickManager.GetBeforeHit.SetSelectCheck(false);
                    myAgent.destination = hit.point;
                }

            }
        }
                
    }
}
