using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingUiData : MonoBehaviour
{
    #region Refrence Member

    [Header("======Difficulty Board======")]
    [SerializeField] private GameObject easyBoard = null;
    [SerializeField] private GameObject nomalBoard = null;
    [SerializeField] private GameObject hardBoard = null;
    [SerializeField] private GameObject veryhardBoard = null;
    private GameObject selectBoard = null;
    [Header("")]

    [Header("======Top User Text======")]
    [SerializeField] private TextMeshProUGUI fistPlaceText = null;
    [SerializeField] private TextMeshProUGUI secondPlaceText = null;
    [SerializeField] private TextMeshProUGUI thirdPlaceText = null;
    [Header("")]

    [Header("======Ranking Boad Name======")]
    public TextMeshProUGUI curGameName = null;
    [Header("")]

    [SerializeField] private UiScore upDownObjPrefab = null;
    private UiScore[] upDownObj = null;

    [Header("")]

    [Header("======Type By Difficulty======")]
    public ScrollRect easyScrollRect = null;
    public ScrollRect nomalScrollRect = null;
    public ScrollRect hardScrollRect = null;
    public ScrollRect veryhardScrollRect = null;
    [Header("")]

    #endregion

    #region Member

    [Header("======Sensetive value======")]
    [SerializeField] private float size = 0;
    [Header("======Up&DownUi Pos======")]
    [SerializeField] private Vector2 upUiPos   = Vector2.zero;
    [SerializeField] private Vector2 downUiPos = Vector2.zero;

    [Header("======UserRank Prefab======")]
    [SerializeField] private UiScore infoBox = null;
    [SerializeField] private Transform canvars = null;
    [Header("")]

    //Cur Type
    private GAMETYPE myType;
    private DIFFICULTY myDifficulty;

    //ScrollRect fist Pos
    private float scrollValue = 0;
    private bool isActiveBoard = false;

    private List<UiScore> contentList = null;
    //CurUser Ranking Ui
    private UiScore curUserInfoUi = null;

    private float up = 0;
    private float down = 0;

    #endregion

    private void Awake()
    {
        contentList = new List<UiScore>();
        upDownObj = new UiScore[2];
    }

    private void Start()
    {
        //Onclick_Easy();
    }
    private void Update()
    {
        if (isActiveBoard == true&&!DataBaseServer.Inst.instDone)
        {
            switch (myDifficulty)
            {
                case DIFFICULTY.EASY:
                    UpDownRankingUI(easyScrollRect);
                    break;
                case DIFFICULTY.NORMAL:
                    UpDownRankingUI(nomalScrollRect);
                    break;
                case DIFFICULTY.HARD:
                    UpDownRankingUI(hardScrollRect);
                    break;
                case DIFFICULTY.VERYHARD:
                    UpDownRankingUI(veryhardScrollRect);
                    break;
            }
        }

    }

    public void SetActiveBoardCheck(bool check = true)
    {
        isActiveBoard = check;
    }

    #region 랭킹보드 난이도 선택 토글기능

    public void ActiveEasyBoard(bool active = true)
    {
        selectBoard.gameObject.SetActive(!active);
        easyBoard.SetActive(active);
    }

    public void ActiveNomalBoard(bool active = true)
    {
        selectBoard.gameObject.SetActive(!active);
        nomalBoard.SetActive(active);
    }

    public void ActiveHardBoard(bool active = true)
    {
        selectBoard.gameObject.SetActive(!active);
        hardBoard.SetActive(active);
    }

    public void ActiveVeryHardBoard(bool active = true)
    {
        selectBoard.gameObject.SetActive(!active);
        veryhardBoard.SetActive(active);
    }

    #endregion

    #region 클릭 이벤트 

    public void Onclick_Easy()
    {
        if (selectBoard != null)
            ActiveEasyBoard();

        SelectDifficultyButton(DIFFICULTY.EASY,easyBoard);

        MoveScrollView(easyScrollRect);
    }

    public void Onclick_Nomal()
    {
        if (selectBoard != null)
            ActiveNomalBoard();

        SelectDifficultyButton(DIFFICULTY.NORMAL,nomalBoard);

        MoveScrollView(nomalScrollRect);
    }

    public void Onclick_Hard()
    {
        if (selectBoard != null)
            ActiveHardBoard();

        SelectDifficultyButton(DIFFICULTY.HARD,hardBoard);

        MoveScrollView(hardScrollRect);
    }

    public void Onclick_VeryHard()
    {
        if(selectBoard!=null)
            ActiveVeryHardBoard();

        SelectDifficultyButton(DIFFICULTY.VERYHARD,veryhardBoard);

        MoveScrollView(veryhardScrollRect);
    }

    public void OnClick_Exit_Ranking()
    {
        //스크롤뷰 상 하단 유저 유아이 삭제
        DestroyUpDownUi();

        //보드의 상태를 꺼짐
        SetActiveBoardCheck(false);

        //현재 게임오브젝트를 삭제
        Destroy(this.gameObject);
    }

    #endregion

    private void SelectDifficultyButton(DIFFICULTY dif, GameObject board)
    {
        //버튼 중복 눌림 방지
        if (DataBaseServer.Inst.isProcessing) return;
        DataBaseServer.Inst.isProcessing = true;

        //현재 선택된 보드를 저장
        selectBoard = board;

        //현재 난이도 설정
        myDifficulty = dif;

        //만약 스크롤뷰에 컨탠트가 있다면 컨탠트들을 삭제
        DestroyContent();

        //데이터를 불러옴 
        DataBaseServer.Inst.Call_GetUserInfo();

    }

    //Call Coroutine_ Change ScrollView Vertical Nomalized Pos
    public void MoveScrollView(ScrollRect scrollRect)
    {
        StartCoroutine(MoveSequence(scrollRect));
    }
    private IEnumerator MoveSequence(ScrollRect scrollRect)
    {
        //컨탠트가 생성이 다 될때까지 기다림
        yield return new WaitUntil(() => DataBaseServer.Inst.instDone);

        int targetIndex = 0;
        int count = 0;

        foreach (Transform child in scrollRect.content.transform)
        {
            UiScore uiScore = null;

            if(child.gameObject.TryGetComponent<UiScore>(out uiScore))
            {
                //컨탠트를 제어할 리스트에 컨탠트를 저장
                contentList.Add(uiScore);

                //유저의 컨탠트를 찾음
                if (uiScore.GetNickNameText == DataBaseServer.Inst.loginUser.id)
                {
                    //찾은 유저의 컨탠트를 저장
                    curUserInfoUi = uiScore;

                    //이름 변경
                    curUserInfoUi.gameObject.name = "my";

                    //유저의 컨탠트가 몇 번째에 있는지 저장
                    targetIndex = count;

                    //컨탠트의 색깔을 변경
                    uiScore.ChangeColor(UnityEngine.Color.cyan);
                }
            }

            count++;
        }

        //스크롤뷰의 값이 세팅 될 때까지 대기 
        yield return new WaitForSeconds(0.05f);


        //Debug.Log("사이즈 : "+ size);
        //Debug.Log("타겟 인덱스 : " + targetIndex);
        //Debug.Log("전체 사이즈 크기 : " + (scrollRect.content.childCount * size));
        //Debug.Log("보이는 높이 : " + scrollRect.GetComponent<RectTransform>().rect.height);
        //Debug.Log("스크롤 바 사이즈 : " + scrollRect.verticalScrollbar.size);

        //스크롤뷰의 컨탠트의 포지션을 바꿀 값 계산
        float value = size * targetIndex / (((scrollRect.content.childCount * size) - 20)- scrollRect.GetComponent<RectTransform>().rect.height);

        scrollValue = (1 - value) + scrollRect.verticalScrollbar.size/2;

        //스크롤뷰의 값이 세팅 될 때까지 대기 
        yield return new WaitForSeconds(0.05f);

        //스크롤뷰에 값 적용
        scrollRect.verticalNormalizedPosition = scrollValue;

        up = scrollValue - (scrollRect.verticalScrollbar.size / 2f);
        down = scrollValue + (scrollRect.verticalScrollbar.size*0.6f);

        if (down < 0)
            down = 0;

        //Debug.Log("스크롤 밸류 : "+scrollValue);
        //Debug.Log("up : " + up);
        //Debug.Log("down : " + down);

        //스크롤뷰 상 하단 유아이에 데이타 전달 
        InsertInfoUpDownUi();

        //1,2,3 등 유아이에 데이타 전달
        SetPlaceNickName();

        //변수 초기화
        DataBaseServer.Inst.instDone = false;
    }



    //Ative curUserRanking Info
    public void UpDownRankingUI(ScrollRect scrollRect)
    {
        //생성되지 않았다면 리턴
        if (upDownObj[0] == null|| scrollRect.verticalScrollbar.size==1) return;
       

        //정한 범위를 벗어 나면 상하단 유아이가 출력됨
        if (scrollRect.verticalNormalizedPosition > down)
        {
            upDownObj[1].gameObject.SetActive(true);
            upDownObj[0].gameObject.SetActive(false);
        }
        else if (scrollRect.verticalNormalizedPosition < up)
        {
            upDownObj[0].gameObject.SetActive(true);
            upDownObj[1].gameObject.SetActive(false);
        }
        else
        {
            upDownObj[1].gameObject.SetActive(false);
            upDownObj[0].gameObject.SetActive(false);
        }

    }

    //Clear ContentList
    private void DestroyContent()
    {
        if (contentList.Count > 0)
        {
            for (int i = 0; i < contentList.Count; i++)
            {
                Destroy(contentList[i].gameObject);
            }
            contentList.Clear();

            curUserInfoUi = null;
        }
    }

    //InstUp&DownUi
    private void InsertInfoUpDownUi()
    {
        if (upDownObj[0]==null)
        {
            //상단 유아이 생성
            UiScore uiScore = Instantiate<UiScore>(upDownObjPrefab,this.transform);

            //데이터 적용
            uiScore.init(DataBaseServer.Inst.curRank, curUserInfoUi.GetNickNameText, DataBaseServer.Inst.loginUser.curScore);
            uiScore.GetComponent<RectTransform>().anchoredPosition = upUiPos;

            //배열에 값 저장
            upDownObj[0] = uiScore;

            //색 변경
            upDownObj[0].ChangeColor(UnityEngine.Color.green);

            //하단 유아이 생성
            uiScore = Instantiate<UiScore>(upDownObjPrefab, this.transform);

            //데이터 적용
            uiScore.init(DataBaseServer.Inst.curRank, curUserInfoUi.GetNickNameText, DataBaseServer.Inst.loginUser.curScore);
            uiScore.GetComponent<RectTransform>().anchoredPosition = downUiPos;

            //배열에 값 저장
            upDownObj[1] = uiScore;

            //색 변경
            upDownObj[1].ChangeColor(UnityEngine.Color.green);

            upDownObj[0].gameObject.SetActive(false);
            upDownObj[1].gameObject.SetActive(false);
        }
        else
        {

            Debug.Log(curUserInfoUi.GetNickNameText);
            //이미 존재한다면 값만 저장
            upDownObj[0].init(DataBaseServer.Inst.curRank, curUserInfoUi.GetNickNameText, DataBaseServer.Inst.loginUser.curScore);
            upDownObj[1].init(DataBaseServer.Inst.curRank, curUserInfoUi.GetNickNameText, DataBaseServer.Inst.loginUser.curScore);

            upDownObj[0].gameObject.SetActive(false);
            upDownObj[1].gameObject.SetActive(false);
        }
      
    }

    //Destroy Up&DownUi
    private void DestroyUpDownUi()
    {
        //이미 없다면 리턴
        if (upDownObj[0] == null) return;
    
        //파괴 후 초기화
        for (int i = 0; i < upDownObj.Length; i++)
        {
            Destroy(upDownObj[i].gameObject);
            upDownObj[i] = null;
        }
    }

    //UserUi Inst Logic
    public void InstLogic(DIFFICULTY dif, int index, float score)
    {
        UiScore info = null;
        Transform contentTr = null;
        //생성 될 위치를 난이도별로 저장

        switch (dif)
        {
            case DIFFICULTY.EASY:
                contentTr = easyScrollRect.content.transform;
                break;
            case DIFFICULTY.NORMAL:
                contentTr = nomalScrollRect.content.transform;
                break;
            case DIFFICULTY.HARD:
                contentTr = hardScrollRect.content.transform;
                break;
            case DIFFICULTY.VERYHARD:
                contentTr = veryhardScrollRect.content.transform;
                break;
        }

        //설정된 위치에서 생성
        info = Instantiate<UiScore>(infoBox, contentTr);

        //값 설정 
        info.init(index + 1, DataBaseServer.Inst.userlist[index].id, score);

    }

    public void SetType(GAMETYPE type)
    {
        myType = type;
    }

    public void SetPlaceNickName()
    {
        fistPlaceText.text   = DataBaseServer.Inst.userlist[0].id;
        secondPlaceText.text = DataBaseServer.Inst.userlist[1].id;
        thirdPlaceText.text  = DataBaseServer.Inst.userlist[2].id;
    }

}
