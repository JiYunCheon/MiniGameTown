using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ClickManager : MonoBehaviour
{
    #region Member

    //레이어 마스크
    private int buildingLayer = 1 << 6;
    private int padLayer      = 1 << 7;

    //이전 건물을 저장 할 변수
    private Interactable curHitObject  = null;
    private Ground beforeGround = null;
    private Vector3 saveHitPos  = Vector3.zero;

    //프리뷰 생성 정보 저장 변수
    private Excel curData;

    //빌딩모드에서 현재 선택된 건물의 정보를 가지고 있을 변수
    private Interactable prefab_Building;
    private Interactable prefab_Object;
    private PreviewObject alphaPrefab_Building;
    private PreviewObject alphaPrefab_Object;


    //프리뷰 위지 변동을 위해 필요한 변수
    private PreviewObject preview = null;
    private Ground ground = null;

    //빌딩모드에 들어가서 클릭을 했는지를 체크 할 불 변수
    private bool clickCheck = false;
    [HideInInspector] public bool selectCheck = false;

    #endregion

    #region Property

    public Interactable GetCurHitObject { get { return curHitObject; } private set { } }

    public Excel GetCurData { get { return curData; } private set { } }

    #endregion
   

    private void Start()
    {
        if(GameManager.Inst.CompareLoadScene())
            GameManager.Inst.LoadObj();

    }

    void Update()
    {
        //유아이 위를 클릭했을 경우 리턴 
        if (selectCheck ||EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true) return;


        //빌딩모드, 편집모드가 아닐경우 
        if (Input.GetMouseButtonDown(0) && !GameManager.Inst.buildingMode && !GameManager.Inst.waitingMode)
            InteractionSequence();

        //편집모드일 경우 
        else if (GameManager.Inst.waitingMode && Input.GetMouseButtonDown(0))
            EditModeSequence();

        else
        {
            //현재 데이터가 있고, 생성된 미리보기 객체가 없고 ,빌딩모드일경우 
            if (GameManager.Inst.buildingMode && preview == null && curData != null)
            {
                //미리보기 객체를 생성
                preview = Instantiate<PreviewObject>(GetAlphaPrefab(curData),
                    Camera.main.ScreenToWorldPoint(Input.mousePosition) + 
                    new Vector3(-5f, -3f, -5f), GetAlphaPrefab(curData).transform.rotation);
                preview.SetMyData(curData);

            }

            //빌딩모드이고 클릭을 한적이 없을 경우
            else if(GameManager.Inst.buildingMode && clickCheck==false)
            {
                //미리보기 객체가 생성되어 있을경우
                if (preview != null)
                    //미리보기객체는 마우스 포지션을 따라다님
                    preview.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(-5f, -3f, -5f);
            }
          
            //빌딩모드이고 마우스를 누르고 있을 경우
            if (GameManager.Inst.buildingMode && Input.GetMouseButton(0))
                BuildingModeMoveSequence();
        }
           

    }

    //설치전 현재 선택된 땅의 주변 노드가 설치가능한지 검사 (가능하면 true 불가능하면 false)
    public bool InstCompare()
    {
        if (beforeGround.CompareNode(curData.occupyPad))
            return true;
        else
            return false;
    }


    #region Sequence_Method

    //상호작용 가능한 건물을 눌렀을때 실행될 시퀀스
    private void InteractionSequence()
    {
        RaycastHit hit;

        //건물만 클릭으로 확인
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, buildingLayer))
        {
            //상호 작용이 가능한 오브젝트인지 확인
            if (hit.transform.gameObject.TryGetComponent<Interactable>(out Interactable obj))
            {
                //플레이어의 도착지로 지정
                GameManager.Inst.GetPlayer.PlayerDestination();

                //이전 오브젝트가 없는 경우
                if (curHitObject == null)
                    Interaction(obj, true);

                //이전 오브젝트와 다른 오브젝트를 클릭했을 경우
                else if (curHitObject.transform.gameObject != hit.transform.transform.gameObject)
                {
                    Interaction(obj, true);
                }
                else if(curHitObject.transform.gameObject == hit.transform.transform.gameObject)
                {
                    Interaction(obj, true);
                }

                GameManager.Inst.GetCameraMove.CameraPosMove(obj);
            }
        }
    }

    //편집모드일때 실행될 시퀀스
    private void EditModeSequence()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, buildingLayer))
        {
            //상호작용 가능한 객체인지 확인
            if (hit.transform.TryGetComponent<Interactable>(out Interactable obj))
            {
                curData = obj.GetMyData;

                GameManager.Inst.GetUiManager.On_Click_BuildingMode();
                
                obj.ChangeState(false, Color.white);

                //선택된 오브젝트 파괴
                Destroy(obj.gameObject);
            }
           
        }
    }

    //빌딩모드일 경우 시퀀스
    private void BuildingModeMoveSequence()
    {
        RaycastHit hit;
        //pad만 클릭
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, padLayer))
        {
            
            if (hit.transform.gameObject.TryGetComponent<Ground>(out ground))
            {
                //첫번째 클릭일 경우
                if (saveHitPos == Vector3.zero)
                {
                    clickCheck = true;
                    //선택된 패드를 저장
                    beforeGround = ground;

                    //주변 색 변경
                    beforeGround.SetColor(curData.occupyPad,Color.green);

                    //선택된 곳의 포지션을 저장
                    saveHitPos = ground.transform.position;

                    //미리보기 객체의 위치를 이동
                    preview.transform.position = saveHitPos + new Vector3(0, 0.5f, 0);

                    preview.ChangeState(beforeGround, curData.occupyPad);

                    preview.Active_BuildOption();

                }
                //레이의 힛의 포지션이 변경되었을 경우
                else if (ground.transform.position != saveHitPos)
                {
                    //기존에 선택되었던 패드의 색을 변경
                    beforeGround.SetColor(curData.occupyPad, Color.white);
                    beforeGround.Clear();

                    //이전 패드를 지금 선택된 패드로 초기화
                    beforeGround = ground;

                    //색을 변경
                    beforeGround.SetColor(curData.occupyPad, Color.green);

                    //이전 포지션을 지금 선택된 포지션으로 초기화
                    saveHitPos = ground.transform.position;
                    //초기화된 포지션으로 알파건물의 위치를 변경
                    preview.transform.position = saveHitPos + new Vector3(0, 0.5f, 0);

                    preview.ChangeState(beforeGround, curData.occupyPad);

                }
            }

        }
    }

    #endregion

    //건물 생성 로직
    public void InstObject(Quaternion rotation)
    {
        clickCheck = false;
        //진짜 건물 생성
        Interactable obj = Instantiate<Interactable>(GetPrefab(curData), saveHitPos + new Vector3(0, 0.5f, 0.5f), GetPrefab(curData).transform.rotation, GameManager.Inst.GetBuildings);
        if (rotation != Quaternion.identity)
            obj.transform.rotation = rotation;
        obj.CompleteEffect();

        obj.SetMyData(GameManager.Inst.FindData(obj.name.Split("(")[0]));

        obj.NameRotate(rotation.eulerAngles.y);

        obj.SaveGround(beforeGround.GetNodeList);

        obj.DownPos();

        Refresh();

        GameManager.Inst.GetUiManager.On_Click_WaitingMode();
    }

    //클릭 될때 호출 기능 들
    private void Interaction(Interactable obj , bool check)
    {
        if (!obj.GetInteracterbleCheck) return;

        if(GameManager.Inst.CompareLoadScene())
        {
            foreach (Transform item in GameManager.Inst.GetBuildings)
            {
                if (item.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    interactable.Active_Name(false);
                }
            }
            GameManager.Inst.GetUiManager.Active_HomeUi(false);
            GameManager.Inst.curGameName = obj.GetMyData.packageName;
        }

        curHitObject = obj;
        selectCheck = true;
        curHitObject.Select_InteractableObj();
        curHitObject.SetSelectCheck(check);
    }
    
    //변수 초기화
    private void Refresh()
    {
        beforeGround = null;
        preview = null;
        saveHitPos = Vector3.zero;
    }

    //빌딩모드를 나갈때 정보 초기화
    public void BuildingRefresh()
    {
        selectCheck = false;
        curHitObject.DeSelect_InteractableObj();

        curHitObject.SetSelectCheck(false);
        GetCurHitObject.GetEntrance.ActiveCollider(false);

        Refresh();
    }

    //패드를 초기화
    public void PadRefresh()
    {
        beforeGround.SetColor(curData.occupyPad, Color.white);
        beforeGround.Clear();
        Refresh();
    }

    //클릭한 객체의 정보를 가지고 옴
    public void SetInfo(Excel data)
    {
        curData = data;

        //활성화 될때 함수를 넣어놨는데 
        //델리게이트 체인으로 바꿔도 됨
        GameManager.Inst.GetUiManager.Active_Pad();
        GameManager.Inst.GetUiManager.Active_Pad(false);
        GameManager.Inst.GetUiManager.Active_Pad();
    }

    //타입에 따라 프래팹을 가지고옴
    private Interactable GetPrefab(Excel data)
    {
        if (data.myType == OBJECT_TYPE.BUILDING)
        {
            prefab_Building = Resources.Load<Interactable>($"Prefabs/Building/{data.prefabName}");

            return prefab_Building;
        }
        else
        {
            prefab_Object = Resources.Load<Interactable>($"Prefabs/Object/{data.prefabName}");

            return prefab_Object;
        }
    }

    //타입에 따라 프래팹을 가지고옴
    private PreviewObject GetAlphaPrefab(Excel data)
    {
        if (data.myType == OBJECT_TYPE.BUILDING)
        {
            alphaPrefab_Building = Resources.Load<PreviewObject>($"Prefabs/Building/{data.alphaPrefabName}");

            return alphaPrefab_Building;
        }
        else
        {
            alphaPrefab_Object = Resources.Load<PreviewObject>($"Prefabs/Object/Alpha/{data.alphaPrefabName}");

            return alphaPrefab_Object;
        }
    }

}
