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

    [Header("ModeUi")]
    [SerializeField] private ScrollRect scroll = null;
    [SerializeField] InventoryItem inven_itemPrefab = null;

    [Header("Result Ui")]
    [SerializeField] private GameObject successWindow = null;
    [SerializeField] private GameObject failedWindow = null;

    [SerializeField] private PadSpawner[] padSpawners = null;

    //CheckValue
    private bool uiCheck = false;
    private bool selecCheck = false;

    Color blackColor = new Color(113f / 255f, 113f / 255f, 113f / 255f);

    private InventoryItem cur_Inven_Item = null;
    private ContentItem cur_Content_Item = null;


    #endregion

    #region Property

    public bool GetSelecCheck { get { return selecCheck; } private set { } }

    public bool GetUiCheck { get { return uiCheck; } set { uiCheck = value; } }

    public ShopBoard GetShopBoard { get { return shopBoard; } private set { } }

    #endregion

    private void Awake()
    {
        floorMaterial.color = Color.white;
    }


    public void Set_Inven_Item(InventoryItem item)
    {
        cur_Inven_Item = item;
    }

    public void Set_Content_Item(ContentItem item)
    {
        cur_Content_Item = item;
    }

    public void DestroyItem()
    {
        if (cur_Inven_Item == null) return;
        Destroy(cur_Inven_Item.gameObject);
        cur_Inven_Item = null;
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
        GameManager.Inst.GetClickManager.Refresh();

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
        ModeControll(false, GameManager.Inst.buildingMode, true, blackColor);

        for (int i = 0; i < padSpawners.Length; i++)
        {
            padSpawners[i].GeneratePad(GameManager.Inst.GetClickManager.GetOccupyPad);
        }


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
        cur_Content_Item.CallGenerate();
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

    public void GenerateContent(PreviewObject alphaPrefab, Interactable prefab, int occupyPad, GAMETYPE type,string spriteName)
    {
        InventoryItem obj = Instantiate<InventoryItem>(inven_itemPrefab,scroll.content.transform);

        obj.Initialized(alphaPrefab, prefab, occupyPad, type);
        obj.ChangeImage(spriteName);

        switch (type)
        {
            case GAMETYPE.BALLOON:
                GameManager.Inst.GetGameData.balloon_B_Count--;
                break;
            case GAMETYPE.FINDPICTURE:
                GameManager.Inst.GetGameData.find_B_Count--;
                break;
            case GAMETYPE.MEMORYCARD:
                GameManager.Inst.GetGameData.memory_B_Count--;
                break;
            case GAMETYPE.PUZZLE:
                GameManager.Inst.GetGameData.puzzle_B_Count--;
                break;
        }
    }

}
