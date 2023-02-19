using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameRanking : MonoBehaviour
{

    const int balloonNum = 0;
    const int memoryCardNum = 1;
    const int juiceNum = 2;
    const int puzzleNum = 3;


    #region Refrence Member

    [Header("======Top User Text======")]
    [SerializeField] private TextMeshProUGUI fistPlaceText   = null;
    [SerializeField] private TextMeshProUGUI secondPlaceText = null;
    [SerializeField] private TextMeshProUGUI thirdPlaceText  = null;
    [Header("")]

    [Header("======Ranking Boad Name======")]
    [SerializeField] private TextMeshProUGUI gameName = null;
    [SerializeField] private string curGameName = null;
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

    [Header("======UserRank Prefab======")]
    [SerializeField] private UiScore rankingContentfrefab = null;

    #endregion

    private List<UiScore> contents = new List<UiScore>();

    private string _id = null;
    private int curGameNum = 0;
    private int curDifNum = 0;
    private float curScore = 0;

    [SerializeField] private ResultWindow resultWindow = null;
    [SerializeField] private GameObject rankingBoard = null;

    [SerializeField] private TypeOfGameRangking typeOfGameRangking = null;

    ///////////////////////////현재 게임 상태 세팅///////////////////////////////////////////// step 1

    private void SetGameInfo(string id, int curGameNum, int curDifNum) 
    {
        this._id = id;
        this.curGameNum = curGameNum;
        this.curDifNum = curDifNum;
    }

    ///////////////////////////랭킹 저장 및 갱신할 때///////////////////////////////////////////// step 2
    public void SaveScore(string id)
    {
        _id = id;
        StartCoroutine(SaveScoreFromDatabase());
    }

    ///////////////////////////전체 랭킹 불러올 때//////////////////////////////////////////////// ui클릭 시
    public void CallRanking(string id, string gameName)
    {
        _id = id;
        this.gameName.text = gameName;

        StartCoroutine(CallRangkingBoard());
    }
    //////////////////////////////////////////////////////////////////////////////////////////////




    private IEnumerator SaveScoreFromDatabase()
    {
        resultWindow.gameObject.SetActive(true);
        resultWindow.SetUserScoreText(curScore);

        typeOfGameRangking.CallRangking();

        yield return new WaitUntil(() => !typeOfGameRangking.isProcessing);

        Debug.Log(CurRank(curGameNum, curDifNum, typeOfGameRangking.totalScoreData));

        resultWindow.SetCurRank(CurRank(curGameNum,curDifNum, typeOfGameRangking.totalScoreData));

        typeOfGameRangking.GetUserData_FromDatabase(_id);

        yield return new WaitUntil(() => typeOfGameRangking.loginUser.score.Length  ==16);


        bool saveCheck = false;

        if (curGameNum == 1)
        {
            if (float.Parse(typeOfGameRangking.loginUser.score[curGameNum * 4 + curDifNum]) > curScore)
            {
                saveCheck = true;
            }
        }
        else
        {
            if (float.Parse(typeOfGameRangking.loginUser.score[curGameNum * 4 + curDifNum]) < curScore)
            {
                saveCheck = true;
            }
        }

        if (!saveCheck)
        {
            resultWindow.SetBestRank(CurRank(curGameNum, curDifNum, typeOfGameRangking.totalScoreData));
            yield break;
        }

        typeOfGameRangking.Save_FromDatabase(_id,curScore,curGameNum,curDifNum);

        yield return new WaitUntil(() => !typeOfGameRangking.isProcessing);

        typeOfGameRangking.CallRangking();

        yield return new WaitUntil(() => !typeOfGameRangking.isProcessing);


        Debug.Log(CurRank(curGameNum, curDifNum, typeOfGameRangking.totalScoreData));
        resultWindow.SetBestRank(CurRank(curGameNum, curDifNum, typeOfGameRangking.totalScoreData));

        typeOfGameRangking.loginUser = null;
    }

    private IEnumerator CallRangkingBoard()
    {
        typeOfGameRangking.CallRangking();

        yield return new WaitUntil(()=> !typeOfGameRangking.isProcessing);

        this.gameObject.SetActive(true);
        OnClick_EasyToggle();
    }

    //현재 랭킹을 얻기위한 함수
    private int CurRank(int gameNum, int difficulty, List<UserData> userdata)
    {
        Debug.Log(userdata.Count);

        if (userdata.Count == 0) return 0;

        UserData.sortingIdx = gameNum * 4 + difficulty;
        userdata.Sort();
        userdata.Reverse();


        for (int i = 0; i < userdata.Count; i++)
        {
            if (userdata[i].id == _id)
                return i+1;
        }

        return 0;
    }

    //컨텐트를 생성하는 함수
    public void GenerateRanking(int gameNum, int difficulty, List<UserData> userdata)
    {
        ScrollRect scroll = scrollRect[difficulty];
        curBoard = difByBoard[difficulty];
        curBoard.SetActive(true);

        UserData.sortingIdx = gameNum * 4 + difficulty;
        userdata.Sort();
        userdata.Reverse();

        for (int i = 0; i < userdata.Count; i++)
        {
            UiScore rankingContent = Instantiate<UiScore>(rankingContentfrefab, scroll.content.transform);
            rankingContent.init(i + 1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty]);

            contents.Add(rankingContent);

            if (userdata[i].id == _id)
            {
                rankingContent.ChangeColor(Color.cyan);
                myScoreInfo.init(i + 1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty]);
            }
        }

        SetTopUser(userdata[0].nickname, userdata[1].nickname, userdata[2].nickname);
    }

    //탑 3 유저의 이름을 셋하는 함수
    private void SetTopUser(string first, string second, string third)
    {
        fistPlaceText.text = first;
        secondPlaceText.text = second;
        thirdPlaceText.text = third;
    }

    //컨텐트를 파괴하는 함수
    private void DestrouContents()
    {
        if (contents.Count == 0) return;

        for (int i = 0; i < contents.Count; i++)
        {
            if (contents[i] == null) continue;

            Destroy(contents[i].gameObject);
        }
        contents.Clear();
    }

    public void OnClick_RankingBoard()
    {
        rankingBoard.SetActive(true);

        CallRanking(_id,curGameName);
    }

    public void OnClick_Exit()
    {
        rankingBoard.SetActive(false);
    }

    public void OnClick_EasyToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);

        DestrouContents();

        curDifNum = 0;

        GenerateRanking(curGameNum, curDifNum, typeOfGameRangking.totalScoreData);
    }

    public void OnClick_NomalToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);

        DestrouContents();

        curDifNum = 1;

        GenerateRanking(curGameNum, curDifNum, typeOfGameRangking.totalScoreData);
    }

    public void OnClick_HardToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);

        DestrouContents();

        curDifNum = 2;

        GenerateRanking(curGameNum, curDifNum, typeOfGameRangking.totalScoreData);
    }

    public void OnClick_VeryHardToggle()
    {
        if (curBoard != null)
            curBoard.SetActive(false);

        DestrouContents();

        curDifNum = 3;

        GenerateRanking(curGameNum, curDifNum, typeOfGameRangking.totalScoreData);
    }
   


}
