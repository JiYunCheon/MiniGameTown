using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{

    #region GameData
    //스크립터블 오브젝트로 받으 데이터 들
    private int occupyPad = 0;
    private Interactable prefab = null;
    private PreviewObject alphaPrefab = null;
    private GAMETYPE myType;

    #endregion

    public Interactable GetPrefab { get { return prefab; } private set { } }
    public PreviewObject GetAlphaPrefab { get { return alphaPrefab; } private set { } }
    public GAMETYPE GetMyType { get { return myType; } private set { } }
    public int GetOccupyPad { get { return occupyPad; } private set { } }

     public void Initialized(PreviewObject alphaPrefab, Interactable prefab, int occupyPad , GAMETYPE type)
    {
        this.alphaPrefab = alphaPrefab;
        this.prefab = prefab;
        this.occupyPad = occupyPad;
        this.myType = type;
    }

}
