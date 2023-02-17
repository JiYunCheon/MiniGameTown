using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RankingUiData : MonoBehaviour
{
    #region Refrence Member

    [Header("======Difficulty Board======")]
    [SerializeField] private GameObject[] easyBoard = null;
    [Header("")]

    [Header("======Top User Text======")]
    [SerializeField] private TextMeshProUGUI fistPlaceText = null;
    [SerializeField] private TextMeshProUGUI secondPlaceText = null;
    [SerializeField] private TextMeshProUGUI thirdPlaceText = null;
    [Header("")]

    [Header("======Ranking Boad Name======")]
    public TextMeshProUGUI curGameName = null;
    [Header("")]

    [Header("======Type By Difficulty======")]
    [SerializeField] private ScrollRect[] scrollRect = null;
    [Header("")]

    #endregion

    #region Member

    [Header("======Sensetive value======")]
    [SerializeField] private float size = 0;

    [Header("======UserRank Prefab======")]
    [SerializeField] private UiScore rankingContentfrefab = null;

    #endregion


    private void Start()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            GenerateRanking(0, 0, DatabaseAccess.Inst.totalScoreData);
        }
    }


    IEnumerator start()
    {
        yield return new WaitUntil(()=>!DatabaseAccess.Inst.isProcessing);
        GenerateRanking(0, 0, DatabaseAccess.Inst.totalScoreData);

    }


    public void GenerateRanking(int gameNum,int difficulty, List<UserData> userdata)
    {
        ScrollRect scroll = scrollRect[difficulty];

        UserData.sortingIdx = gameNum*4+ difficulty;
        userdata.Sort();
        userdata.Reverse();
        Debug.Log(userdata.Count);

        for (int i = 0; i < userdata.Count; i++)
        {
            UiScore rankingContent = Instantiate<UiScore>(rankingContentfrefab, scroll.content.transform);
            rankingContent.init(i+1, userdata[i].nickname, userdata[i].score[gameNum * 4 + difficulty]);
        }

    }



}
