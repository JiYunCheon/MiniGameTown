using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ClickManager : MonoBehaviour
{
    #region Member

    //���̾� ����ũ
    private int buildingLayer = 1 << 6;
    private int padLayer      = 1 << 7;

    //���� �ǹ��� ���� �� ����
    private Building curHitObject  = null;
    private Ground beforeGround = null;
    private Vector3 saveHitPos  = Vector3.zero;

    //������ ���� ���� ���� ����
    private Excel curData;

    //������忡�� ���� ���õ� �ǹ��� ������ ������ ���� ����
    private Interactable prefab_Building;
    private Interactable prefab_Object;
    private PreviewObject alphaPrefab_Building;
    private PreviewObject alphaPrefab_Object;


    //������ ���� ������ ���� �ʿ��� ����
    private PreviewObject preview = null;
    private Ground ground = null;

    //������忡 ���� Ŭ���� �ߴ����� üũ �� �� ����
    private bool clickCheck = false;
    private bool editCheck = false;
    [HideInInspector] public bool selectCheck = false;

    #endregion

    #region Property

    public Building GetCurHitObject { get { return curHitObject; } private set { } }

    public Excel GetCurData { get { return curData; } private set { } }

    #endregion
   

    private void Start()
    {
        if(GameManager.Inst.CompareLoadScene())
            GameManager.Inst.LoadObj();

    }

    void Update()
    {
        //������ ���� Ŭ������ ��� ���� 
        if (selectCheck || EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true) return;


        //�������, ������尡 �ƴҰ�� 
        if (Input.GetMouseButtonDown(0) && !GameManager.Inst.buildingMode && !GameManager.Inst.waitingMode)
            InteractionSequence();

        //��������� ��� 
        else if (GameManager.Inst.waitingMode && Input.GetMouseButtonDown(0))
            EditModeSequence();

        else
        {
            //���� �����Ͱ� �ְ�, ������ �̸����� ��ü�� ���� ,��������ϰ�� 
            if (GameManager.Inst.buildingMode && preview == null && curData != null)
            {
                //�̸����� ��ü�� ����
                preview = Instantiate<PreviewObject>(GetAlphaPrefab(curData),
                    Camera.main.ScreenToWorldPoint(Input.mousePosition) + 
                    new Vector3(-5f, -3f, -5f), GetAlphaPrefab(curData).transform.rotation);
                preview.SetMyData(curData);

            }

            //��������̰� Ŭ���� ������ ���� ���
            else if(GameManager.Inst.buildingMode && clickCheck==false)
            {
                //�̸����� ��ü�� �����Ǿ� �������
                if (preview != null)
                    //�̸����ⰴü�� ���콺 �������� ����ٴ�
                    preview.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(-5f, -3f, -5f);
            }
          
            //��������̰� ���콺�� ������ ���� ���
            if (GameManager.Inst.buildingMode && Input.GetMouseButton(0))
                BuildingModeMoveSequence();
        }
           

    }

    //��ġ�� ���� ���õ� ���� �ֺ� ��尡 ��ġ�������� �˻� (�����ϸ� true �Ұ����ϸ� false)
    public bool InstCompare()
    {
        if (beforeGround.CompareNode(curData.occupyPad))
            return true;
        else
            return false;
    }


    #region Sequence_Method

    //��ȣ�ۿ� ������ �ǹ��� �������� ����� ������
    private void InteractionSequence()
    {
        RaycastHit hit;

        //�ǹ��� Ŭ������ Ȯ��
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, buildingLayer))
        {
            //��ȣ �ۿ��� ������ ������Ʈ���� Ȯ��
            if (hit.transform.gameObject.TryGetComponent<Building>(out Building obj))
            {
                //�÷��̾��� �������� ����
                GameManager.Inst.GetPlayer.PlayerDestination();

                //���� ������Ʈ�� ���� ���
                if (curHitObject == null)
                    Interaction(obj, true);

                //���� ������Ʈ�� �ٸ� ������Ʈ�� Ŭ������ ���
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

    //��������϶� ����� ������
    private void EditModeSequence()
    {
        RaycastHit hit;
        editCheck = true;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, buildingLayer))
        {
            //��ȣ�ۿ� ������ ��ü���� Ȯ��
            if (hit.transform.TryGetComponent<Interactable>(out Interactable obj))
            {
                curData = obj.GetMyData;

                GameManager.Inst.GetUiManager.On_Click_BuildingMode();
                
                obj.ChangeState(false, Color.white);

                //���õ� ������Ʈ �ı�
                Destroy(obj.gameObject);
            }
           
        }
    }

    //��������� ��� ������
    private void BuildingModeMoveSequence()
    {
        RaycastHit hit;
        //pad�� Ŭ��
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, padLayer))
        {
            
            if (hit.transform.gameObject.TryGetComponent<Ground>(out ground))
            {
                //ù��° Ŭ���� ���
                if (saveHitPos == Vector3.zero)
                {
                    clickCheck = true;
                    //���õ� �е带 ����
                    beforeGround = ground;

                    //�ֺ� �� ����
                    beforeGround.SetColor(curData.occupyPad,Color.green);

                    //���õ� ���� �������� ����
                    saveHitPos = ground.transform.position;

                    //�̸����� ��ü�� ��ġ�� �̵�
                    preview.transform.position = saveHitPos + new Vector3(0, 0.5f, 0);

                    preview.ChangeState(beforeGround, curData.occupyPad);

                    preview.Active_BuildOption();

                    if(editCheck)
                    {
                        preview.editCheck = true;
                        editCheck = false;
                    }
                }
                //������ ���� �������� ����Ǿ��� ���
                else if (ground.transform.position != saveHitPos)
                {
                    //������ ���õǾ��� �е��� ���� ����
                    beforeGround.SetColor(curData.occupyPad, Color.white);
                    beforeGround.Clear();

                    //���� �е带 ���� ���õ� �е�� �ʱ�ȭ
                    beforeGround = ground;

                    //���� ����
                    beforeGround.SetColor(curData.occupyPad, Color.green);

                    //���� �������� ���� ���õ� ���������� �ʱ�ȭ
                    saveHitPos = ground.transform.position;
                    //�ʱ�ȭ�� ���������� ���İǹ��� ��ġ�� ����
                    preview.transform.position = saveHitPos + new Vector3(0, 0.5f, 0);

                    preview.ChangeState(beforeGround, curData.occupyPad);

                }
            }

        }
    }

    #endregion

    //�ǹ� ���� ����
    public void InstObject(Quaternion rotation)
    {
        clickCheck = false;
        //��¥ �ǹ� ����
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

    //Ŭ�� �ɶ� ȣ�� ��� ��
    private void Interaction(Building obj , bool check)
    {
        foreach (Transform item in GameManager.Inst.GetBuildings)
        {
            if (item.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.Active_Name(false);
            }
        }
        GameManager.Inst.GetUiManager.Active_HomeUi(false);

        curHitObject = obj;
        curHitObject.Select_InteractableObj();
        curHitObject.SetSelectCheck(check);
        GameManager.Inst.curGameName = obj.GetMyData.packageName;
        selectCheck = true;
    }
    
    //���� �ʱ�ȭ
    private void Refresh()
    {
        beforeGround = null;
        preview = null;
        saveHitPos = Vector3.zero;
    }

    //������带 ������ ���� �ʱ�ȭ
    public void BuildingRefresh()
    {
        selectCheck = false;
        curHitObject.DeSelect_InteractableObj();

        curHitObject.SetSelectCheck(false);
        GetCurHitObject.GetEntrance.ActiveCollider(false);

        Refresh();
    }

    //�е带 �ʱ�ȭ
    public void PadRefresh()
    {
        beforeGround.SetColor(curData.occupyPad, Color.white);
        beforeGround.Clear();
        Refresh();
    }

    //Ŭ���� ��ü�� ������ ������ ��
    public void SetInfo(Excel data)
    {
        curData = data;

        //Ȱ��ȭ �ɶ� �Լ��� �־���µ� 
        //��������Ʈ ü������ �ٲ㵵 ��
        GameManager.Inst.GetUiManager.Active_Pad();
        GameManager.Inst.GetUiManager.Active_Pad(false);
        GameManager.Inst.GetUiManager.Active_Pad();
    }

    //Ÿ�Կ� ���� �������� �������
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

    //Ÿ�Կ� ���� �������� �������
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
