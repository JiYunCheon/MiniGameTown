using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContentItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameNameText = null;
    [SerializeField] private TextMeshProUGUI priceText = null;
    [SerializeField] private Button priceBtn = null;

    [SerializeField] private int curPrice = 0;
    [SerializeField] private int occupyPad = 0;
    [SerializeField] private string gameName = null;

    [SerializeField] private InteractionObject prefab = null;
    [SerializeField] private Transform alphaPrefab = null;

    [SerializeField] private GAMETYPE myType;


    public InteractionObject GetPrefab { get { return prefab; } private set { } }

    private void Awake()
    {
        Initialized();
    }

    private void OnEnable()
    {
        switch (myType)
        {
            case GAMETYPE.BALLOON:
                if(GameManager.Inst.GetGameData.balloon_B_Count<=0)
                {
                    priceText.text = $"Already this";
                    priceBtn.interactable = false;
                }
                break;
            case GAMETYPE.FINDPICTURE:
                if (GameManager.Inst.GetGameData.find_B_Count <= 0)
                {
                    priceText.text = $"Already this";
                    priceBtn.interactable = false;
                }
                break;
            case GAMETYPE.MEMORYCARD:
                if (GameManager.Inst.GetGameData.memory_B_Count <= 0)
                {
                    priceText.text = $"Already this";
                    priceBtn.interactable = false;
                }
                break;
            case GAMETYPE.PUZZLE:
                if (GameManager.Inst.GetGameData.puzzle_B_Count <= 0)
                {
                    priceText.text = $"Already this";
                    priceBtn.interactable = false;
                }
                break;
        }
    }

    private void Initialized()
    {
        gameNameText.text = gameName;
        priceText.text = $"{curPrice}$";
    }
    

    public void OnClick_Price()
    {
        if (curPrice < GameManager.Inst.GetGameData.gameMoney)
        {
            GameManager.Inst.GetUiManager.GetShopBoard.Active_S_Window();
            GameManager.Inst.GetClickManager.SetPrefab(alphaPrefab,prefab,occupyPad);
            GameManager.Inst.GetUiManager.GetShopBoard.TypeChange(myType);
        }
        else
            GameManager.Inst.GetUiManager.GetShopBoard.Active_F_Window();

        
    }


}
