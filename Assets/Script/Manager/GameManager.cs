using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string gameName_Momory = "com.DefaultCompany.OneWeek_MemoryCard";
    const string gameName_Find = "com.DefaultCompany.WrongPicture";
    const string gameName_Puzzle = "com.DefaultCompany.JigsawPuzzle";
    const string gameName_Balloon = "com.DefaultCompany.Pop_The_Balloon";

    private UiManager uiManager = null;
    private FirstSceneUiController firstSceneUiController = null;
    private ClickManager clickManager = null;
    [HideInInspector] public string curGameName = null;
    [SerializeField] private bool buildingMode = false;
    public bool GetBuildingMode {get {return buildingMode;} private set { } }

    #region Property

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
    }

}
