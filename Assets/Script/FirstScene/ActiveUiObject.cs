using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUiObject : Select
{
    [SerializeField] private RankingUiData rankingBoard = null;

    public override void GameSelect()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        rankingBoard.gameObject.SetActive(true);
    }
 
}
