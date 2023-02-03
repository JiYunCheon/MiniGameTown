using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    #region Member

    [Header("Parents")]
    [SerializeField] private Transform buildings = null;

    //���̾� ����ũ
    private int buildingLayer = 1 << 6;
    private int padLayer      = 1 << 7;

    //���� �ǹ��� ���� �� ����
    private Building beforeHit  = null;
    private Ground beforeGround = null;
    private Vector3 saveHitPos  = Vector3.zero;

    //������ ���� ���� ���� ����
    [SerializeField] private Data curData;
    private Interactable prefab_Building;
    private Interactable prefab_Object;
    private PreviewObject alphaPrefab_Building;
    private PreviewObject alphaPrefab_Object;


    //������ ���� ������ ���� �ʿ��� ����
    private PreviewObject preview = null;
    private Ground ground = null;

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

        //�ǹ��� Ŭ������ Ȯ��
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, buildingLayer))
        {
            //��ȣ �ۿ��� ������ ������Ʈ���� Ȯ��
            if (hit.transform.gameObject.TryGetComponent<Building>(out Building obj))
            {
                GameManager.Inst.GetPlayer.PlayerDestination();

                //���� ������Ʈ�� ���� ���
                if (beforeHit == null)
                    Interection(obj, true);
                //���� ������Ʈ�� �ٸ� ������Ʈ�� Ŭ������ ���
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
        //pad�� Ŭ��
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, padLayer))
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * 10, Color.red, 0.3f);

            if (hit.transform.gameObject.TryGetComponent<Ground>(out ground))
            {
                //ù��° Ŭ���� ���
                if (preview == null && saveHitPos == Vector3.zero)
                {
                    //���õ� �е带 ����
                    beforeGround = ground;

                    //�ֺ� �� ����
                    beforeGround.SetColor(curData.OccupyPad,Color.green);

                    //���õ� ���� �������� ����
                    saveHitPos = ground.transform.position;

                    //�� �����ǿ� ���� �ǹ� ����
                    preview = Instantiate<PreviewObject>(GetAlphaPrefab(curData), saveHitPos+new Vector3(0,0.5f,0), Quaternion.identity);

                    preview.Active_BuildOption();

                }
                //������ ���� �������� ����Ǿ��� ���
                else if (ground.transform.position != saveHitPos)
                {
                    //������ ���õǾ��� �е��� ���� ����
                    beforeGround.SetColor(curData.OccupyPad, Color.white);
                    beforeGround.Clear(curData.OccupyPad);

                    //���� �е带 ���� ���õ� �е�� �ʱ�ȭ
                    beforeGround = ground;

                    //���� ����
                    beforeGround.SetColor(curData.OccupyPad, Color.green);

                    //���� �������� ���� ���õ� ���������� �ʱ�ȭ
                    saveHitPos = ground.transform.position;
                    //�ʱ�ȭ�� ���������� ���İǹ��� ��ġ�� ����
                    preview.transform.position = saveHitPos + new Vector3(0, 0.5f, 0);
                }
            }

        }
    }

    #endregion

    //�ǹ� ���� ����
    public void InstObject(Quaternion rotation)
    {

        //��¥ �ǹ� ����
        Interactable obj= Instantiate<Interactable>(GetPrefab(curData), saveHitPos + new Vector3(0, 0.5f, 0.5f), rotation, buildings);
        obj.SetMyData(curData);
        //�ֺ� ��� �ǹ��� ����
        obj.SaveGround(beforeGround.GetNodeList);
        obj.DownPos();
        //�� �ʱ�ȭ
        Refresh();

        GameManager.Inst.GetUiManager.On_Click_WatingMode();
    }

    //Ŭ�� �ɶ� ȣ�� ��� ��
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
        beforeGround.SetColor(curData.OccupyPad, Color.white);
        Refresh();
    }


    //Ŭ���� ��ü�� ������ ������ ��
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
