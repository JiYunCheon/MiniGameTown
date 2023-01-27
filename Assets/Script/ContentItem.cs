using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContentItem : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI gameNameText = null;
    [SerializeField] private TextMeshProUGUI priceText = null;

    [Header("Button")]
    [SerializeField] private Button priceBtn = null;

    #region GameData

    [SerializeField] private int curPrice = 0;
    [SerializeField] private int occupyPad = 0;
    [SerializeField] private string gameName = null;
    [SerializeField] private InteractionObject prefab = null;
    [SerializeField] private PreviewObject alphaPrefab = null;
    [SerializeField] private GAMETYPE myType;
    [SerializeField] private string spriteName = null;

    #endregion


    private void Awake()
    {
        Initialized();
    }

    private void OnEnable()
    {
        switch (myType)
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

    private void Initialized()
    {
        gameNameText.text = gameName;
        priceText.text = $"{curPrice}$";
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
        if (curPrice < GameManager.Inst.GetGameData.gameMoney)
        {
            GameManager.Inst.GetUiManager.GetShopBoard.Active_S_Window();
            GameManager.Inst.GetUiManager.GenerateContent(alphaPrefab, prefab, occupyPad,myType, spriteName);
        }
        else
            GameManager.Inst.GetUiManager.GetShopBoard.Active_F_Window();
        
    }



}
