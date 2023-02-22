using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    [Header("===Entrance===")]
    [SerializeField] private TriggerCheck entrance = null;

    [Header("===ListCheck===")]
    public List<Ground> myGround = new List<Ground>();

    [Header("Destination")]
    [SerializeField] private Transform cameraPos = null;
    [SerializeField] private Transform nameTr = null;

    private Excel myData = null;

    [SerializeField] protected new Renderer renderer = null;
    [SerializeField] private bool interacterbleCheck = true;
    [SerializeField] private bool uiCheck = false;


    private bool selectCheck = false;

    #region Property

    public bool GetUiCheck { get { return uiCheck; } private set { } }

    public bool GetInteracterbleCheck { get { return interacterbleCheck; } private set { } }

    public TriggerCheck GetEntrance { get { return entrance; } private set { } }

    public bool GetSelecCheck { get { return selectCheck; } private set { } }

    public Transform GetCameraPos { get { return cameraPos; } private set { } }

    public Excel GetMyData { get { return myData; }private set { } }

    #endregion

    private void Start()
    {
        Initialized();
    }

    protected virtual void Initialized()
    {
        renderer = GetComponent<Renderer>();
    }

    //주변 패드를 저장
    public void SaveGround(List<Ground> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            myGround.Add(nodes[i]);
        }
    }

    //건물이 설치되면 위에서 아래로 떨어짐
    public void DownPos()
    {
        StartCoroutine(Down());
    }
    IEnumerator Down()
    {
        while (true)
        {
            transform.Translate(0, -0.2f, 0);

            if (transform.localPosition.y < 1.41f)
            {
                Vector3 pos = transform.localPosition;
                pos.y = 1.4f;
                transform.localPosition = pos;   
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    //생성된 건물의 로테이션에 따른 이름 회전값 변경
    public void NameRotate(float rotationY)
    {
        if(nameTr != null)
            nameTr.Rotate(0, -rotationY, 0);
    }

    //건물 이름의 활정화 제어
    public void Active_Name(bool check = true)
    {
        if (nameTr != null)
            nameTr.gameObject.SetActive(check);
    }

    //현재 가지고있는 패드의 상태를 변화
    public void ChangeState(bool state , Color color)
    {
        for (int i = 0; i < myGround.Count; i++)
        {
            myGround[i].ChangePadState(state,color);
        }
    }

    public virtual void SetMyData(Excel data) => myData = data;

    //설치완료 이펙트 출력
    public virtual void CompleteEffect()
    {
        GameManager.Inst.GetEffectManager.Inst_SpriteEffect(this.transform.position+ new Vector3(0,3.5f,0), "EffectImage/MakeComplete_Image");
    }

    //선택되었을 때 실행될 함수
    public virtual void Select_InteractableObj() { }

    //선택취소되었을 때 실행될 함수
    public virtual void DeSelect_InteractableObj() { }

    public virtual void SetSelectCheck(bool check)
    {
        if (entrance == null) return;

        selectCheck = check;

        if (selectCheck == true)
            entrance.ActiveCollider(true);
        else
        {
            entrance.ActiveCollider(false);

            if(GameManager.Inst.GetUiManager!=null)
                GameManager.Inst.GetUiManager.Active_GameInBtn(false);
        }
    }
}
