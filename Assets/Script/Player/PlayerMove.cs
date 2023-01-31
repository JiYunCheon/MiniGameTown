using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent myAgent;

    private void Awake()
    {
        myAgent=GetComponent<NavMeshAgent>();
    }
   
    void Update()
    {
        //�����̰� ���������� ����
        if (GameManager.Inst.GetUiManager.GetSelecCheck || GameManager.Inst.buildingMode || GameManager.Inst.GetUiManager.GetUiCheck) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Building obj = null;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                if(hit.transform.gameObject.TryGetComponent<Building>(out obj))
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

                    if (GameManager.Inst.GetClickManager.GetBeforeHit!=null)
                        GameManager.Inst.GetClickManager.GetBeforeHit.SetDefaultShader();

                    myAgent.destination = hit.point;
                }

            }
        }
                
    }
}
