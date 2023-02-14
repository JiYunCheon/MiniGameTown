using System.Collections.Generic;
using UnityEngine;
using System;

public enum OBJECT_TYPE
{
    BUILDING,
    OBJECT
}
public enum DIFFICULTY
{
    EASY,
    NORMAL,
    HARD,
    VERYHARD
}

public class PlayerData
{
    public int gameMoney;
    public int[] objectCount;
}

public class GameManager : MonoBehaviour
{
    const string gameName_Memory = "com.DefaultCompany.OneWeek_MemoryCard";
    const string gameName_Find = "com.DefaultCompany.WrongPicture";
    const string gameName_Puzzle = "com.DefaultCompany.Jigsaw_Final";
    const string gameName_Balloon = "com.DefaultCompany.Pop_The_Balloon";

    [SerializeField] private Data data = null;

    private UiManager uiManager = null;
    private FirstSceneUiController firstSceneUiController = null;
    private ClickManager clickManager = null;
    private CameraControll cameraMove = null;
    private PadSpawner[] padSpawner = null;
    private EffectManager effectManager = null;

    [HideInInspector] public string curGameName = null;
    public bool buildingMode = false;
    public bool waitingMode = false;

    [HideInInspector] public int pointerID;

    //패드를 생성 할 때 패드들을 add 함 
    public List<Ground> grounds = new List<Ground>();
    public int[] grounds_Info;

    //save Data value
    public Vector3[] objectsPos;
    public Vector3[] objectsRot;
    public string[] objectsName;

    PlayerMove player = null;


    //생성될 건물의 부모
    [Header("Parents")]
    private Transform buildings = null;

    #region Property

    public Transform GetBuildings
    {
        get
        {
            if (buildings == null)
                buildings = GameObject.Find("Buildings").transform;

            return buildings;
        }

        private set { }
    }

    public UserInfo GetPlayerData { get { return DataBaseServer.Inst.loginUser; } private set { } }

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
                padSpawner = FindObjectsOfType(typeof(PadSpawner)) as PadSpawner[];
            }
            return padSpawner;
        }
        private set { }
    }

    #endregion

    public static GameManager Inst = null;
    private void Awake()
    {

        if (Inst == null)
        {
            Inst = this;
            LoadData();

            SaveDic();
            DontDestroyOnLoad(Inst);
        }
        else Destroy(this);

#if UNITY_EDITOR
        pointerID = -1;
#elif UNITY_ANDROID
                pointerID = 0; 
#endif
    }

    private void Start()
    {
        GetUiFirstSceneUiController.InputGameMoney(DatabaseAccess.Inst.loginUser.gamemoney.ToString());
    }


    public void Call_IntractableObj_Method(int method = 0)
    {
        foreach (Transform item in Inst.GetBuildings)
        {
            if (item.TryGetComponent<Interactable>(out Interactable interactable))
            {
                if (method == 0)
                    interactable.ChangeState(true, Color.red);
                else if (method == 1)
                    interactable.Active_Name(true);
            }
        }
    }

    public void ChangeMode(out bool mode, bool check) => mode = check;


    public void SaveData()
    {
        objectsName = new string[GetBuildings.transform.childCount];
        objectsPos = new Vector3[buildings.transform.childCount];
        objectsRot = new Vector3[buildings.transform.childCount];

        DatabaseAccess.Inst.loginUser.posX = new string[buildings.transform.childCount];
        DatabaseAccess.Inst.loginUser.posY = new string[buildings.transform.childCount];
        DatabaseAccess.Inst.loginUser.posZ = new string[buildings.transform.childCount];
        DatabaseAccess.Inst.loginUser.rotY = new string[buildings.transform.childCount];
        DatabaseAccess.Inst.loginUser.objname = new string[buildings.transform.childCount];

        string name = "";
        int index = 0;
        foreach (Transform child in buildings)
        {
            name = child.name.Split("(")[0];
            objectsName[index] = name;
            objectsPos[index] = child.position;
            objectsRot[index] = child.transform.eulerAngles;

            if (child.TryGetComponent<Interactable>(out Interactable obj))
            {
                for (int i = 0; i < obj.myGround.Count; i++)
                {
                    obj.myGround[i].name = "SavePad";
                }
            }

            index++;
        }

        List<int> save = new List<int>();

        for (int i = 0; i < grounds.Count; i++)
        {
            if (grounds[i].name == "SavePad")
            {
                save.Add(i);
            }
        }

        DatabaseAccess.Inst.loginUser.grounds_Save = new string[save.Count];

        for (int i = 0; i < save.Count; i++)
        {
            DatabaseAccess.Inst.loginUser.grounds_Save[i] = save[i].ToString();
        }


        DatabaseAccess.Inst.loginUser.objname = objectsName;

        for (int i = 0; i < objectsName.Length; i++)
        {
            DatabaseAccess.Inst.loginUser.posX[i] = objectsPos[i].x.ToString();
            DatabaseAccess.Inst.loginUser.posY[i] = objectsPos[i].y.ToString();
            DatabaseAccess.Inst.loginUser.posZ[i] = objectsPos[i].z.ToString();
            DatabaseAccess.Inst.loginUser.rotY[i] = objectsRot[i].y.ToString();
        }


        //DatabaseAccess.Inst.loginUser.gamemoney = DatabaseAccess.Inst.loginUser.gamemoney;

        //DatabaseAccess.Inst.SaveUserData(DatabaseAccess.Inst.loginUser);
        DatabaseAccess.Inst.SetUserData_Replace_FromDatabase(DatabaseAccess.Inst.loginUser.id);
    }

    public void LoadData()
    {
        //DatabaseAccess.Inst.GetUserData(DatabaseAccess.Inst.loginUser.id, DatabaseAccess.Inst.loginUser.password);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            LoadObj();
    }
    public void LoadObj()
    {
        Interactable prefab = null;
        Interactable obj = null;

        string type = "";
        int count = 0;

        GetUiManager.Active_Pad();

        objectsName = DatabaseAccess.Inst.loginUser.objname;
        objectsPos = new Vector3[DatabaseAccess.Inst.loginUser.objname.Length];
        objectsRot = new Vector3[DatabaseAccess.Inst.loginUser.objname.Length];
        grounds_Info = new int[DatabaseAccess.Inst.loginUser.objname.Length];

        for (int i = 0; i < DatabaseAccess.Inst.loginUser.objname.Length; i++)
        {
            GameManager.Inst.objectsPos[i].x = float.Parse(DatabaseAccess.Inst.loginUser.posX[i]);
            GameManager.Inst.objectsPos[i].y = float.Parse(DatabaseAccess.Inst.loginUser.posY[i]);
            GameManager.Inst.objectsPos[i].z = float.Parse(DatabaseAccess.Inst.loginUser.posZ[i]);

            GameManager.Inst.objectsRot[i].y = float.Parse(DatabaseAccess.Inst.loginUser.rotY[i]);
        }

        for (int i = 0; i < objectsName.Length; i++)
        {
            if (objectsName[i].Split("_")[0] == "Building")
                type = "Building";
            else
                type = "Object";

            prefab = Resources.Load<Interactable>($"Prefabs/{type}/{objectsName[i]}");
            obj = Instantiate(prefab, objectsPos[i], Quaternion.identity, GetBuildings);
            obj.transform.eulerAngles = objectsRot[i];
            obj.SetMyData(GameManager.Inst.FindData(obj.name.Split("(")[0]));

            for (int j = count; j < DatabaseAccess.Inst.loginUser.grounds_Save.Length; j++) 
            {

                if (j < count + obj.GetMyData.occupyPad)
                {
                    grounds[int.Parse(DatabaseAccess.Inst.loginUser.grounds_Save[j])].ChangePadState(true, Color.red);
                    obj.myGround.Add(grounds[int.Parse(DatabaseAccess.Inst.loginUser.grounds_Save[j])]);
                }
                else
                {
                    count = j;
                    break;
                }
            }
        }

        GetUiManager.Active_Pad(false);
    }


    private Dictionary<string, Excel> dataDictionary = new Dictionary<string, Excel>();

    public Excel FindData(string key)
    {
        Excel data;

        if (dataDictionary.TryGetValue(key, out data))
        {
            return data;
        }
        else
        {
            Debug.LogError("OMG NO DATA");
            return null;
        }
    }

    private void SaveDic()
    {
        for (int i = 0; i < data.objectdatas.Count; i++)
        {
            dataDictionary.Add(data.objectdatas[i].prefabName, data.objectdatas[i]);
        }
    }

    int i = 0;
    public int TrySetValue(int index, int value)
    {
        Debug.Log(i);
        int calValue = int.Parse(DatabaseAccess.Inst.loginUser.objectcount[index]);
        i++;
        calValue = calValue + value;

        if (calValue < 0)
            calValue = 0;

        DatabaseAccess.Inst.loginUser.objectcount[index] = calValue.ToString();

        return calValue;
    }


    public int TrySetGameMoney(int value)
    {
        DatabaseAccess.Inst.loginUser.gamemoney = DatabaseAccess.Inst.loginUser.gamemoney + value;

        if (DatabaseAccess.Inst.loginUser.gamemoney < 0)
            DatabaseAccess.Inst.loginUser.gamemoney = 0;


        return DatabaseAccess.Inst.loginUser.gamemoney;
    }

}
