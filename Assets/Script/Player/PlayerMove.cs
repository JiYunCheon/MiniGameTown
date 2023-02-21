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
            //Ŭ���� ��ü�� �ǹ��� ���
            if (hit.transform.gameObject.TryGetComponent<Building>(out Building obj))
            {
                //�ǹ��� �Ա��� �̵�
                anim.SetBool("move", true);

                myAgent.destination = obj.GetEntrance.transform.position;
                des = obj.GetEntrance.transform.position;
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

                anim.SetBool("move", true);
                myAgent.destination = hit.point;

                des = hit.point;
            }
        }
    }
   

}
