using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RankingUiData : MonoBehaviour
{
    #region Refrence Member

    [Header("======Top User Text======")]
    [SerializeField] private TopUser topUser = null;
    [Header("")]

    [Header("======Ranking Boad Name======")]
    [SerializeField] private UiScore myScoreInfo = null;
    [Header("")]

    [Header("======Type By Difficulty======")]
    [SerializeField] private ScrollRect[] scrollRect = null;
    [Header("")]

    [Header("======Difficulty Board======")]
    [SerializeField] private GameObject[] difByBoard = null;
    private GameObject curBoard = null;
    [Header("")]

    #endregion

    #region Member

    [Header("======Sensetive value======")]
    [SerializeField] private float size = 0;

    [Header("======UserRank Prefab======")]
    [SerializeField] private UiScore rankingContentfrefab = null;

    #endregion


    private List<UiScore> contents = new List<UiScore>();

    private int curGameNum = 0;
    private int curDifNum = 0;

    private void OnEnable()
    {
        SoundManager.Inst.PlayBGM("BGM_Ranking");
        Onclick_BalloonToggle();
    }



    public void GenerateRanking(int gameNum, int difficulty, List<UserData> userdata)
    {
        ScrollRect scroll = scrollRect[difficulty];

        DestrouContents();

        curBoard = difByBoard[difficulty];
        curBoard.SetActive(true);

        UserData.sortingIdx = gameNum * 4 + difficulty;
        userdata.Sort();

        if (gameNum != 1 && gameNum != 3)
            userdata.Reverse();

        int instNum = 0;
        int saveMyIndex = 0;
        for (int i = 0; i < userdata.Count; i++)
        {
            if (float.Parse(userdata[i].score[gameNum * 4 + difficulty]) == 0)
            {
                if (userdata[i].id == DatabaseAccess.Inst.loginUser.id)
                    myScoreInfo.init(0, userdata[i].nickname, "0", userdata[i].selectNum, gameNum);
                continue;
            }

            UiScore rankingContent = Instantiate<UiScore>(rankingContentfrefab, scroll.content.transform);
            rankingContent.init(instNum + 1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty], userdata[i].selectNum, gameNum);
            rankingContent.charNum = userdata[i].selectNum;
            contents.Add(rankingContent);

            if (userdata[i].id == DatabaseAccess.Inst.loginUser.id)
            {
                saveMyIndex = instNum;
                myScoreInfo.init(instNum + 1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty], userdata[i].selectNum, gameNum);
            }

            instNum++;
        }
        StartCoroutine(MoveScroll(difficulty, saveMyIndex));

        TopUser();

    }

    private IEnumerator MoveScroll(int difNum, int instNum)
    {
        ScrollRect scroll = scrollRect[difNum];

        float value = size * instNum / (((scroll.content.childCount * size) - 20) - scroll.GetComponent<RectTransform>().rect.height);
        float scrollvalue = (1 - value) + scroll.verticalScrollbar.size / 2;

        yield return new WaitForSeconds(0.5f);
        scroll.verticalNormalizedPosition = scrollvalue;
    }


    private void TopUser()
    {

        string[] name = new string[3];
        int[] charNum = new int[3];

        for (int i = 0; i < charNum.Length; i++)
        {
            charNum[i] = -1;
        }

        for (int i = 0; i < 3; i++)
        {
            if (i > contents.Count-1) break;


            name[i] = contents[i].GetNickNameText;

            if (name[i] == null)
                charNum[i] = 2;
            else
                charNum[i] = contents[i].charNum;

        }

        topUser.SetNickName(name[0], name[1], name[2]);
        topUser.SetImage(charNum[0], charNum[1], charNum[2]);
    }
  

    private void DestrouContents()
    {
        if (contents.Count == 0) return;

        for (int i = 0; i < contents.Count; i++)
        {
            Destroy(contents[i].gameObject);
        }
        contents.Clear();
    }



    public void OnClick_Exit()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");


        if (SceneManager.GetActiveScene().name== "3.MiniTown")
            SoundManager.Inst.PlayBGM("BGM_MiniTown");
        else if (SceneManager.GetActiveScene().name == "2.BaseTown")
            SoundManager.Inst.PlayBGM("BGM_BaseTown");
        else if (SceneManager.GetActiveScene().name == "5.PlazaScene")
            SoundManager.Inst.PlayBGM("BGM_MiniTown");

        if (GameManager.Inst.GetClickManager!=null && GameManager.Inst.GetClickManager.GetCurHitObject !=null)
        {
            GameManager.Inst.GetClickManager.BuildingRefresh();
            GameManager.Inst.GetClickManager.GetCurHitObject.SetSelectCheck(false);
        }

        this.gameObject.SetActive(false);
    }


    public void OnClick_EasyToggle()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");


        if (curBoard != null)
            curBoard.SetActive(false);

        curDifNum = 0;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_NomalToggle()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");


        if (curBoard != null)
            curBoard.SetActive(false);


        curDifNum = 1;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_HardToggle()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");


        if (curBoard != null)
            curBoard.SetActive(false);


        curDifNum = 2;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_VeryHardToggle()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        if (curBoard != null)
            curBoard.SetActive(false);


        curDifNum = 3;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }


    public void Onclick_BalloonToggle()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        curGameNum = 0;

        OnClick_EasyToggle();
    }

    public void Onclick_MemoryCard()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        curGameNum = 1;

        OnClick_EasyToggle();
    }

    public void Onclick_Juice()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        curGameNum = 2;

        OnClick_EasyToggle();
    }

    public void Onclick_PuzzleToggle()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        curGameNum = 3;

        OnClick_EasyToggle();
    }

}
