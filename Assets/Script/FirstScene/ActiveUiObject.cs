using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUiObject : Select
{
    [SerializeField] private RankingUiData rankingBoard = null;

    public override void GameSelect()
    {
        rankingBoard.gameObject.SetActive(true);
    }
 
}
