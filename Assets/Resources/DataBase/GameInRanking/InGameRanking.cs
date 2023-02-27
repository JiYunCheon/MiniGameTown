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

    const int easyDif = 0;
    const int nomalDif = 0;
    const int hardDif = 0;
    const int veryhardDif = 0;

    #region Refrence Member

    [Header("======Ref Ui======")]
    [SerializeField] private ResultWindow resultWindow = null;
    [SerializeField] private GameObject rankingBoard = null;
    

    public static InGameRanking Inst;

    #endregion

    #region Member

    private List<UiScore> contents = new List<UiScore>();

    private string _id = null;
    private string curGameName = null;
    private int curGameNum = 0;
    private int curDifNum = 0;
    private float curScore = 0;
    private int star = 0;

    #endregion

    private void Start()
    {
        Inst = this;
        //ex)
        SetGameInfo("1",150,5,0,0);
        SaveScore(_id);
    }

    ///////////////////////////현재 게임 상태 세팅///////////////////////////////////////////// step 1
    public void SetGameInfo(string id ,int curScore, int star ,int curGameNum, int curDifNum) 
    {
        this._id = id;
        this.star = star;
        this.curScore = curScore;
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
        TypeOfGameRangking.Inst.CallRangking();
    }
    //////////////////////////////////////////////////////////////////////////////////////////////

    //데이터 세이브 및 Ui표시 로직
    private IEnumerator SaveScoreFromDatabase()
    {
        bool saveCheck = false;

        resultWindow.gameObject.SetActive(true);
        resultWindow.SetCurScore(curScore);
        resultWindow.SetStartCount(star);

        TypeOfGameRangking.Inst.GetUserData_FromDatabase(_id);

        yield return new WaitUntil(() => !TypeOfGameRangking.Inst.isProcessing);

        float score = float.Parse(TypeOfGameRangking.Inst.loginUser.score[curGameNum * 4 + curDifNum]);

        resultWindow.SetBeforeScoreText(score);

        if (curGameNum == 1 || curGameNum == 3)
        {
            if (score > curScore ||
                score == 0)
            {
                saveCheck = true;
            }
        }
        else
        {
            if (score < curScore ||
                score == 0)
            {
                saveCheck = true;
            }
        }


        if (saveCheck)
        {
          resultWindow.Actvie_UpRank();
        }
        else
        {
            curScore = score;
        }

        TypeOfGameRangking.Inst.Save_FromDatabase(_id, curScore, star, curGameNum, curDifNum);

        yield return new WaitUntil(() => !TypeOfGameRangking.Inst.isProcessing);

    }
 

    #region Btn Event

    public void OnClick_RankingBoard()
    {
        rankingBoard.SetActive(true);

        CallRanking(_id, curGameName);
    }

    public void OnClick_Exit()
    {
        rankingBoard.SetActive(false);
    }
 

    #endregion

}
