using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private string[] gameNames = null;
    const string gameName_Momory = "com.DefaultCompany.OneWeek_MemoryCard";
    const string gameName_Find = "com.DefaultCompany.FindtheWrongPicture";

    [SerializeField] private UiManager uiManager = null;
    [SerializeField] private ClickManager clickManager = null;

    public UiManager GetUiManager { get { return uiManager; } private set{ } }
    public ClickManager GetClickManager { get { return clickManager; } private set{ } }
    public string[] GetGameNames { get { return gameNames; } private set { } }
}
