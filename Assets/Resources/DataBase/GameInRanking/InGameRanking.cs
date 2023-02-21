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

    [Header("======Top User Text======")]
    [SerializeField] private TextMeshProUGUI fistPlaceText   = null;
    [SerializeField] private TextMeshProUGUI secondPlaceText = null;
    [SerializeField] private TextMeshProUGUI thirdPlaceText  = null;
    [Header("")]

    [Header("======Ranking Boad Name======")]
    [SerializeField] private TextMeshProUGUI gameName = null;
    [Header("")]

    [Header("======Type By Difficulty======")]
    [SerializeField] private ScrollRect[] scrollRect = null;
    [Header("")]

    [Header("======Difficulty Board======")]
    [SerializeField] private GameObject[] difByBoard = null;
    [Header("")]

    [Header("======UserRank Prefab======")]
    [SerializeField] private UiScore rankingContentfrefab = null;
    [Header("")]

    [Header("======CurInfo======")]
    [SerializeField] private UiScore myScoreInfo = null;
    [SerializeField] private TextMeshProUGUI gameDifText = null;

    [SerializeField] private string[] difName = null;

    [Header("======Ref Ui======")]
    [SerializeField] private ResultWindow resultWindow = null;
    [SerializeField] private GameObject rankingBoard = null;
    [SerializeField] private TypeOfGameRangking typeOfGameRangking = null;

    #endregion

    #region Member

    private List<UiScore> contents = new List<UiScore>();
    private GameObject curBoard = null;

    private string _id = null;
    private string curGameName = null;
    private int curGameNum = 0;
    private int curDifNum = 0;
    private float curScore = 0;

    #endregion

    private void Start()
    {
        //ex)
        SetGameInfo("1","ǳ����Ʈ����",0,0);
        curScore = 4;
        SaveScore(_id);
    }

    ///////////////////////////���� ���� ���� ����///////////////////////////////////////////// step 1
    private void SetGameInfo(string id,string gameName ,int curGameNum, int curDifNum) 
    {
        gameDifText.text = difName[curDifNum];
        this._id = id;
        this.curGameName = gameName;
        this.curGameNum = curGameNum;
        this.curDifNum = curDifNum;
    }
    ///////////////////////////��ŷ ���� �� ������ ��///////////////////////////////////////////// step 2
    public void SaveScore(string id)
    {
        _id = id;
        StartCoroutine(SaveScoreFromDatabase());
    }
    ///////////////////////////��ü ��ŷ �ҷ��� ��//////////////////////////////////////////////// uiŬ�� ��
    public void CallRanking(string id, string gameName)
    {
        _id = id;
        this.gameName.text = gameName;
        StartCoroutine(CallRangkingBoard());
    }
    //////////////////////////////////////////////////////////////////////////////////////////////

    //������ ���̺� �� Uiǥ�� ����
    private IEnumerator SaveScoreFromDatabase()
    {
        resultWindow.gameObject.SetActive(true);
        resultWindow.SetUserScoreText(curScore);

        typeOfGameRangking.CallRangking();

        yield return new WaitUntil(() => !typeOfGameRangking.isProcessing);

        resultWindow.SetCurRank(CurRank(curGameNum,curDifNum, typeOfGameRangking.totalScoreData));

        typeOfGameRangking.GetUserData_FromDatabase(_id);

        yield return new WaitUntil(() => typeOfGameRangking.loginUser.score.Length  ==16);


        bool saveCheck = false;

        if (curGameNum == 1)
        {
            if (float.Parse(typeOfGameRangking.loginUser.score[curGameNum * 4 + curDifNum]) > curScore||
                float.Parse(typeOfGameRangking.loginUser.score[curGameNum * 4 + curDifNum])==0)
            {
                saveCheck = true;
            }
        }
        else
        {
            if (float.Parse(typeOfGameRangking.loginUser.score[curGameNum * 4 + curDifNum]) < curScore||
                float.Parse(typeOfGameRangking.loginUser.score[curGameNum * 4 + curDifNum]) == 0)
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


        resultWindow.SetBestRank(CurRank(curGameNum, curDifNum, typeOfGameRangking.totalScoreData));

        typeOfGameRangking.loginUser = null;
    }

    //��ŷ���� �ҷ����鼭 ������ ��ü�����͸� ui�� ǥ���ϴ� ����
    private IEnumerator CallRangkingBoard()
    {
        typeOfGameRangking.CallRangking();

        yield return new WaitUntil(()=> !typeOfGameRangking.isProcessing);

        this.gameObject.SetActive(true);
        OnClick_EasyToggle();
    }

    //���� ��ŷ�� ������� �Լ�
    private int CurRank(int gameNum, int difficulty, List<UserData> userdata)
    {
        if (userdata.Count == 0) return 0;

        UserData.sortingIdx = gameNum * 4 + difficulty;
        userdata.Sort();

        if(gameNum==1)
            userdata.Reverse();


        for (int i = 0; i < userdata.Count; i++)
        {
            if (userdata[i].id == _id)
                return i+1;
        }

        return 0;
    }

    //����Ʈ�� �����ϴ� �Լ�
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
                if (userdata[i].id == _id)
                    myScoreInfo.init(0, "����", "0");

                continue;
            }

            UiScore rankingContent = Instantiate<UiScore>(rankingContentfrefab, scroll.content.transform);
            rankingContent.init(instNum + 1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty]);
            contents.Add(rankingContent);

            if (userdata[i].id == _id)
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

    //��ũ�� ��ġ�� �������ִ� �Լ�
    private IEnumerator MoveScroll(int difNum, int instNum)
    {
        ScrollRect scroll = scrollRect[difNum];
        Debug.Log(instNum);
        float value = 170 * instNum / (((scroll.content.childCount * 170) - 20) - scroll.GetComponent<RectTransform>().rect.height);
        float scrollvalue = (1 - value) + scroll.verticalScrollbar.size / 2;

        yield return new WaitForSeconds(0.5f);
        scroll.verticalNormalizedPosition = scrollvalue;
    }

    //top3 ������ ǥ���ϴ� ����
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

    //top3 ������ �̸��� ���ϴ� �Լ�
    private void SetTopUser(string first, string second, string third)
    {
        fistPlaceText.text = first;
        secondPlaceText.text = second;
        thirdPlaceText.text = third;
    }

    //����Ʈ�� �ı��ϴ� �Լ�
    private void DestrouContents()
    {
        if (contents.Count == 0) return;

        SetTopUser("����", "����", "����");

        for (int i = 0; i < contents.Count; i++)
        {
            if (contents[i] == null) continue;

            Destroy(contents[i].gameObject);
        }
        contents.Clear();
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

    #endregion

}
