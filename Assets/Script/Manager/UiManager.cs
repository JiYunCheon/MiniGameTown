using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GAMETYPE
{
    FINDPICTURE,
    MEMORYCARD,
    BALLOON,
    PUZZLE,
    OBJECT
}

public class UiManager : MonoBehaviour
{
    #region Member

    [Header("Button")]
    [SerializeField] private GameObject gameInBtn       = null;
    [SerializeField] private Button buildingShopBtn = null;

    [Header("ShopUi")]
    [SerializeField] private ShopBoard shopBoard        = null;
    [SerializeField] private GameObject modeBtnUi       = null;

    [Header("MapLevel")]
    [SerializeField] private Material floorMaterial     = null;
    [SerializeField] private GameObject padBoard        = null;

    [Header("Result Ui")]
    [SerializeField] private GameObject successWindow = null;
    [SerializeField] private GameObject failedWindow = null;

    //CheckValue
    private bool uiCheck = false;
    private bool selecCheck = false;
    private int clclickCount = 0;

    Color blackColor = new Color(113f / 255f, 113f / 255f, 113f / 255f);

    [SerializeField] private Transform invenContentTr = null;
    [SerializeField] private RectTransform inventoryRect = null;

    private ContentItem cur_Content_Item = null;
    private InventoryItem cur_Inven_Item = null;

    public InventoryItem GetCur_Inven_Item { get { return cur_Inven_Item; } set { cur_Inven_Item = value; } } 
    public Transform GetInvenContentTr { get { return invenContentTr; } private set { } }


    #endregion

    #region Property

    public bool GetSelecCheck { get { return selecCheck; } private set { } }

    public bool GetUiCheck { get { return uiCheck; } set { uiCheck = value; } }

    public ShopBoard GetShopBoard { get { return shopBoard; } private set { } }

    #endregion

    private void Awake()
    {
        floorMaterial.color = Color.white;
        shopBoard.InstInvenItem();
    }

    public void Set_Content_Item(ContentItem item)
    {
        cur_Content_Item = item;
    }

    public void Set_Inven_Item(InventoryItem item)
    {
        cur_Inven_Item = item;
    }

    //�ǹ��� ���� �Ǿ����� üũ    
    public void ChangeSelecChcek(bool check)
    {
        selecCheck = check;
        if (selecCheck == true)
            buildingShopBtn.interactable = false;
        else
            buildingShopBtn.interactable = true;
    }

    //������ Ui Ȱ��ȭ ����
    public void Active_GameInBtn(bool activeSelf = true)
    {
        gameInBtn.SetActive(activeSelf);
    }

    //�е� Ȱ��ȭ ����
    public void Active_Pad(bool activeSelf = true)
    {
        padBoard.SetActive(activeSelf);
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
        GameManager.Inst.GetClickManager.GetBeforeHit.GetEntrance.ActiveCollider(false);
        GameManager.Inst.GetClickManager.BuildingRefresh();

        ChangeSelecChcek(false);
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

        foreach (Transform item in GameManager.Inst.GetClickManager.GetBuildings)
        {
            if (item.TryGetComponent<Interactable>(out Interactable interactable))
            {
                for (int i = 0; i < interactable.myGround.Count; i++)
                {
                    interactable.myGround[i].ChangeBuildingState(true, Color.red);
                }
            }
        }
        ModeControll(false, GameManager.Inst.buildingMode, true, blackColor);
    }

    public void On_Click_WatingMode()
    {
        GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode, false);
        GameManager.Inst.ChangeMode(out GameManager.Inst.waitingMode, true);


        



        ModeControll(true,GameManager.Inst.waitingMode,false,blackColor);
    }

    public void OnClick_ModeExit()
    {
        GetUiCheck = false;

        GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode, false);
        GameManager.Inst.ChangeMode(out GameManager.Inst.waitingMode, false);

        Active_ShopBtn();

        ModeControll(false, false, false, Color.white);

    }

    public void OnClick_PurchaseSuccess()
    {
        GameManager.Inst.GetUiManager.On_Click_WatingMode();

        cur_Content_Item.CompareSoldOutCheck();

        cur_Content_Item.GetItem.CountControll(1);

        Active_S_Window(false);

    }

    public void OnClick_Cancel()
    {
        Active_S_Window(false);
        Active_F_Window(false);
    }

    #endregion


    private void ModeControll(bool modeBtn, bool mode,bool pad,Color color)
    {
        shopBoard.ActiveControll(false);
        floorMaterial.color = color;
        modeBtnUi.SetActive(modeBtn);

        GameManager.Inst.GetUiManager.Active_Pad(pad);
        GameManager.Inst.GetCameraMove.ChangCameraSize(mode);
    }


    public void OnClick_InvenMove()
    {
       
        if(clclickCount==0)
        {
            StopAllCoroutines();
            StartCoroutine(Move(-695f));
            clclickCount++;
        }
        else if(clclickCount==1)
        {
            StopAllCoroutines();
            StartCoroutine(Move(-365f));
            clclickCount = 0;
        }
    }

    IEnumerator Move(float ypos)
    {
        Vector2 pos = inventoryRect.anchoredPosition;

        while(true)
        {
            pos.y = Mathf.Lerp(inventoryRect.anchoredPosition.y,ypos,2.5f*Time.deltaTime);
            inventoryRect.anchoredPosition = pos;

            if(ypos==-365f && inventoryRect.anchoredPosition.y> -360f)
            {
                pos.y = -365f;
                inventoryRect.anchoredPosition = pos;
                yield break;
            }
            else if (ypos == -695f && inventoryRect.anchoredPosition.y < -690f)
            {
                pos.y = -695f;
                inventoryRect.anchoredPosition = pos;
                yield break;
            }

            yield return null;
        }
    }

}
