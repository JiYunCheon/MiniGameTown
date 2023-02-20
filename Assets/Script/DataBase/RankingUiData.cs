using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    private void OnEnable()
    {
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

        if (gameNum != 1)
            userdata.Reverse();

        int instNum = 0;
        int saveMyIndex = 0;
        for (int i = 0; i < userdata.Count; i++)
        {
            if (float.Parse(userdata[i].score[gameNum * 4 + difficulty]) == 0)
            {
                if (userdata[i].id == DatabaseAccess.Inst.loginUser.id)
                    myScoreInfo.init(0, "¾øÀ½", "0");

                continue;
            }

            UiScore rankingContent = Instantiate<UiScore>(rankingContentfrefab, scroll.content.transform);
            rankingContent.init(instNum + 1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty]);
            contents.Add(rankingContent);

            if (userdata[i].id == DatabaseAccess.Inst.loginUser.id)
            {
                saveMyIndex = instNum;
                rankingContent.ChangeColor(Color.cyan);
                myScoreInfo.init(instNum + 1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty]);
            }

            instNum++;
        }
        StartCoroutine(MoveScroll(difficulty, saveMyIndex));

        TopUser(scroll);

    }

    private IEnumerator MoveScroll(int difNum, int instNum)
    {
        ScrollRect scroll = scrollRect[difNum];
        Debug.Log(instNum);

        float value = size * instNum / (((scroll.content.childCount * size) - 20) - scroll.GetComponent<RectTransform>().rect.height);
        float scrollvalue = (1 - value) + scroll.verticalScrollbar.size / 2;

        yield return new WaitForSeconds(0.5f);
        scroll.verticalNormalizedPosition = scrollvalue;
    }


    private void TopUser(ScrollRect scroll)
    {
        string[] name = new string[3];
        int index = 0;

        for (int i = 0; i < contents.Count; i++)
        {
            if (index > 2) break;

            name[index] = contents[i].GetNickNameText;
            index++;
        }

        SetTopUser(name[0], name[1], name[2]);
    }

    private void SetTopUser(string first, string second, string third)
    {
        fistPlaceText.text = first;
        secondPlaceText.text = second;
        thirdPlaceText.text = third;
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

        curDifNum = 0;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_NomalToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);


        curDifNum = 1;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_HardToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);


        curDifNum = 2;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }

    public void OnClick_VeryHardToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);


        curDifNum = 3;

        GenerateRanking(curGameNum, curDifNum, DatabaseAccess.Inst.totalScoreData);
    }


    public void Onclick_BalloonToggle()
    {
        curGameNum = 0;
        curGameName.text = gameName[curGameNum];

        OnClick_EasyToggle();
    }

    public void Onclick_MemoryCard()
    {
        curGameNum = 1;
        curGameName.text = gameName[curGameNum];

        OnClick_EasyToggle();
    }

    public void Onclick_Juice()
    {
        curGameNum = 2;
        curGameName.text = gameName[curGameNum];

        OnClick_EasyToggle();
    }

    public void Onclick_PuzzleToggle()
    {
        curGameNum = 3;
        curGameName.text = gameName[curGameNum];

        OnClick_EasyToggle();
    }

}
