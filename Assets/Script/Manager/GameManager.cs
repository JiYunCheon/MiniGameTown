using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Unity;
using TMPro.Examples;
using System.Threading;
using UnityEditor;

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
    public bool waitingMode  = false;

    [HideInInspector] public int pointerID;

    //패드를 생성 할 때 패드들을 add 함 
    public List<Ground> grounds = new List<Ground>();
    public int[] grounds_Info;

    //save Data value
     public Vector3[] objectsPos;
     public Vector3[] objectsRot ;
     public string[]  objectsName;

    PlayerMove player = null;

    private Vector3 mousePos = Vector3.zero;

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


    public void ChangeMode(out bool mode, bool check) => mode = check;

    public Vector3 CurMousePos()
    {
        mousePos = Camera.main.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        return mousePos;
    }


    public int SetCount(string key, int _value) => GetPlayerData.TrySetValue(key, _value);


    public void SaveData()
    {
        objectsName = new string[buildings.transform.childCount];
        objectsPos = new Vector3[buildings.transform.childCount];
        objectsRot = new Vector3[buildings.transform.childCount];

        DataBaseServer.Inst.loginUser.posX = new string[buildings.transform.childCount];
        DataBaseServer.Inst.loginUser.posY = new string[buildings.transform.childCount];
        DataBaseServer.Inst.loginUser.posZ = new string[buildings.transform.childCount];
        DataBaseServer.Inst.loginUser.rotY = new string[buildings.transform.childCount];
        DataBaseServer.Inst.loginUser.objname = new string[buildings.transform.childCount];

        string name = "";
        int index = 0;
        foreach (Transform child in buildings)
        {
            name = child.name.Split("(")[0];
            objectsName[index] = name;
            objectsPos[index] = child.position;
            objectsRot[index] = child.transform.eulerAngles;

            if(child.TryGetComponent<Interactable>(out Interactable obj))
            {
                for (int i = 0; i < obj.myGround.Count; i++)
                {
                    obj.myGround[i].name = "SavePad";
                }
            }

            index++;
        }

        List<int> save =new List<int>();

        for (int i = 0; i < grounds.Count; i++)
        {
            if (grounds[i].name=="SavePad")
            {
                save.Add(i);
            }
        }
     
        DataBaseServer.Inst.loginUser.grounds_Save = new string[save.Count];

        for (int i = 0; i < save.Count; i++)
        {
            DataBaseServer.Inst.loginUser.grounds_Save[i] = save[i].ToString();
        }

    
        DataBaseServer.Inst.loginUser.objname = objectsName;

        for (int i = 0; i < objectsName.Length; i++)
        {
            DataBaseServer.Inst.loginUser.posX[i] = objectsPos[i].x.ToString();
            DataBaseServer.Inst.loginUser.posY[i] = objectsPos[i].y.ToString();
            DataBaseServer.Inst.loginUser.posZ[i] = objectsPos[i].z.ToString();
            DataBaseServer.Inst.loginUser.rotY[i] = objectsRot[i].y.ToString();
        }

        DataBaseServer.Inst.SaveScore();
    }

    public void LoadData()
    {
        DataBaseServer.Inst.Login();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            LoadData();
    }
    public void LoadObj()
    {
        Interactable prefab = null;
        Interactable  obj= null;

        string type = "";
        int value = 0;
        int count = 0;

        GetUiManager.Active_Pad();

        for (int i = 0; i < objectsName.Length; i++)
        {
            if (objectsName[i].Split("_")[0] == "Building")
                type = "Building";
            else
                type = "Object";

            prefab = Resources.Load<Interactable>($"Prefabs/{type}/{objectsName[i]}");
            obj = Instantiate(prefab, objectsPos[i], Quaternion.identity,GetBuildings);
            obj.transform.eulerAngles = objectsRot[i];
            obj.SetMyData(GameManager.Inst.FindData(obj.name.Split("(")[0]));

            for (int j = count; j < DataBaseServer.Inst.loginUser.grounds_Save.Length; j++)
            {

                if(j<count+obj.GetMyData.occupyPad)
                {
                    grounds[int.Parse(DataBaseServer.Inst.loginUser.grounds_Save[j])].ChangePadState(true,Color.red);
                    obj.myGround.Add(grounds[int.Parse(DataBaseServer.Inst.loginUser.grounds_Save[j])]);
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


    Dictionary<string, Excel> dataDictionary = new Dictionary<string, Excel>();

    public Excel FindData(string key)
    {
        Excel data;

        if(dataDictionary.TryGetValue(key, out data))
        {
            return data;
        }

        return null;
    }

    private void SaveDic()
    {
        for (int i = 0; i < data.objectdatas.Count; i++)
        {
            dataDictionary.Add(data.objectdatas[i].prefabName, data.objectdatas[i]);
        }
    }






}
