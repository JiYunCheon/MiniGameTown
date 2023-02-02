using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    #region Member

    [Header("Parents")]
    [SerializeField] private Transform buildings = null;

    //레이어 마스크
    private int buildingLayer = 1 << 6;
    private int padLayer      = 1 << 7;

    //이전 건물을 저장 할 변수
    private Building beforeHit  = null;
    private Ground beforeGround = null;
    private Vector3 saveHitPos  = Vector3.zero;

    //프리뷰 생성 정보 저장 변수
    [SerializeField] private Data curData;
    private Interactable prefab_Building;
    private Interactable prefab_Object;
    private PreviewObject alphaPrefab_Building;
    private PreviewObject alphaPrefab_Object;


    //프리뷰 위지 변동을 위해 필요한 변수
    private PreviewObject preview = null;
    private Ground ground = null;

    //편집툴 확인 변수
    [HideInInspector] public bool choiceCheck = false;

    #endregion

    #region Property

    public int GetOccupyPad { get { return curData.OccupyPad; } private set { } }
    public Transform GetBuildings { get { return buildings; } private set { } }
    public Building GetBeforeHit { get { return beforeHit; } private set { } }


    #endregion

    void Update()
    {
        if (GameManager.Inst.GetUiManager.GetSelecCheck || 
            EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true) return;

        if (Input.GetMouseButtonDown(0) && !GameManager.Inst.buildingMode && !GameManager.Inst.waitingMode)
            BuildingInteractionSequence();

        else if (GameManager.Inst.waitingMode && Input.GetMouseButtonDown(0))
            EditModeSequence();

        else
        {
            if (GameManager.Inst.buildingMode && Input.GetMouseButton(0))
                BuildingModeMoveSequence();

            else if (Input.GetMouseButtonUp(0))
            {
                if (beforeGround == null) return;

                if (InstCompare())
                {
                    choiceCheck = true;

                }
            }
        }
    }

    public bool InstCompare()
    {
        if (beforeGround.GetNodeList.Count == curData.OccupyPad && beforeGround.CompareNode(curData.OccupyPad))
            return true;
        else
            return false;
    }


    #region Sequence_Method

    private void BuildingInteractionSequence()
    {
        RaycastHit hit;

        //건물만 클릭으로 확인
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, buildingLayer))
        {
            //상호 작용이 가능한 오브젝트인지 확인
            if (hit.transform.gameObject.TryGetComponent<Building>(out Building obj))
            {
                GameManager.Inst.GetPlayer.PlayerDestination();
               

                //이전 오브젝트가 없는 경우
                if (beforeHit == null)
                    Interection(obj, true);
                //이전 오브젝트와 다른 오브젝트를 클릭했을 경우
                else if (beforeHit.transform.gameObject != hit.transform.transform.gameObject)
                {
                    beforeHit.SetDefaultShader();
                    GameManager.Inst.GetUiManager.ChangeSelecChcek(false);
                    Interection(obj, true);
                }
                else if (beforeHit.transform.gameObject == hit.transform.transform.gameObject)
                {
                    beforeHit.SetDefaultShader();
                    GameManager.Inst.GetUiManager.ChangeSelecChcek(false);
                    Interection(obj, true);
                }
            }
        }
    }

    private void EditModeSequence()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, buildingLayer))
        {
            GameManager.Inst.GetUiManager.On_Click_BuildingMode();
           

            if (hit.transform.TryGetComponent<Interactable>(out Interactable obj))
            {
                SetInfo(obj.GetMyData);
                GameManager.Inst.GetUiManager.GetCur_Inven_Item = obj.GetInventoryItem;

                for (int i = 0; i < obj.myGround.Count; i++)
                {
                    obj.myGround[i].ChangeBuildingState(false, Color.white);
                }

                Destroy(obj.gameObject);
            }
        }
    }

    private void BuildingModeMoveSequence()
    {
        RaycastHit hit;
        //pad만 클릭
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, padLayer))
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * 10, Color.red, 0.3f);

            if (hit.transform.gameObject.TryGetComponent<Ground>(out ground) && choiceCheck == false)
            {
                //첫번째 클릭일 경우
                if (preview == null && saveHitPos == Vector3.zero)
                {
                    //선택된 패드를 저장
                    beforeGround = ground;

                    //주변 색 변경
                    beforeGround.SetColor(curData.OccupyPad,Color.green);

                    //선택된 곳의 포지션을 저장
                    saveHitPos = ground.transform.position;

                    //그 포지션에 알파 건물 생성
                    preview = Instantiate<PreviewObject>(GetAlphaPrefab(curData), saveHitPos, Quaternion.identity);

                    preview.Active_BuildOption();

                }
                //레이의 힛의 포지션이 변경되었을 경우
                else if (ground.transform.position != saveHitPos)
                {
                    //기존에 선택되었던 패드의 색을 변경
                    beforeGround.SetColor(curData.OccupyPad, Color.white);
                    beforeGround.Clear(curData.OccupyPad);

                    //이전 패드를 지금 선택된 패드로 초기화
                    beforeGround = ground;

                    //색을 변경
                    beforeGround.SetColor(curData.OccupyPad, Color.green);

                    //이전 포지션을 지금 선택된 포지션으로 초기화
                    saveHitPos = ground.transform.position;
                    //초기화된 포지션으로 알파건물의 위치를 변경
                    preview.transform.position = saveHitPos;
                }
            }

        }
    }

    #endregion

    //건물 생성 로직
    public void InstObject(Quaternion rotation)
    {

        //진짜 건물 생성
        Interactable obj= Instantiate<Interactable>(GetPrefab(curData), saveHitPos + new Vector3(0, 0, 0.5f), rotation, buildings);
        obj.SetMyData(curData);
        //주변 노드 건물에 저장
        obj.SaveGround(beforeGround.GetNodeList);

        //값 초기화
        Refresh();

        GameManager.Inst.GetUiManager.On_Click_WatingMode();
    }

    //클릭 될때 호출 기능 들
    private void Interection(Building obj , bool check)
    {
        beforeHit = obj;
        beforeHit.SetOutLineShader();
        beforeHit.SetSelectCheck(check);
        GameManager.Inst.curGameName = obj.GetMyData.PackageName;
        GameManager.Inst.GetUiManager.ChangeSelecChcek(check);
    }
    
    private void Refresh()
    {
        beforeGround = null;
        preview = null;
        saveHitPos = Vector3.zero;
    }

    public void BuildingRefresh()
    {
        beforeHit.SetDefaultShader();
        beforeHit.SetSelectCheck(false);
        GameManager.Inst.GetUiManager.ChangeSelecChcek(false);
        Refresh();
    }

    public void PadRefresh()
    {
        choiceCheck = false;
        beforeGround.SetColor(curData.OccupyPad, Color.white);
        Refresh();
    }


    //클릭한 객체의 정보를 가지고 옴
    public void SetInfo(Data data)
    {
        curData = data;

        GameManager.Inst.GetUiManager.Active_Pad();
        GameManager.Inst.GetUiManager.Active_Pad(false);
        GameManager.Inst.GetUiManager.Active_Pad();
    }

    private Interactable GetPrefab(Data data)
    {
        if (data.MyType == OBJECT_TYPE.BUIDING)
        {
            prefab_Building = Resources.Load<Interactable>($"Prefabs/Building/{data.PrefabName}");

            return prefab_Building;
        }
        else
        {
            prefab_Object = Resources.Load<Interactable>($"Prefabs/Object/{data.PrefabName}");

            return prefab_Object;
        }
    }

    private PreviewObject GetAlphaPrefab(Data data)
    {
        if (data.MyType == OBJECT_TYPE.BUIDING)
        {
            alphaPrefab_Building = Resources.Load<PreviewObject>($"Prefabs/Building/{data.AlphaPrefabName}");

            return alphaPrefab_Building;
        }
        else
        {
            alphaPrefab_Object = Resources.Load<PreviewObject>($"Prefabs/Object/{data.AlphaPrefabName}");

            return alphaPrefab_Object;
        }
    }

}
