using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UiManager : MonoBehaviour
{
    #region Member

    [Header("Button")]
    [SerializeField] private TextUi gameInBtn       = null;
    [SerializeField] private Button buildingShopBtn     = null;

    [Header("ShopUi")]
    [SerializeField] private ShopBoard shopBoard        = null;
    [SerializeField] private GameObject modeBtnUi       = null;
    [SerializeField] private GameObject homeUi       = null;

    [Header("MapLevel")]
    [SerializeField] private Material floorMaterial     = null;
    [SerializeField] public PadController padBoard        = null;

    [Header("Iventory")]
    [SerializeField] private RectTransform inventoryRect = null;
    [SerializeField] private ScrollRect inventoryScrollRect = null;
    [SerializeField] private Image invenUpDownBtn;
    [SerializeField] private Sprite[] invenBtnImage;
    [SerializeField] private float upPos = 0;
    [SerializeField] private float downPos = 0;

    [Header("GameMoneyText")]
    [SerializeField] private TextMeshProUGUI gameMoneyText = null;

    [Header("PurchaseComplteUi")]
    [SerializeField] private PurchaseResult successWindow = null;

    [Header("RankingBoard")]
    [SerializeField] private RankingUiData rankingBoard = null;

    //CheckValue
    private int clickCount = 0;

    Color blackColor = new Color(113f / 255f, 113f / 255f, 113f / 255f);
    Coroutine moveCoroutine;

    #endregion

    #region Property

    public PurchaseResult GetSuccessWindow { get { return successWindow; } private set { } }

    #endregion

    private void Awake()
    {
        //Ȥ�ó� ������峪 ������忡�� �������� ���� ����� 
        floorMaterial.color = Color.white;
        //���絷�� ǥ��

        //�κ��丮 ���ٿ� �̹���
        invenBtnImage = new Sprite[2];
        invenBtnImage[0] = Resources.Load<Sprite>("Shop&Inventory_Image/Inven/iven_up");
        invenBtnImage[1] = Resources.Load<Sprite>("Shop&Inventory_Image/Inven/iven_down");

    }

    private void Start()
    {
        InputGameMoney(DatabaseAccess.Inst.loginUser.gamemoney.ToString());
    }
    //���ӸӴ� ǥ�� ������
    public void InputGameMoney(string gameMoneyText)
    {
        this.gameMoneyText.text = gameMoneyText;
    }

    //Ȩ ������ ��ư Ȱ��ȭ ����
    public void Active_HomeUi(bool activeSelf = true)
    {
        homeUi.SetActive(activeSelf);
    }

    //������ Ui Ȱ��ȭ ����
    public void Active_GameInBtn(bool activeSelf = true)
    {
        if(activeSelf)
        {
            gameInBtn.gameObject.SetActive(activeSelf);
            gameInBtn.SetMyData(GameManager.Inst.GetClickManager.GetCurHitObject.GetMyData);
            gameInBtn.SetExplain();
        }
        else
        {
            gameInBtn.gameObject.SetActive(activeSelf);

            //�������� �̸��� ��
            GameManager.Inst.Call_IntractableObj_Method(1);

            GameManager.Inst.GetUiManager.Active_HomeUi(true);
        }

    }

    //�е� Ȱ��ȭ ����
    public void Active_Pad(bool activeSelf = true)
    {
        padBoard.gameObject.SetActive(activeSelf);
    }

    //ShopBoardUi Ȱ��ȭ ����
    public void Active_ShopBtn(bool activeSelf = true)
    {
        buildingShopBtn.gameObject.SetActive(activeSelf);
    }
    public void Active_RankingBtn(bool activeSelf = true)
    {
        rankingBoard.gameObject.SetActive(activeSelf);
    }

    #region ButtonEvent

    //���� ����
    public void OnClick_GameIn()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        OnClick_GameInExit();
        InGame.openApp(GameManager.Inst.curGameName);
    }

    //���� ���� ���
    public void OnClick_GameInExit()
    {
        //ī�޶� ��ġ �ʱ�ȭ
        GameManager.Inst.GetCameraMove.CameraPosMove(null,false);

        GameManager.Inst.GetClickManager.GetCurHitObject.SetSelectCheck(false);

        //Ŭ���� ������Ʈ �ʱ�ȭ
        GameManager.Inst.GetClickManager.BuildingRefresh();

        Active_GameInBtn(false);
        Active_ShopBtn(true);
        GameManager.Inst.GetClickManager.Active_Effect(false);
    }

    //���� ui
    public void OnClick_BuildingShop()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        shopBoard.ActiveControll();

        Active_ShopBtn(false);
    }

    //�������� �̵�
    public void On_Click_BuildingMode()
    {
        ModeChange(true, false);

        padBoard.ActivePadByType(GameManager.Inst.GetClickManager.GetCurData.myType);

        GameManager.Inst.Call_IntractableObj_Method();

        ModeChangeSquence(false, true, blackColor);
    }

    //�������� �̵�
    public void On_Click_WaitingMode()
    {
        ModeChange(false, true);

        Vector2 pos = inventoryRect.anchoredPosition;
        pos.y = upPos;
        inventoryRect.anchoredPosition = pos;

        ModeChangeSquence(true,false,blackColor);
    }

    //��� ������
    public void OnClick_ModeExit()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");


        ModeChange(false, false);

        Active_ShopBtn();

        ModeChangeSquence(false, false, Color.white);
    }

    //��� ������ ���� �̵�
    public void OnClick_ModeExit_Shop()
    {
        OnClick_ModeExit();

        shopBoard.ActiveControll();

        Active_ShopBtn(false);
    }

    //�ź���
    public void OnClick_Go_2DTown()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        GameManager.Inst.SaveData();
        GameManager.Inst.ListClear();

        //�� ����
        SceneManager.LoadScene("2.BaseTown");
    }

    public void Onclick_Save()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        //������ ����
        GameManager.Inst.SaveData();
    }

    public void Onclick_GameExit()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        //������ ����
        GameManager.Inst.SaveData();

        //�����Ͱ� ����� �� ������
        StartCoroutine(WaitForSaveData());
    }

    public void OnClick_Ranking()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        Active_RankingBtn();
    }
    


    #endregion


    private IEnumerator WaitForSaveData()
    {
        //��ٸ��� ���� ui?
        yield return new WaitUntil(() => !DatabaseAccess.Inst.isProcessing);

        InGame.ExitGame();

    }

    //��� ��Ʈ��
    private void ModeChange(bool buildingMode, bool editMode)
    {
        if(GameManager.Inst.buildingMode!=buildingMode)
        {
            GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode, buildingMode);
        }
        if (GameManager.Inst.waitingMode != editMode)
        {
            GameManager.Inst.ChangeMode(out GameManager.Inst.waitingMode, editMode);
        }
    }

    //��尡 ����Ǹ鼭 ����� ������
    private void ModeChangeSquence(bool modeBtn,bool pad,Color color)
    {
        shopBoard.ActiveControll(false);
        
        floorMaterial.color = color;

        modeBtnUi.SetActive(modeBtn);
        bool check = !GameManager.Inst.waitingMode && !GameManager.Inst.buildingMode;
        homeUi.SetActive(check);

        GameManager.Inst.GetUiManager.Active_Pad(pad);
        GameManager.Inst.GetCameraMove.ChangeCameraSize();
    }

    //�κ��丮 ���� �̵�
    public void OnClick_InvenMove()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        if (clickCount==0)
        {
            StopAllCoroutines();
            invenUpDownBtn.sprite = invenBtnImage[0];
            moveCoroutine = StartCoroutine(Move(downPos));
            clickCount++;
        }
        else if(clickCount==1)
        {
            StopAllCoroutines();
            invenUpDownBtn.sprite = invenBtnImage[1];
            moveCoroutine = StartCoroutine(Move(upPos));
            clickCount = 0;
        }
    }
    IEnumerator Move(float ypos)
    {
        Vector2 pos = inventoryRect.anchoredPosition;

        while(true)
        {
            pos.y = Mathf.Lerp(inventoryRect.anchoredPosition.y,ypos, 2.5f * Time.deltaTime);
            inventoryRect.anchoredPosition = pos;

            if (ypos == upPos && inventoryRect.anchoredPosition.y > upPos - 5f)
            {
                pos.y = upPos;
                inventoryRect.anchoredPosition = pos;
                yield break;
            }
            else if (ypos == downPos && inventoryRect.anchoredPosition.y < downPos + 5f)
            {
                pos.y = downPos;
                inventoryRect.anchoredPosition = pos;
                yield break;
            }

            yield return null;
        }
    }

    public InventoryItem GetInventoryContent(Excel data)
    {
        foreach (Transform child in inventoryScrollRect.content.transform)
        {
            if(child.TryGetComponent<InventoryItem>(out InventoryItem item))
            {
                if(item.GetMyData.name==data.name)
                {
                    return item;
                }
            }
        }

        Debug.LogError("OMG NO INVENTORY CONTENT");

        return null;
    }


}
