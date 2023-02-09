using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankingText = null;
    [SerializeField] private TextMeshProUGUI nickNameText = null;
    [SerializeField] private TextMeshProUGUI socreText = null;
    [SerializeField] private Image Image = null;

    public string GetRankingText { get { return rankingText.text; } private set { } }
    public string GetNickNameText { get { return nickNameText.text; } private set { } } 
    public string GetSocreText { get { return socreText.text; } private set { } }
         

    public void init(int ranking,string nickname,float score)
    {
        if(score==10000)
        {
            socreText.text = "None";
            rankingText.text = "Nub";
            nickNameText.text = nickname;
        }
        else
        {
            socreText.text = score.ToString();
            rankingText.text = ranking.ToString();
            nickNameText.text = nickname;
        }

    }


    public void ChangeColor(Color color)
    {
        Image.color = color;
    }

}
