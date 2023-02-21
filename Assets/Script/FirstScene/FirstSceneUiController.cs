using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FirstSceneUiController : MonoBehaviour
{
    [SerializeField] private GameObject isGameInButton = null;
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

    public void ActiveButton(bool check = true)
    {
        isGameInButton.SetActive(check);
    }

    public void OnClick_Exit()
    {
        Debug.Log("¿ä¿ì");
        firstSceneClick.GetSaveObj.Refresh();
        ActiveButton(false);
    }

    public void OnClick_GameIn()
    {
        if (GameManager.Inst.curGameName == "Town1")
            SceneManager.LoadScene("Town1");
        else
        {
            OnClick_Exit();
            InGame.openApp(GameManager.Inst.curGameName);
        }
    }


}
