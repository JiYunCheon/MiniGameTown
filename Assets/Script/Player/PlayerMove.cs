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
    Coroutine stepSound = null;
    WaitForSeconds stepInterval = null;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        myAgent.speed = 4;
        myAgent.angularSpeed = 300;
        stepInterval = new WaitForSeconds(0.4f);
    }

    void Update()
    {
        if (des!=Vector3.zero && Vector3.Distance(transform.position, des) < 0.8f)
        {
            if(stepSound!=null)
                StopCoroutine(stepSound);
            SoundManager.Inst.StopSFX();

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
                Debug.Log("들어옴");
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
            if(stepSound!=null)
            {
                SoundManager.Inst.StopSFX();
                StopCoroutine(stepSound);
            }

            //클릭한 객체가 건물일 경우
            if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable obj) && 
                obj.GetInteracterbleCheck && obj.GetEntrance != null)
            {
                SoundManager.Inst.StopSFX();

                //건물의 입구로 이동
                anim.SetBool("move", true);

                myAgent.destination = obj.GetEntrance.transform.position;

                des = obj.GetEntrance.transform.position;
                
                GameManager.Inst.GetClickManager.EffectSequence(obj.transform.position, new Vector3(0, 5f, 0));
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

                stepSound = StartCoroutine(StartSound());

                anim.SetBool("move", true);
                myAgent.destination = hit.point;

                des = hit.point;

                GameManager.Inst.GetClickManager.EffectSequence(hit.point, new Vector3(0, 1f, 0));
            }
        }
    }

    

    IEnumerator StartSound()
    {
        while (true)
        {
            SoundManager.Inst.PlaySFX("SFX_Footstep");

            yield return stepInterval;

        }

    }



}
