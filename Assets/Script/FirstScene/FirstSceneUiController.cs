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
    [SerializeField] private TextMeshProUGUI userProfileText = null;

    private void Start()
    {
        InputGameMoney(DatabaseAccess.Inst.loginUser.gamemoney.ToString());
        UserInfo(DatabaseAccess.Inst.loginUser.nickname);
    }

    public void InputGameMoney(string gameMoneyText)
    {
        this.gameMoneyText.text = gameMoneyText;
    }

    public void UserInfo(string nickName)
    {
        this.userProfileText.text = nickName;
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
        if (GameManager.Inst.curGameName == "3.MiniTown")
            SceneManager.LoadScene("3.MiniTown");
        else
        {
            OnClick_Exit();
            InGame.openApp(GameManager.Inst.curGameName);
        }
    }


}
