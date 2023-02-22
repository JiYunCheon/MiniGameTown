using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Inst;
    public void Awake()
    {
        Inst = this;
    }

    public int curStar;
    public Text starTxt;

    public List<Scriptable_Item> itemLis = new List<Scriptable_Item>();
    List<Button_Item> btnLis = new List<Button_Item>();

    public Transform ScrollViewContent;

    [Header("BuyPanel")]
    [SerializeField] GameObject buyPanel;
    [SerializeField] Image itemImage;
    [SerializeField] Text itemDescription;
    [SerializeField] Text itemCost;
    Scriptable_Item curItem;
    
    private void Start()
    {
        for(int i = 0; i < itemLis.Count; i++)
        {
            GameObject temp = itemLis[i].createButtonFromItem();
            temp.transform.SetParent(ScrollViewContent);
            btnLis.Add(temp.GetComponent<Button_Item>());
        }
        updateUI();
    }


    void updateUI()
    {
        starTxt.text = "º°: " + curStar.ToString();
    }

    public void openBuyPanel(Scriptable_Item data)
    {
        curItem = data;
        itemImage.sprite = data.sp;
        itemDescription.text = data.description;
        itemCost.text = "Cost: " + data.cost;
        buyPanel.SetActive(true);
    }


    #region Btn
    public void Btn_closeBuyPanel()
    {
        buyPanel.SetActive(false);
    }
    public void Btn_buyItem()
    {
        curStar -= curItem.cost;
        FarmData.Inst.myItemLis.Add(curItem);
        updateUI();
    }

    public void Btn_categoryAll()
    {
        for (int i = 0; i < btnLis.Count; i++)
        {
            btnLis[i].gameObject.SetActive(true);
        }
        updateUI();
    }
    public void Btn_categoryPet()
    {
        for (int i = 0; i < btnLis.Count; i++)
        {
            if(btnLis[i].getType().Equals(ITEMTYPE.PET)) btnLis[i].gameObject.SetActive(true);
            else btnLis[i].gameObject.SetActive(false);
        }
        updateUI();
    }

    public void Btn_cetegoryPlant()
    {
        for (int i = 0; i < btnLis.Count; i++)
        {
            if (btnLis[i].getType().Equals(ITEMTYPE.PLANT)) btnLis[i].gameObject.SetActive(true);
            else btnLis[i].gameObject.SetActive(false);
        }
        updateUI();
    }

    public void Btn_cetegoryOrnament()
    {
        for (int i = 0; i < btnLis.Count; i++)
        {
            if (btnLis[i].getType().Equals(ITEMTYPE.ORNAMENT)) btnLis[i].gameObject.SetActive(true);
            else btnLis[i].gameObject.SetActive(false);
        }
        updateUI();
    }

    public void Btn_cetegoryBackground()
    {
        for (int i = 0; i < btnLis.Count; i++)
        {
            if (btnLis[i].getType().Equals(ITEMTYPE.BACKGROUND)) btnLis[i].gameObject.SetActive(true);
            else btnLis[i].gameObject.SetActive(false);
        }
        updateUI();
    }

    public void Btn_StartMyFarm()
    {
        SceneManager.LoadScene("MyFarm");
    }
    #endregion
}
