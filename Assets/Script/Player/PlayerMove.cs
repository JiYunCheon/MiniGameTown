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
        Debug.Log("�ȳ�");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Debug.Log("�ȳ�2");

            if (hit.transform.gameObject.TryGetComponent<Building>(out Building obj))
            {
                Debug.Log("�ȳ�3");

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

                if (GameManager.Inst.GetClickManager.GetBeforeHit != null)
                    GameManager.Inst.GetClickManager.GetBeforeHit.SetDefaultShader();

                myAgent.destination = hit.point;
            }

        }
    }

}
