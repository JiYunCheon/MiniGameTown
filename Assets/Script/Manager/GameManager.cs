using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Unity;
public enum OBJECT_TYPE
{
    BUIDING,
    OBJECT
}

public class GameManager : MonoBehaviour
{
    const string gameName_Momory = "com.DefaultCompany.OneWeek_MemoryCard";
    const string gameName_Find = "com.DefaultCompany.WrongPicture";
    const string gameName_Puzzle = "com.DefaultCompany.Jigsaw_Final";
    const string gameName_Balloon = "com.DefaultCompany.Pop_The_Balloon";

    [SerializeField] private ObjectData data = null;


    private UiManager uiManager = null;
    private FirstSceneUiController firstSceneUiController = null;
    private ClickManager clickManager = null;
    private CameraControll cameraMove = null;
    private PadSpawner[] padSpawner = null;
    private EffectManager effectManager = null;

    [HideInInspector] public string curGameName = null;
    public bool buildingMode = false;
    public bool waitingMode  = false;

    [HideInInspector] public int pointerID;

    public Dictionary<string, int> datas = new Dictionary<string, int>();

    public List<Ground> grounds = new List<Ground>();
    private int[] grounds_Info;
    PlayerMove player = null;

    private Vector3 mousePos = Vector3.zero;

    #region Property

    public PlayerData GetPlayerData { get { return data.playerData[0]; } private set { } }
    public List<Excel> GetObjectData { get { return data.objectdatas; } private set { } }

    public EffectManager GetEffectManager
    {
        get
        {
            if (effectManager == null)
                effectManager = FindObjectOfType<EffectManager>();
            return effectManager;
        }
        set { }
    }

    public PlayerMove GetPlayer
    {
        get
        {
            if (player == null)
                player = FindObjectOfType<PlayerMove>();
            return player;
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


    public void GameMoneyControll(int money)
    {

        GetPlayerData.gameMoney -= money;
        Debug.Log(GetPlayerData.gameMoney);
    }


    public void ChangeMode(out bool mode, bool check)
    {
        mode = check;
    }

    public Vector3 CurMousePos()
    {
        mousePos = Camera.main.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        return mousePos;
    }

    private void Init()
    {
        //dicpool 처럼 고치기


        //Building
        datas.Add(GetPlayerData.balloon_B_Name, GetPlayerData.balloon_B_Count);
        datas.Add(GetPlayerData.find_B_Name, GetPlayerData.find_B_Count);
        datas.Add(GetPlayerData.memory_B_Name, GetPlayerData.memory_B_Count);
        datas.Add(GetPlayerData.puzzle_B_Name, GetPlayerData.puzzle_B_Count);
        datas.Add(GetPlayerData.cook_B_Name, GetPlayerData.cook_B_Count);
        datas.Add(GetPlayerData.myRoom_B_Name, GetPlayerData.myRoom_B_Count);

        //object
        datas.Add(GetPlayerData.cart_O_Name, GetPlayerData.cart_O_Count);

        datas.Add(GetPlayerData.appleTree_O_Name, GetPlayerData.appleTree_O_Count);

        datas.Add(GetPlayerData.cat_Black_O_Name, GetPlayerData.cat_Black_O_Count);

        datas.Add(GetPlayerData.cat_White_O_Name, GetPlayerData.cat_White_O_Count);

        datas.Add(GetPlayerData.fence_End_Short_O_Name, GetPlayerData.fence_End_Short_O_Count);

        datas.Add(GetPlayerData.fence_End_Vertical_O_Name, GetPlayerData.fence_End_Vertical_O_Count);

        datas.Add(GetPlayerData.fence_End_O_Name, GetPlayerData.fence_End_Count);

        datas.Add(GetPlayerData.flower_1x_Orange_Name, GetPlayerData.flower_1x_Orange_Count);

        datas.Add(GetPlayerData.flower_1x_Purple_Name, GetPlayerData.flower_1x_Purple_Count);

        datas.Add(GetPlayerData.flowers_BlueLight_Name, GetPlayerData.flowers_BlueLight_Count);

        datas.Add(GetPlayerData.flowers_Pink_Name, GetPlayerData.flowers_Pink_Count);

        datas.Add(GetPlayerData.garbageCan_Blue_Name, GetPlayerData.garbageCan_Blue_Count);

        datas.Add(GetPlayerData.garbageCan_Red_Name, GetPlayerData.garbageCan_Red_Count);

        datas.Add(GetPlayerData.lantern_Path_Name, GetPlayerData.lantern_Path_Count);

        datas.Add(GetPlayerData.lantern_Small_Name, GetPlayerData.lantern_Small_Count);

        datas.Add(GetPlayerData.pineTree_Bright_Name, GetPlayerData.pineTree_Bright_Count);

        datas.Add(GetPlayerData.pineTree_Snow_1_Name, GetPlayerData.pineTree_Snow_1_Count);

        datas.Add(GetPlayerData.pineTree_Snow_2_Name, GetPlayerData.pineTree_Snow_2_Count);

        datas.Add(GetPlayerData.Tree_Fruits_Plums_Name, GetPlayerData.tree_Fruits_Plums_Count);

        datas.Add(GetPlayerData.trunk_Name, GetPlayerData.trunk_Count);

        datas.Add(GetPlayerData.trunk_x3_Name, GetPlayerData.trunk_x3_Count);

        datas.Add(GetPlayerData.umbrella_Purple_Name, GetPlayerData.umbrella_Purple_Count); 

        datas.Add(GetPlayerData.umbrella_Red_Name, GetPlayerData.umbrella_Red_Count);

        datas.Add(GetPlayerData.balloonStand_Name, GetPlayerData.balloonStand_Count);


    }


    public int SetCount(string name, int _value)
    {
        if (datas.TryGetValue(name, out int value))
        {
            int add = value + _value;
            if (add <= 0)
                add = 0;

            datas[name] = add;

            return datas[name];
        }
        else
        {
            
            Debug.LogError(name+" :: "+"OMG NO DATAS");
            return 0;
        }
    }







    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SaveData();
    }

    private void SaveData()
    {
        grounds_Info = new int[grounds.Count];

        for (int i = 0; i < grounds.Count; i++)
        {
            if(grounds[i].buildingCheck)
                grounds_Info[i] = 1;
            else
                grounds_Info[i] = 0;
        }

        List<DataFormat> dats = new List<DataFormat>();

        for (int i = 0; i < grounds.Count; i++)
        {
            DataFormat data = new DataFormat();

            data.checkValues = grounds_Info[i];
            dats.Add(data);
        }

        CSVUTILS.saveData(dats,"wow");

    }

    private void LoadData()
    {
        for (int i = 0; i < grounds_Info.Length; i++)
        {
            if (grounds_Info[i]==1)
                grounds[i].buildingCheck = true;
            else
                grounds[i].buildingCheck = false;

        }
    }
}
