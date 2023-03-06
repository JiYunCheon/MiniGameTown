using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneUi : MonoBehaviour
{
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
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        InGame.ExitGame();
    }

    public void OnClick_BaseTown()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        //½Å º¯°æ
        SceneManager.LoadScene("2.BaseTown");
    }
}
