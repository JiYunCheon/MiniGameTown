using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    private PreviewObject alphaPrefab = null;
    private Interactable prefab       = null;
    private int occupyPad             = 0;

    //������ ���� ������ ���� �ʿ��� ����
    private PreviewObject preview = null;
    private Ground ground = null;

    //������ Ȯ�� ����
    [HideInInspector] public bool choiceCheck = false;

    #endregion

    #region Property

    public int GetOccupyPad { get { return occupyPad;} private set { } }
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

                if (beforeGround.GetNodeList.Count == occupyPad && beforeGround.CompareNode(occupyPad))
                {
                    choiceCheck = true;

                    preview.Active_BuildOption();
                }
            }
        }
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
                Debug.Log(obj.GetMyData.OccupyPad);
                SetInfo(obj.GetMyData.AlphaPrefab, obj.GetMyData.Prefab, obj.GetMyData.OccupyPad);

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

            if (hit.transform.gameObject.TryGetComponent<Ground>(out ground) && choiceCheck == false)
            {
                //ù��° Ŭ���� ���
                if (preview == null && saveHitPos == Vector3.zero)
                {
                    //���õ� �е带 ����
                    beforeGround = ground;

                    //�ֺ� �� ����
                    beforeGround.SetColor(occupyPad,Color.green);

                    //���õ� ���� �������� ����
                    saveHitPos = ground.transform.position;
                    //�� �����ǿ� ���� �ǹ� ����
                    preview = Instantiate<PreviewObject>(alphaPrefab, saveHitPos, Quaternion.identity);
                }
                //������ ���� �������� ����Ǿ��� ���
                else if (ground.transform.position != saveHitPos)
                {
                    //������ ���õǾ��� �е��� ���� ����
                    beforeGround.SetColor(occupyPad, Color.white);
                    beforeGround.Clear(occupyPad);

                    //���� �е带 ���� ���õ� �е�� �ʱ�ȭ
                    beforeGround = ground;

                    //���� ����
                    beforeGround.SetColor(occupyPad, Color.green);

                    //���� �������� ���� ���õ� ���������� �ʱ�ȭ
                    saveHitPos = ground.transform.position;
                    //�ʱ�ȭ�� ���������� ���İǹ��� ��ġ�� ����
                    preview.transform.position = saveHitPos;
                }
            }

        }
    }

    #endregion

    //�ǹ� ���� ����
    public void InstObject(Quaternion rotation)
    {
        //��¥ �ǹ� ����
        Interactable obj= Instantiate<Interactable>(prefab, saveHitPos + new Vector3(0, 0, 0.5f), rotation, buildings);

        //�ֺ� ��� �ǹ��� ����
        obj.SaveGround(beforeGround.GetNodeList);

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
        GameManager.Inst.curGameName = obj.GetMyData.GameName;
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
        beforeGround.SetColor(occupyPad, Color.white);
        Refresh();
    }


    //Ŭ���� ��ü�� ������ ������ ��
    public void SetInfo(PreviewObject alphaPrefab ,Interactable prefab , int occupyPad)
    {
        this.prefab = prefab;
        this.occupyPad = occupyPad;
        this.alphaPrefab = alphaPrefab;

        GameManager.Inst.GetUiManager.Active_Pad();
        GameManager.Inst.GetUiManager.Active_Pad(false);
        GameManager.Inst.GetUiManager.Active_Pad();
    }


}
