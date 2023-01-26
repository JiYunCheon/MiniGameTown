using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
[RequireComponent(typeof(GameData))]
public class GameManager : MonoBehaviour
{
    const string gameName_Momory = "com.DefaultCompany.OneWeek_MemoryCard";
    const string gameName_Find = "com.DefaultCompany.WrongPicture";
    const string gameName_Puzzle = "com.DefaultCompany.JigsawPuzzle";
    const string gameName_Balloon = "com.DefaultCompany.Pop_The_Balloon";

    private UiManager uiManager = null;
    private FirstSceneUiController firstSceneUiController = null;
    private ClickManager clickManager = null;
    private GameData gameData = null;
    private CameraControll cameraMove = null;
    private PadSpawner padSpawner = null;

    [HideInInspector] public string curGameName = null;
    public bool buildingMode = false;
    public bool waitingMode  = false;

    [HideInInspector] public int pointerID;

  


    #region Property

    public GameData GetGameData 
    { 
        get 
        { 
            if(gameData == null)
                gameData = GetComponent<GameData>();
            return gameData;
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

    public PadSpawner GetPadSpawner
    {
        get
        {
            if (padSpawner == null)
                padSpawner = FindObjectOfType<PadSpawner>();

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


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ChangeMode(out buildingMode,true);
        }
    }


    public void ChangeMode(out bool mode, bool check)
    {
        mode = check;
    }


}
