using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RankingUiData : MonoBehaviour
{
    #region Refrence Member

    [Header("======Top User Text======")]
    [SerializeField] private TextMeshProUGUI fistPlaceText = null;
    [SerializeField] private TextMeshProUGUI secondPlaceText = null;
    [SerializeField] private TextMeshProUGUI thirdPlaceText = null;
    [Header("")]

    [Header("======Ranking Boad Name======")]
    [SerializeField] private TextMeshProUGUI curGameName = null;
    [SerializeField] private string[] gameName = null;
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Onclick_BalloonToggle();
        }
    }


    public void GenerateRanking(int gameNum,int difficulty, List<UserData> userdata)
    {
        ScrollRect scroll = scrollRect[difficulty];
        curBoard = difByBoard[difficulty];
        curBoard.SetActive(true);
        curGameName.text = gameName[gameNum];


        UserData.sortingIdx = gameNum*4+ difficulty;
        userdata.Sort();
        userdata.Reverse();
        Debug.Log(userdata.Count);

        for (int i = 0; i < userdata.Count; i++)
        {
            UiScore rankingContent = Instantiate<UiScore>(rankingContentfrefab, scroll.content.transform);
            rankingContent.init(i+1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty]);

            contents.Add(rankingContent);

            if (userdata[i].id == DatabaseAccess.Inst.loginUser.id)
            {
                rankingContent.ChangeColor(Color.cyan);
                myScoreInfo.init(i + 1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty]);
            }
        }
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
        this.gameObject.SetActive(false);
    }


    public void OnClick_EasyToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);

        DestrouContents();

        curDifNum = 0;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_NomalToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);

        DestrouContents();

        curDifNum = 1;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_HardToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);

        DestrouContents();

        curDifNum = 2;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_VeryHardToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);

        DestrouContents();

        curDifNum = 3;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }


    public void Onclick_BalloonToggle()
    {
        curGameNum = 0;

        OnClick_EasyToggle();
    }

    public void Onclick_MemoryCard()
    {
        curGameNum = 1;

        OnClick_EasyToggle();
    }

    public void Onclick_Juice()
    {
        curGameNum = 2;

        OnClick_EasyToggle();
    }

    public void Onclick_PuzzleToggle()
    {
        curGameNum = 3;

        OnClick_EasyToggle();
    }

}
