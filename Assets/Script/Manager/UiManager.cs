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
        //혹시나 편집모드나 빌딩모드에서 강종했을 때를 대비해 
        floorMaterial.color = Color.white;
        //현재돈을 표시

        //인벤토리 업다운 이미지
        invenBtnImage = new Sprite[2];
        invenBtnImage[0] = Resources.Load<Sprite>("Shop&Inventory_Image/Inven/iven_up");
        invenBtnImage[1] = Resources.Load<Sprite>("Shop&Inventory_Image/Inven/iven_down");

    }

    private void Start()
    {
        InputGameMoney(DatabaseAccess.Inst.loginUser.gamemoney.ToString());
    }
    //게임머니 표시 유아이
    public void InputGameMoney(string gameMoneyText)
    {
        this.gameMoneyText.text = gameMoneyText;
    }

    //홈 유아이 버튼 활성화 제어
    public void Active_HomeUi(bool activeSelf = true)
    {
        homeUi.SetActive(activeSelf);
    }

    //게임인 Ui 활성화 제어
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

            //빌딩들의 이름을 끔
            GameManager.Inst.Call_IntractableObj_Method(1);

            GameManager.Inst.GetUiManager.Active_HomeUi(true);
        }

    }

    //패드 활성화 제어
    public void Active_Pad(bool activeSelf = true)
    {
        padBoard.gameObject.SetActive(activeSelf);
    }

    //ShopBoardUi 활성화 제어
    public void Active_ShopBtn(bool activeSelf = true)
    {
        buildingShopBtn.gameObject.SetActive(activeSelf);
    }
    public void Active_RankingBtn(bool activeSelf = true)
    {
        rankingBoard.gameObject.SetActive(activeSelf);
    }

    #region ButtonEvent

    //게임 들어가기
    public void OnClick_GameIn()
    {
        OnClick_GameInExit();
        InGame.openApp(GameManager.Inst.curGameName);
    }

    //게임 들어가기 취소
    public void OnClick_GameInExit()
    {
        //카메라 위치 초기화
        GameManager.Inst.GetCameraMove.CameraPosMove(null,false);

        GameManager.Inst.GetClickManager.GetCurHitObject.SetSelectCheck(false);

        //클릭된 오브젝트 초기화
        GameManager.Inst.GetClickManager.BuildingRefresh();

        Active_GameInBtn(false);
        Active_ShopBtn(true);
        GameManager.Inst.GetClickManager.Active_Effect(false);
    }

    //상점 ui
    public void OnClick_BuildingShop()
    {
        shopBoard.ActiveControll();

        Active_ShopBtn(false);
    }

    //빌딩모드로 이동
    public void On_Click_BuildingMode()
    {

        ModeChange(true, false);

        padBoard.ActivePadByType(GameManager.Inst.GetClickManager.GetCurData.myType);

        GameManager.Inst.Call_IntractableObj_Method();

        ModeChangeSquence(false, true, blackColor);
    }

    //편집모드로 이동
    public void On_Click_WaitingMode()
    {
        ModeChange(false, true);

        Vector2 pos = inventoryRect.anchoredPosition;
        pos.y = upPos;
        inventoryRect.anchoredPosition = pos;

        ModeChangeSquence(true,false,blackColor);
    }

    //모드 나가기
    public void OnClick_ModeExit()
    {
        ModeChange(false, false);

        Active_ShopBtn();

        ModeChangeSquence(false, false, Color.white);
    }

    //모드 나가고 상점 이동
    public void OnClick_ModeExit_Shop()
    {
        OnClick_ModeExit();

        OnClick_BuildingShop();
    }

    //신변경
    public void OnClick_Go_2DTown()
    {
        GameManager.Inst.SaveData();
        GameManager.Inst.ListClear();

        //신 변경
        SceneManager.LoadScene("2.BaseTown");
    }

    public void Onclick_Save()
    {
        //데이터 저장
        GameManager.Inst.SaveData();
    }

    public void Onclick_GameExit()
    {
        //데이터 저장
        GameManager.Inst.SaveData();

        //데이터가 저장된 후 나가기
        StartCoroutine(WaitForSaveData());
    }

    public void OnClick_Ranking()
    {
        Active_RankingBtn();
    }
    


    #endregion


    private IEnumerator WaitForSaveData()
    {
        //기다리는 동안 ui?
        yield return new WaitUntil(() => !DatabaseAccess.Inst.isProcessing);

        InGame.ExitGame();

    }

    //모드 컨트롤
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

    //모드가 변경되면서 실행될 시퀀스
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

    //인벤토리 상하 이동
    public void OnClick_InvenMove()
    {
       
        if(clickCount==0)
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

    public void Active_BuildingName(Interactable obj)
    {
        foreach (Transform item in GameManager.Inst.GetBuildings)
        {
            if (item.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.Active_Name(false);
            }
        }

        GameManager.Inst.curGameName = obj.GetMyData.packageName;
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
