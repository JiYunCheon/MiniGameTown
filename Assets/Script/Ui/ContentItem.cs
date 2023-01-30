using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContentItem : Item
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI gameNameText = null;
    [SerializeField] private TextMeshProUGUI priceText = null;

    [Header("Button")]
    [SerializeField] private Button priceBtn = null;


    private void OnEnable()
    {
        switch (GetMyData.MyType)
        {
            case GAMETYPE.BALLOON:

                CompareSoldOutCheck(GameManager.Inst.GetGameData.balloon_B_Count);

                break;
            case GAMETYPE.FINDPICTURE:
               
                CompareSoldOutCheck(GameManager.Inst.GetGameData.find_B_Count);

                break;
            case GAMETYPE.MEMORYCARD:
             
                CompareSoldOutCheck(GameManager.Inst.GetGameData.memory_B_Count);

                break;
            case GAMETYPE.PUZZLE:
             
                CompareSoldOutCheck(GameManager.Inst.GetGameData.puzzle_B_Count);

                break;
        }
    }

    protected override void Initialized()
    {
        gameNameText.text = GetMyData.GameName;
        priceText.text = $"{GetMyData.Price}$";
        picture.sprite = Resources.Load<Sprite>(GetMyData.SpriteName);
    }


    private void CompareSoldOutCheck(int count)
    {
        if (count <= 0)
        {
            priceText.text = $"Already this";
            priceBtn.interactable = false;
        }
    }

    public void OnClick_Price()
    {
        if (GetMyData.Price < GameManager.Inst.GetGameData.gameMoney)
        {
            GameManager.Inst.GetUiManager.Active_S_Window();
            GameManager.Inst.GetUiManager.Set_Content_Item(this);
        }
        else
            GameManager.Inst.GetUiManager.Active_F_Window();
        
    }

    public void CallGenerate()
    {
        GameManager.Inst.GetUiManager.GenerateContent(this);
    }

   
}
