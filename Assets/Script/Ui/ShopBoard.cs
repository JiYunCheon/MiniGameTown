using UnityEngine;
using UnityEngine.UI;

public class ShopBoard : MonoBehaviour
{
    [Header("ScriptableObject")]
    [SerializeField] Data[] datas = null;
    [SerializeField] private ContentItem contentPrefab = null;

    [Header("Scroll View")]
    [SerializeField] private ScrollRect Building_Scroll = null;
    [SerializeField] private ScrollRect Object_Scroll = null;


    private void Awake()
    {
        Inst_ContentItem(datas);
        Inst_InvenItem();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        
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

    public void OnClick_Exit()
    {
        GameManager.Inst.GetUiManager.GetUiCheck = false;
        GameManager.Inst.GetUiManager.Active_ShopBtn();
        GameManager.Inst.GetUiManager.OnClick_Cancel();
        ActiveControll(false);
    }

    public void OnClick_Toggle_Building()
    {
        Scroll_OnOff(true);
    }

    public void OnClick_Toggle_Object()
    {
        Scroll_OnOff(false);
    }

   

    public void OnClick_WatingMode()
    {
        GameManager.Inst.GetUiManager.On_Click_WatingMode();
    }

    #endregion

    private void Scroll_OnOff(bool active)
    {
        Building_Scroll.gameObject.SetActive(active);
        Object_Scroll.gameObject.SetActive(!active);
    }

    private void Inst_ContentItem(Data[] datas)
    {
        Transform scrollTr = null;

        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].MyType == OBJECT_TYPE.BUIDING)
            {
                scrollTr = Building_Scroll.content.transform;
                ContentItem content = Instantiate<ContentItem>(contentPrefab, scrollTr);
                content.SetMyData(datas[i]);
            }
            else
            {
                scrollTr = Object_Scroll.content.transform;
                ContentItem content = Instantiate<ContentItem>(contentPrefab, scrollTr);
                content.SetMyData(datas[i]);
            }
                

            
        }

    }






    private void Inst_InvenItem()
    {
        foreach (Transform item in Building_Scroll.content.transform)
        {
            if(item.TryGetComponent<ContentItem>(out ContentItem content))
            {
                content.GenerateContent();
            }
        }

        foreach (Transform item in Object_Scroll.content.transform)
        {
            if (item.TryGetComponent<ContentItem>(out ContentItem content))
            {
                content.GenerateContent();
            }
        }

    }
  

}
