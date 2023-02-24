using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FirstSceneUiController : MonoBehaviour
{
    [SerializeField] private FirstSceneClick firstSceneClick = null;
    [SerializeField] private TextMeshProUGUI gameMoneyText = null;

    private void Start()
    {
        InputGameMoney(DatabaseAccess.Inst.loginUser.gamemoney.ToString());
    }

    public void InputGameMoney(string gameMoneyText)
    {
        this.gameMoneyText.text = gameMoneyText;
    }

    
    public void OnClick_Exit()
    {
        InGame.ExitGame();
    }

    public void OnClick_MyHome()
    {
        SceneManager.LoadScene("MyFarm");
    }

}
