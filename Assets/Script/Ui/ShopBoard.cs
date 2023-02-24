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
        //�κ��丮��, ������ ����Ʈ ����
        Inst_ContentItem();
        Inst_InvenItem();

        //��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnClick_Toggle_Building();
    }

    public void ActiveControll(bool active = true)
    {
        GameManager.Inst.GetUiManager.uiSelectCheck = active;
        this.gameObject.SetActive(active);
    }

    #region Button Event

    //���� x Ŭ��
    public void OnClick_Exit()
    {
        GameManager.Inst.GetUiManager.Active_ShopBtn();
        ActiveControll(false);
    }

    //��Ŭ ���� Ŭ��
    public void OnClick_Toggle_Building()
    {
        Scroll_OnOff(true);
    }

    //��Ŭ ������Ʈ Ŭ��
    public void OnClick_Toggle_Object()
    {
        Scroll_OnOff(false);
    }
    
    //������� ���� Ŭ��
    public void OnClick_WatingMode()
    {
        GameManager.Inst.GetUiManager.On_Click_WaitingMode();
    }

    #endregion


    //��� �Ѱ� ��
    private void Scroll_OnOff(bool active)
    {
        Building_Scroll.gameObject.SetActive(active);
        Object_Scroll.gameObject.SetActive(!active);
    }

    //���� ����Ʈ ���� ����
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
                content.countIndex = i;
            }
            else
            {
                scrollTr = Object_Scroll.content.transform;
                ContentItem content = Instantiate<ContentItem>(contentPrefab, scrollTr);
                content.SetMyData(GameManager.Inst.GetObjectData[i]);
                content.countIndex = i;
            }
        }
    }

    //�κ��丮 ����Ʈ ���� ����
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
