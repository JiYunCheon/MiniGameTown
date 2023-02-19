using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userScore = null;
    [SerializeField] private TextMeshProUGUI curRank = null;
    [SerializeField] private TextMeshProUGUI bestRank = null;

    [SerializeField] private TextMeshProUGUI difiiculty = null;

    public void SetUserScoreText(float score)
    {
        userScore.text=score.ToString("F1");
    }

    public void SetCurRank(int rank)
    {
        curRank.text = rank.ToString();
    }

    public void SetBestRank(int rank)
    {
        bestRank.text = rank.ToString();
    }

    public void SetDifficulty(string dif)
    {
        difiiculty.text = dif;
    }
    

}
