//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class RankingUiData : MonoBehaviour
//{
//    #region Refrence Member

//    [Header("======Difficulty Board======")]
//    [SerializeField] private GameObject easyBoard = null;
//    [SerializeField] private GameObject nomalBoard = null;
//    [SerializeField] private GameObject hardBoard = null;
//    [SerializeField] private GameObject veryhardBoard = null;
//    private GameObject selectBoard = null;
//    [Header("")]

//    [Header("======Top User Text======")]
//    [SerializeField] private TextMeshProUGUI fistPlaceText = null;
//    [SerializeField] private TextMeshProUGUI secondPlaceText = null;
//    [SerializeField] private TextMeshProUGUI thirdPlaceText = null;
//    [Header("")]

//    [Header("======Ranking Boad Name======")]
//    public TextMeshProUGUI curGameName = null;
//    [Header("")]

//    [SerializeField] private UiScore upDownObjPrefab = null;
//    private UiScore[] upDownObj = null;

//    [Header("")]

//    [Header("======Type By Difficulty======")]
//    public ScrollRect easyScrollRect = null;
//    public ScrollRect nomalScrollRect = null;
//    public ScrollRect hardScrollRect = null;
//    public ScrollRect veryhardScrollRect = null;
//    [Header("")]

//    #endregion

//    #region Member

//    [Header("======Sensetive value======")]
//    [SerializeField] private float size = 0;
//    [Header("======Up&DownUi Pos======")]
//    [SerializeField] private Vector2 upUiPos   = Vector2.zero;
//    [SerializeField] private Vector2 downUiPos = Vector2.zero;

//    [Header("======UserRank Prefab======")]
//    [SerializeField] private UiScore infoBox = null;
//    [Header("")]

//    //Cur Type
//    private GAMETYPE myType;
//    private DIFFICULTY myDifficulty;

//    //ScrollRect fist Pos
//    private float scrollValue = 0;
//    private bool isActiveBoard = false;

//    private List<UiScore> contentList = null;
//    //CurUser Ranking Ui
//    private UiScore curUserInfoUi = null;

//    private float up = 0;
//    private float down = 0;

//    #endregion

//    private void Awake()
//    {
//        contentList = new List<UiScore>();
//        upDownObj = new UiScore[2];
//    }

//    private void Start()
//    {
//        //Onclick_Easy();
//    }
//    private void Update()
//    {
//        if (isActiveBoard == true&&!DataBaseServer.Inst.instDone)
//        {
//            switch (myDifficulty)
//            {
//                case DIFFICULTY.EASY:
//                    UpDownRankingUI(easyScrollRect);
//                    break;
//                case DIFFICULTY.NORMAL:
//                    UpDownRankingUI(nomalScrollRect);
//                    break;
//                case DIFFICULTY.HARD:
//                    UpDownRankingUI(hardScrollRect);
//                    break;
//                case DIFFICULTY.VERYHARD:
//                    UpDownRankingUI(veryhardScrollRect);
//                    break;
//            }
//        }

//    }

//    public void SetActiveBoardCheck(bool check = true)
//    {
//        isActiveBoard = check;
//    }

//    #region ��ŷ���� ���̵� ���� ��۱��

//    public void ActiveEasyBoard(bool active = true)
//    {
//        selectBoard.gameObject.SetActive(!active);
//        easyBoard.SetActive(active);
//    }

//    public void ActiveNomalBoard(bool active = true)
//    {
//        selectBoard.gameObject.SetActive(!active);
//        nomalBoard.SetActive(active);
//    }

//    public void ActiveHardBoard(bool active = true)
//    {
//        selectBoard.gameObject.SetActive(!active);
//        hardBoard.SetActive(active);
//    }

//    public void ActiveVeryHardBoard(bool active = true)
//    {
//        selectBoard.gameObject.SetActive(!active);
//        veryhardBoard.SetActive(active);
//    }

//    #endregion

//    #region Ŭ�� �̺�Ʈ 

//    public void Onclick_Easy()
//    {
//        if (selectBoard != null)
//            ActiveEasyBoard();

//        SelectDifficultyButton(DIFFICULTY.EASY,easyBoard);

//        MoveScrollView(easyScrollRect);
//    }

//    public void Onclick_Nomal()
//    {
//        if (selectBoard != null)
//            ActiveNomalBoard();

//        SelectDifficultyButton(DIFFICULTY.NORMAL,nomalBoard);

//        MoveScrollView(nomalScrollRect);
//    }

//    public void Onclick_Hard()
//    {
//        if (selectBoard != null)
//            ActiveHardBoard();

//        SelectDifficultyButton(DIFFICULTY.HARD,hardBoard);

//        MoveScrollView(hardScrollRect);
//    }

//    public void Onclick_VeryHard()
//    {
//        if(selectBoard!=null)
//            ActiveVeryHardBoard();

//        SelectDifficultyButton(DIFFICULTY.VERYHARD,veryhardBoard);

//        MoveScrollView(veryhardScrollRect);
//    }

//    public void OnClick_Exit_Ranking()
//    {
//        //��ũ�Ѻ� �� �ϴ� ���� ������ ����
//        DestroyUpDownUi();

//        //������ ���¸� ����
//        SetActiveBoardCheck(false);

//        //���� ���ӿ�����Ʈ�� ����
//        Destroy(this.gameObject);
//    }

//    #endregion

//    private void SelectDifficultyButton(DIFFICULTY dif, GameObject board)
//    {
//        //��ư �ߺ� ���� ����
//        if (DataBaseServer.Inst.isProcessing) return;
//        DataBaseServer.Inst.isProcessing = true;

//        //���� ���õ� ���带 ����
//        selectBoard = board;

//        //���� ���̵� ����
//        myDifficulty = dif;

//        //���� ��ũ�Ѻ信 ����Ʈ�� �ִٸ� ����Ʈ���� ����
//        DestroyContent();

//        //�����͸� �ҷ��� 
//        DataBaseServer.Inst.Call_GetUserInfo();

//    }

//    //Call Coroutine_ Change ScrollView Vertical Nomalized Pos
//    public void MoveScrollView(ScrollRect scrollRect)
//    {
//        StartCoroutine(MoveSequence(scrollRect));
//    }
//    private IEnumerator MoveSequence(ScrollRect scrollRect)
//    {
//        //����Ʈ�� ������ �� �ɶ����� ��ٸ�
//        yield return new WaitUntil(() => DataBaseServer.Inst.instDone);

//        int targetIndex = 0;
//        int count = 0;

//        foreach (Transform child in scrollRect.content.transform)
//        {
//            UiScore uiScore = null;

//            if(child.gameObject.TryGetComponent<UiScore>(out uiScore))
//            {
//                //����Ʈ�� ������ ����Ʈ�� ����Ʈ�� ����
//                contentList.Add(uiScore);

//                //������ ����Ʈ�� ã��
//                if (uiScore.GetNickNameText == DataBaseServer.Inst.loginUser.id)
//                {
//                    //ã�� ������ ����Ʈ�� ����
//                    curUserInfoUi = uiScore;

//                    //�̸� ����
//                    curUserInfoUi.gameObject.name = "my";

//                    //������ ����Ʈ�� �� ��°�� �ִ��� ����
//                    targetIndex = count;

//                    //����Ʈ�� ������ ����
//                    uiScore.ChangeColor(UnityEngine.Color.cyan);
//                }
//            }

//            count++;
//        }

//        //��ũ�Ѻ��� ���� ���� �� ������ ��� 
//        yield return new WaitForSeconds(0.05f);


//        //Debug.Log("������ : "+ size);
//        //Debug.Log("Ÿ�� �ε��� : " + targetIndex);
//        //Debug.Log("��ü ������ ũ�� : " + (scrollRect.content.childCount * size));
//        //Debug.Log("���̴� ���� : " + scrollRect.GetComponent<RectTransform>().rect.height);
//        //Debug.Log("��ũ�� �� ������ : " + scrollRect.verticalScrollbar.size);

//        //��ũ�Ѻ��� ����Ʈ�� �������� �ٲ� �� ���
//        float value = size * targetIndex / (((scrollRect.content.childCount * size) - 20)- scrollRect.GetComponent<RectTransform>().rect.height);

//        scrollValue = (1 - value) + scrollRect.verticalScrollbar.size/2;

//        //��ũ�Ѻ��� ���� ���� �� ������ ��� 
//        yield return new WaitForSeconds(0.05f);

//        //��ũ�Ѻ信 �� ����
//        scrollRect.verticalNormalizedPosition = scrollValue;

//        up = scrollValue - (scrollRect.verticalScrollbar.size / 2f);
//        down = scrollValue + (scrollRect.verticalScrollbar.size*0.6f);

//        if (down < 0)
//            down = 0;

//        //Debug.Log("��ũ�� ��� : "+scrollValue);
//        //Debug.Log("up : " + up);
//        //Debug.Log("down : " + down);

//        //��ũ�Ѻ� �� �ϴ� �����̿� ����Ÿ ���� 
//        InsertInfoUpDownUi();

//        //1,2,3 �� �����̿� ����Ÿ ����
//        SetPlaceNickName();

//        //���� �ʱ�ȭ
//        DataBaseServer.Inst.instDone = false;
//    }



//    //Ative curUserRanking Info
//    public void UpDownRankingUI(ScrollRect scrollRect)
//    {
//        //�������� �ʾҴٸ� ����
//        if (upDownObj[0] == null|| scrollRect.verticalScrollbar.size==1) return;
       

//        //���� ������ ���� ���� ���ϴ� �����̰� ��µ�
//        if (scrollRect.verticalNormalizedPosition > down)
//        {
//            upDownObj[1].gameObject.SetActive(true);
//            upDownObj[0].gameObject.SetActive(false);
//        }
//        else if (scrollRect.verticalNormalizedPosition < up)
//        {
//            upDownObj[0].gameObject.SetActive(true);
//            upDownObj[1].gameObject.SetActive(false);
//        }
//        else
//        {
//            upDownObj[1].gameObject.SetActive(false);
//            upDownObj[0].gameObject.SetActive(false);
//        }

//    }

//    //Clear ContentList
//    private void DestroyContent()
//    {
//        if (contentList.Count > 0)
//        {
//            for (int i = 0; i < contentList.Count; i++)
//            {
//                Destroy(contentList[i].gameObject);
//            }
//            contentList.Clear();

//            curUserInfoUi = null;
//        }
//    }

//    //InstUp&DownUi
//    private void InsertInfoUpDownUi()
//    {
//        if (upDownObj[0]==null)
//        {
//            //��� ������ ����
//            UiScore uiScore = Instantiate<UiScore>(upDownObjPrefab,this.transform);

//            //������ ����
//            uiScore.init(DataBaseServer.Inst.curRank, curUserInfoUi.GetNickNameText, DataBaseServer.Inst.loginUser.curScore);
//            uiScore.GetComponent<RectTransform>().anchoredPosition = upUiPos;

//            //�迭�� �� ����
//            upDownObj[0] = uiScore;

//            //�� ����
//            upDownObj[0].ChangeColor(UnityEngine.Color.green);

//            //�ϴ� ������ ����
//            uiScore = Instantiate<UiScore>(upDownObjPrefab, this.transform);

//            //������ ����
//            uiScore.init(DataBaseServer.Inst.curRank, curUserInfoUi.GetNickNameText, DataBaseServer.Inst.loginUser.curScore);
//            uiScore.GetComponent<RectTransform>().anchoredPosition = downUiPos;

//            //�迭�� �� ����
//            upDownObj[1] = uiScore;

//            //�� ����
//            upDownObj[1].ChangeColor(UnityEngine.Color.green);

//            upDownObj[0].gameObject.SetActive(false);
//            upDownObj[1].gameObject.SetActive(false);
//        }
//        else
//        {

//            Debug.Log(curUserInfoUi.GetNickNameText);
//            //�̹� �����Ѵٸ� ���� ����
//            upDownObj[0].init(DataBaseServer.Inst.curRank, curUserInfoUi.GetNickNameText, DataBaseServer.Inst.loginUser.curScore);
//            upDownObj[1].init(DataBaseServer.Inst.curRank, curUserInfoUi.GetNickNameText, DataBaseServer.Inst.loginUser.curScore);

//            upDownObj[0].gameObject.SetActive(false);
//            upDownObj[1].gameObject.SetActive(false);
//        }
      
//    }

//    //Destroy Up&DownUi
//    private void DestroyUpDownUi()
//    {
//        //�̹� ���ٸ� ����
//        if (upDownObj[0] == null) return;
    
//        //�ı� �� �ʱ�ȭ
//        for (int i = 0; i < upDownObj.Length; i++)
//        {
//            Destroy(upDownObj[i].gameObject);
//            upDownObj[i] = null;
//        }
//    }

//    //UserUi Inst Logic
//    public void InstLogic(DIFFICULTY dif, int index, float score)
//    {
//        UiScore info = null;
//        Transform contentTr = null;
//        //���� �� ��ġ�� ���̵����� ����

//        switch (dif)
//        {
//            case DIFFICULTY.EASY:
//                contentTr = easyScrollRect.content.transform;
//                break;
//            case DIFFICULTY.NORMAL:
//                contentTr = nomalScrollRect.content.transform;
//                break;
//            case DIFFICULTY.HARD:
//                contentTr = hardScrollRect.content.transform;
//                break;
//            case DIFFICULTY.VERYHARD:
//                contentTr = veryhardScrollRect.content.transform;
//                break;
//        }

//        //������ ��ġ���� ����
//        info = Instantiate<UiScore>(infoBox, contentTr);

//        //�� ���� 
//        info.init(index + 1, DataBaseServer.Inst.userlist[index].id, score);

//    }

//    public void SetType(GAMETYPE type)
//    {
//        myType = type;
//    }

//    public void SetPlaceNickName()
//    {
//        fistPlaceText.text   = DataBaseServer.Inst.userlist[0].id;
//        secondPlaceText.text = DataBaseServer.Inst.userlist[1].id;
//        thirdPlaceText.text  = DataBaseServer.Inst.userlist[2].id;
//    }

//}
