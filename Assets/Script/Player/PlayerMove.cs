using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject[] balloon = null;

    private GameObject beforeBalloon = null;
    public NavMeshAgent myAgent;
    Animator anim;
    Vector3 des = Vector3.zero;
    Coroutine interaction = null;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        myAgent.speed = 4;
        myAgent.angularSpeed = 300;
    }

    void Update()
    {
        if (des!=Vector3.zero && Vector3.Distance(transform.position, des) < 0.8f)
        {
            GameManager.Inst.GetClickManager.Active_Effect(false);
            anim.SetBool("move", false);
            des = Vector3.zero;
        }
    }

    private void Active_Item(bool check = true)
    {
        int random = 0;
        random = Random.Range(0,balloon.Length);

        if(beforeBalloon!=null)
            beforeBalloon.SetActive(false);

        balloon[random].SetActive(check);
        beforeBalloon = balloon[random];
    }

    public void Interaction(Interactable obj)
    {
        if (interaction != null)
            StopCoroutine(interaction);


        interaction = StartCoroutine(DistanceCheck(obj));
    }


    IEnumerator DistanceCheck(Interactable obj)
    {
        int count = 0;

        while (true)
        {
            if (count > 10)
            {
                yield break;
            }

            yield return new WaitForSeconds(1f);

            count++;

            if (Vector3.Distance(this.transform.position, obj.transform.position)<2f)
            {
                StartCoroutine(ActiveControll());
                yield break;
            }

        }
    }



    IEnumerator ActiveControll()
    {
        int count = 0;
        Active_Item();
        while (true)
        {
            yield return new WaitForSeconds(1f);

            count++;

            if(count > 5)
            {
                Debug.Log("����");
                Active_Item(false);
                yield break;
            }
        }

    }


    public void PlayerDestination()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            //Ŭ���� ��ü�� �ǹ��� ���
            if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable obj) && 
                obj.GetInteracterbleCheck && obj.GetEntrance != null)
            {
                //�ǹ��� �Ա��� �̵�
                anim.SetBool("move", true);

                myAgent.destination = obj.GetEntrance.transform.position;

                des = obj.GetEntrance.transform.position;
                
                GameManager.Inst.GetClickManager.EffectSequence(obj.transform.position, new Vector3(0, 5f, 0));
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

                GameManager.Inst.GetClickManager.EffectSequence(hit.point, new Vector3(0, 1f, 0));
            }
        }
    }
   

}
