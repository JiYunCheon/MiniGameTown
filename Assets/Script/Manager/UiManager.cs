using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    #region Member

    [Header("Button")]
    [SerializeField] private GameObject gameInBtn       = null;
    [SerializeField] private Button buildingShopBtn = null;

    [Header("ShopUi")]
    [SerializeField] private ShopBoard shopBoard        = null;
    [SerializeField] private GameObject modeBtnUi       = null;
    [SerializeField] private GameObject homeUi       = null;

    [Header("MapLevel")]
    [SerializeField] private Material floorMaterial     = null;
    [SerializeField] private PadController padBoard        = null;

    [Header("Result Ui")]
    [SerializeField] private GameObject successWindow = null;
    [SerializeField] private GameObject failedWindow = null;

    [SerializeField] private TextMeshProUGUI gameMoneyText = null;
    //CheckValue
    private bool uiCheck = false;
    private int clclickCount = 0;

    Color blackColor = new Color(113f / 255f, 113f / 255f, 113f / 255f);

    [SerializeField] private Transform invenContentTr = null;
    [SerializeField] private RectTransform inventoryRect = null;

    private ContentItem cur_Content_Item = null;

    public Transform GetInvenContentTr { get { return invenContentTr; } private set { } }


    #endregion

    #region Property

    public bool GetUiCheck { get { return uiCheck; } set { uiCheck = value; } }

    public ShopBoard GetShopBoard { get { return shopBoard; } private set { } }

    #endregion

    private void Awake()
    {
        floorMaterial.color = Color.white;
        InputGameMoney(GameManager.Inst.GetPlayerData.GameMoney.ToString());
        invenBtnImage = new Sprite[2];
        invenBtnImage[0] = Resources.Load<Sprite>("Shop&Inventory_Image/Inven/iven_up");
        invenBtnImage[1] = Resources.Load<Sprite>("Shop&Inventory_Image/Inven/iven_down");

    }

    public void InputGameMoney(string gameMoneyText)
    {
        this.gameMoneyText.text = gameMoneyText;
    }

    public void Set_Content_Item(ContentItem item)
    {
        cur_Content_Item = item;
    }

    //�ǹ��� ���� �Ǿ����� üũ    
    public void SeclectStateUi(bool check)
    {
        if (check)
            buildingShopBtn.interactable = false;
        else
            buildingShopBtn.interactable = true;
    }

    public void Active_HomeUi(bool activeSelf = true)
    {
        homeUi.SetActive(activeSelf);
    }


    //������ Ui Ȱ��ȭ ����
    public void Active_GameInBtn(bool activeSelf = true)
    {
        if(activeSelf)
        {
            gameInBtn.SetActive(activeSelf);
        }
        else
        {
            gameInBtn.SetActive(activeSelf);

            foreach (Transform item in GameManager.Inst.GetClickManager.GetBuildings)
            {
                if (item.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    interactable.Active_Name(true);
                }
            }
            GameManager.Inst.GetUiManager.Active_HomeUi(true);
        }

    }

    //�е� Ȱ��ȭ ����
    public void Active_Pad(bool activeSelf = true)
    {
        padBoard.gameObject.SetActive(activeSelf);
        //padBoard.ActivePadByType(cur_Content_Item.GetMyData.MyType);
    }

    //ShopBoardUi Ȱ��ȭ ����
    public void Active_ShopBtn(bool activeSelf = true)
    {
        buildingShopBtn.gameObject.SetActive(activeSelf);
    }

    public void Active_S_Window(bool active = true)
    {
        successWindow.SetActive(active);
    }

    public void Active_F_Window(bool active = true)
    {
        failedWindow.SetActive(active);
    }

    #region ButtonEvent

    public void OnClick_GameIn()
    {
        OnClick_Exit();
        InGame.openApp(GameManager.Inst.curGameName);
    }

    public void OnClick_Exit()
    {
        GameManager.Inst.GetCameraMove.CameraPosMove(null,false);

        GameManager.Inst.GetClickManager.GetCurHitObject.GetEntrance.ActiveCollider(false);
        GameManager.Inst.GetClickManager.BuildingRefresh();
        GameManager.Inst.GetClickManager.GetCurHitObject.DeSelect_Select_InteractableObj();

        SeclectStateUi(false);
        Active_GameInBtn(false);
        Active_ShopBtn(true);
    }

    public void OnClick_BuildingShop()
    {
        GetUiCheck = true;
        shopBoard.ActiveControll();

        Active_ShopBtn(false);
    }

    public void On_Click_BuildingMode()
    {
        GameManager.Inst.ChangeMode(out GameManager.Inst.waitingMode, false);
        GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode, true);

        padBoard.ActivePadByType(GameManager.Inst.GetClickManager.GetCurData.MyType);
        //Ŭ���Ŵ�����
        foreach (Transform item in GameManager.Inst.GetClickManager.GetBuildings)
        {
            if (item.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.ChangeState(true,Color.red);
            }
        }
        ModeControll(false, true, blackColor);
    }

    public void On_Click_WatingMode()
    {
        GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode, false);
        GameManager.Inst.ChangeMode(out GameManager.Inst.waitingMode, true);

        Vector2 pos = inventoryRect.anchoredPosition;
        pos.y = upPos;
        inventoryRect.anchoredPosition = pos;

        ModeControll(true,false,blackColor);
    }

    public void OnClick_ModeExit()
    {
        GetUiCheck = false;

        GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode, false);
        GameManager.Inst.ChangeMode(out GameManager.Inst.waitingMode, false);

        Active_ShopBtn();

        ModeControll(false, false, Color.white);

    }

    public void OnClick_ModeExit_Shop()
    {
        GetUiCheck = false;

        GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode, false);
        GameManager.Inst.ChangeMode(out GameManager.Inst.waitingMode, false);

        Active_ShopBtn();

        ModeControll(false, false, Color.white);

        OnClick_BuildingShop();
    }

    public void OnClick_PurchaseSuccess()
    {
        GameManager.Inst.GetUiManager.On_Click_WatingMode();
        GameManager.Inst.GameMoneyControll(cur_Content_Item.GetMyData.Price);
        InputGameMoney(GameManager.Inst.GetPlayerData.GameMoney.ToString());


        cur_Content_Item.CompareSoldOutCheck();

        cur_Content_Item.GetItem.InventoryCount(1);

        Active_S_Window(false);

    }

    public void OnClick_Cancel()
    {
        Active_S_Window(false);
        Active_F_Window(false);
    }


    public void OnClick_Go_2DTown()
    {
        SceneManager.LoadScene("2DTown");
    }


    #endregion


    private void ModeControll(bool modeBtn,bool pad,Color color)
    {
        shopBoard.ActiveControll(false);
        floorMaterial.color = color;
        modeBtnUi.SetActive(modeBtn);
        bool check = !GameManager.Inst.waitingMode && !GameManager.Inst.buildingMode;
        homeUi.SetActive(check);

        GameManager.Inst.GetUiManager.Active_Pad(pad);
        GameManager.Inst.GetCameraMove.ChangCameraSize();
    }

    Coroutine moveCoroutine;
    [SerializeField] private Sprite[] invenBtnImage;
    [SerializeField] private Image invenUpDownBtn;
    [SerializeField] private float upPos = 0;
    [SerializeField] private float downPos = 0;


    public void OnClick_InvenMove()
    {
       
        if(clclickCount==0)
        {
            StopAllCoroutines();
            Debug.Log(invenBtnImage[0]);
            invenUpDownBtn.sprite = invenBtnImage[0];
            moveCoroutine = StartCoroutine(Move(downPos));
            clclickCount++;
        }
        else if(clclickCount==1)
        {
            StopAllCoroutines();
            Debug.Log(invenBtnImage[1]);
            invenUpDownBtn.sprite = invenBtnImage[1];
            moveCoroutine = StartCoroutine(Move(upPos));
            clclickCount = 0;
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

}
