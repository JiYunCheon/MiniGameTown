using UnityEngine;
using UnityEngine.UI;

public class ShopBoard : MonoBehaviour
{
    [Header("ScriptableObject")]
    [SerializeField] private ContentItem contentPrefab = null;

    [Header("Scroll View")]
    [SerializeField] private ScrollRect Building_Scroll = null;
    [SerializeField] private ScrollRect Object_Scroll = null;
    [Header("InventoryContentTr")]
    [SerializeField] private ScrollRect invetoryScroll = null;


    private void Awake()
    {
        //인벤토리와, 상점에 컨텐트 생성
        Inst_ContentItem();
        Inst_InvenItem();

        //비활성화
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnClick_Toggle_Building();
    }

    public void ActiveControll(bool active = true)
    {
        this.gameObject.SetActive(active);
    }

    #region Button Event

    //상점 x 클릭
    public void OnClick_Exit()
    {
        GameManager.Inst.GetUiManager.Active_ShopBtn();
        ActiveControll(false);
    }

    //토클 빌딩 클릭
    public void OnClick_Toggle_Building()
    {
        Scroll_OnOff(true);
    }

    //토클 오브젝트 클릭
    public void OnClick_Toggle_Object()
    {
        Scroll_OnOff(false);
    }
    
    //편집모드 가기 클릭
    public void OnClick_WatingMode()
    {
        GameManager.Inst.GetUiManager.On_Click_WatingMode();
    }

    #endregion


    //토글 켜고 끔
    private void Scroll_OnOff(bool active)
    {
        Building_Scroll.gameObject.SetActive(active);
        Object_Scroll.gameObject.SetActive(!active);
    }

    //상점 컨텐트 생성 로직
    private void Inst_ContentItem()
    {
        Transform scrollTr = null;

        for (int i = 0; i < GameManager.Inst.GetObjectData.Count; i++)
        {
            if (GameManager.Inst.GetObjectData[i].myType == OBJECT_TYPE.BUILDING)
            {
                scrollTr = Building_Scroll.content.transform;
                ContentItem content = Instantiate<ContentItem>(contentPrefab, scrollTr);
                content.SetMyData(GameManager.Inst.GetObjectData[i]);
            }
            else
            {
                scrollTr = Object_Scroll.content.transform;
                ContentItem content = Instantiate<ContentItem>(contentPrefab, scrollTr);
                content.SetMyData(GameManager.Inst.GetObjectData[i]);
            }

        }
    }

    //인벤토리 컨텐트 생성 로직
    private void Inst_InvenItem()
    {
        foreach (Transform item in Building_Scroll.content.transform)
        {
            if(item.TryGetComponent<ContentItem>(out ContentItem content))
            {
                content.GenerateContent(invetoryScroll.content.transform);
            }
        }

        foreach (Transform item in Object_Scroll.content.transform)
        {
            if (item.TryGetComponent<ContentItem>(out ContentItem content))
            {
                content.GenerateContent(invetoryScroll.content.transform);
            }
        }

    }
  

}
