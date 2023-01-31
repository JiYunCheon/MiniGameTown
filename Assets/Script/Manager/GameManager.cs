using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    const string gameName_Momory = "com.DefaultCompany.OneWeek_MemoryCard";
    const string gameName_Find = "com.DefaultCompany.WrongPicture";
    const string gameName_Puzzle = "com.DefaultCompany.JigsawPuzzle";
    const string gameName_Balloon = "com.DefaultCompany.Pop_The_Balloon";

    [SerializeField] private PlayerData playerData = null;

    private UiManager uiManager = null;
    private FirstSceneUiController firstSceneUiController = null;
    private ClickManager clickManager = null;
    private CameraControll cameraMove = null;
    private PadSpawner[] padSpawner = null;


    [HideInInspector] public string curGameName = null;
    public bool buildingMode = false;
    public bool waitingMode  = false;

    [HideInInspector] public int pointerID;

    public Dictionary<string, int> datas = new Dictionary<string, int>();

    #region Property

    public PlayerData GetPlayerData 
    { 
        get 
        {
            return playerData;
        } 
        set { }
    }

    public UiManager GetUiManager
    {
        get
        {
            if (uiManager == null)
                uiManager = FindObjectOfType<UiManager>();

            return uiManager;
        }
        private set { }
    }

    public ClickManager GetClickManager
    {
        get
        {
            if (clickManager == null)
                clickManager = FindObjectOfType<ClickManager>();

            return clickManager;
        }
        private set { }
    }

    public CameraControll GetCameraMove
    {
        get
        {
            if (cameraMove == null)
                cameraMove = FindObjectOfType<CameraControll>();

            return cameraMove;
        }
        private set { }
    }

    public FirstSceneUiController GetUiFirstSceneUiController
    {
        get
        {
            if (firstSceneUiController == null)
                firstSceneUiController = FindObjectOfType<FirstSceneUiController>();

            return firstSceneUiController;
        }
        private set { }
    }

    public PadSpawner[] GetPadSpawner
    {
        get
        {
            if (padSpawner == null)
            {
                padSpawner = FindObjectsOfType(typeof (PadSpawner))as PadSpawner[];
            }
            return padSpawner;
        }
        private set { }
    }

    #endregion

    public static GameManager Inst = null;
    private void Awake()
    {
        if(Inst == null)
        {
            Inst = this;
            Init();
            DontDestroyOnLoad(Inst);
        }
        else
        {
            Destroy(this);
        }

        #if UNITY_EDITOR
                pointerID = -1;
        #elif UNITY_ANDROID
                pointerID = 0; 
        #endif
    }


    public void ChangeMode(out bool mode, bool check)
    {
        mode = check;
    }

    private void Init()
    {
        datas.Add(GetPlayerData._GameMoney, GetPlayerData.GameMoney);

        datas.Add(GetPlayerData._Balloon_B_Count, GetPlayerData.Balloon_B_Count);
        datas.Add(GetPlayerData._Find_B_Count, GetPlayerData.Find_B_Count);
        datas.Add(GetPlayerData._Memory_B_Count, GetPlayerData.Memory_B_Count);
        datas.Add(GetPlayerData._Puzzle_B_Count, GetPlayerData.Puzzle_B_Count);
        datas.Add(GetPlayerData._Cook_B_Count, GetPlayerData.Cook_B_Count);
        datas.Add(GetPlayerData._MyRoom_B_Count, GetPlayerData.MyRoom_B_Count);

        datas.Add(GetPlayerData._Cart_O_Count, GetPlayerData.Cart_O_Count);
        datas.Add(GetPlayerData._AppleTree_O_Count, GetPlayerData.AppleTree_O_Count);
    }



}
