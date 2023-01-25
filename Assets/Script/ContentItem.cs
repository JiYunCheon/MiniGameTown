using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameNameText = null;
    [SerializeField] private TextMeshProUGUI priceText = null;
    [SerializeField] private GameObject successWindow = null;
    [SerializeField] private GameObject failedWindow = null;
    [SerializeField] private int curPrice = 0;

    [SerializeField] private string gameName = null;

    private void Awake()
    {
        Initialized();
    }

    private void Initialized()
    {
        gameNameText.text = gameName;
        priceText.text = curPrice.ToString();
    }


    public void OnClick_Price()
    {
        if (curPrice < GameManager.Inst.GetGameData.gameMoney)
            GameManager.Inst.GetUiManager.GetShopBoard.Active_S_Window();
        else
            GameManager.Inst.GetUiManager.GetShopBoard.Active_F_Window();
    }


}
