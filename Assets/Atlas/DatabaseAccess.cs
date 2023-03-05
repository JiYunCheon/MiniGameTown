using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

[Serializable]
public class UserData : IComparable
{
    public static int sortingIdx;

    public string id;
    public string password;
    public string nickname;
    public int year;
    public int selectNum;

    public string[] score;
    public int gamemoney;

    public string[] objname;
    public string[] posX;
    public string[] posY;
    public string[] posZ;
    public string[] rotY;
    public string[] grounds_Save;

    public string[] objectcount;
    public string[] shopmaxcount;

    public string[] farmobjname;
    public string[] farminvenobjname;
    public string[] farmobjposX;
    public string[] farmobjposY;

    public int CompareTo(object obj)
    {
        UserData data = (UserData)obj;

        return float.Parse(this.score[sortingIdx]).CompareTo(float.Parse(data.score[sortingIdx]));
    }
}

public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://CheonJiYun:wldbs3456@cluster0.yyfatob.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    [HideInInspector] public UserData loginUser = null;

    [HideInInspector] public bool isProcessing = false;

    public static DatabaseAccess Inst = null;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            database = client.GetDatabase("UserData");
            collection = database.GetCollection<BsonDocument>("UserInfo");
            DontDestroyOnLoad(Inst);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        GetTotalData_FromDatabase();
    }
    public async void SaveUserData(UserData curUserData)
    {
        await collection.InsertOneAsync(curUserData.ToBsonDocument());
    }

    public async void GetUserData(string _id, string _password)
    {
        isProcessing= true;
        await GetUserData_FromDatabase(_id, _password);
    }

    private IEnumerator IsProcessing()
    {
        yield return new WaitUntil(()=>!isProcessing);

        if(loginUser.selectNum== -1)
        {
            SceneManager.LoadScene("CharacterSelectScene");
        }
        else
        {
            SceneManager.LoadScene("2.BaseTown");
        }

    }



    //�α��� ���� ������ ������ ����
    public async Task<UserData> GetUserData_FromDatabase(string _id, string _password)
    {
        BsonDocument find = new BsonDocument { { "_id", _id }, { "password", _password } };

        var allDatasTask = collection.FindAsync(find);
        var datasAwated = await allDatasTask;

        object arrayString;

        UserData user = new UserData();

        foreach (var data in datasAwated.ToList())
        {
            user.id = (string)data.GetValue("_id");
            user.password = (string)data.GetValue("password");
            user.gamemoney = (int)data.GetValue("gamemoney");
            user.year = (int)data.GetValue("year");
            user.nickname = (string)data.GetValue("nickname");
            user.selectNum=(int)data.GetValue("selectNum");

            arrayString = (object)data.GetValue("farmobjname");
            user.farmobjname = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("farminvenobjname");
            user.farminvenobjname = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("farmobjposX");
            user.farmobjposX = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("farmobjposY");
            user.farmobjposY = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("score");
            user.score = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("objname");
            //��Ʈ������ ��ȯ�� �迭���� ���� �迭�� ����
            user.objname = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("posX");
            user.posX = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("posY");
            user.posY = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("posZ");
            user.posZ = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("rotY");
            user.rotY = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("grounds_Save");
            user.grounds_Save = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("objectcount");
            user.objectcount = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("shopmaxcount");
            user.shopmaxcount = ArraySplitSort(arrayString);
        }

        loginUser = user;

        PlayerLogin.setPlayedData(loginUser.id);



        isProcessing = false;

        if (loginUser.id == null)
        {
            Debug.Log("�α��� ����");

            GameManager.Inst.GetLoginSceneController.Active_LoginFaied();

            return null;
        }
        Debug.Log("�α��� ���� :  " + loginUser.nickname);

        StartCoroutine(IsProcessing());

        return user;
    }

    private string[] ArraySplitSort(object str)
    {
        string[] words = str.ToString().Split(",");
        char[] charArray = str.ToString().ToCharArray();

        bool indexCheck = false;
        if (words[0] == "BsonNull") return null;

        for (int i = 0; i < charArray.Length; i++)
        {
            if (charArray[i]==',')
            {
                indexCheck = true;
                break;
            }
        }

        if (indexCheck)
        {

            words[0] = words[0].Split("[")[1];
            words[words.Length - 1] = words[words.Length - 1].Split("]")[0];
        }
        else
        {
            string word = words[0].Split("[")[1];
            words = new string[1];
            words = word.Split("]");

            word = "";
            for (int i = 0; i < words.Length; i++)
            {
                word += words[i];
            }
            words = new string[1];
            words[0] = word;
        }

        return words;
    }

    public void SetUserData_Replace_FromDatabase(string _id)
    {
        BsonDocument find = new BsonDocument { { "_id", _id }};

        collection.ReplaceOne(find,loginUser.ToBsonDocument());
    }


    //������ ���̺� �� ���̵� �ߺ� Ȯ��
    public async void CompareID_FromDatabase(string _id)
    {
        BsonDocument find = new BsonDocument { { "_id", _id }};

        var allDatasTask = collection.FindAsync(find);
        var datasAwated = await allDatasTask;

        if(datasAwated.ToList().Count==0)
        {
            GameManager.Inst.GetLoginSceneController.ChangeCreate_ID_State();
        }
        else
        {
            GameManager.Inst.GetLoginSceneController.ChangeCreate_ID_State(false);
        }

        isProcessing = false;
    }

    //�ʱ� ���������� ����
    public UserData BeginUserDataInit(string id, string psw,int year,string nickname)
    {
        UserData newUser = new UserData();

        newUser.id = id;
        newUser.password = psw;
        newUser.year = year;
        newUser.nickname = nickname;
        newUser.gamemoney = 5000;
        newUser.selectNum = -1;



        newUser.objectcount = new string[38];
        newUser.shopmaxcount = new string[38];
        newUser.score = new string[16];

        for (int i = 0; i < newUser.score.Length; i++)
        {
            newUser.score[i] = "0";
        }


        for (int i = 0; i < newUser.objectcount.Length; i++)
        {
            newUser.objectcount[i] = "0";
            newUser.shopmaxcount[i] = GameManager.Inst.GetObjectData[i].maxCount.ToString();
        }

        return newUser;
    }

    //��й�ȣ ã��
    public async void ComparePsw_FromDatabase(string _id, int _year)
    {
        BsonDocument find = new BsonDocument { { "_id", _id }, { "year", _year } };

        var allDatasTask = collection.FindAsync(find);
        var datasAwated = await allDatasTask;

        UserData user = new UserData();

        foreach (var data in datasAwated.ToList())
        {
            user.password = (string)data.GetValue("password");
        }

        if(user.password==null)
        {
            GameManager.Inst.GetLoginSceneController.ChangeFind_ID_State(GameManager.Inst.GetLoginSceneController.red);
        }
        else
        {
            GameManager.Inst.GetLoginSceneController.ChangeFind_ID_State(GameManager.Inst.GetLoginSceneController.green, user.password);
        }

    }


    public List<UserData> totalScoreData = new List<UserData>();

    public async void GetTotalData_FromDatabase()
    {

        var allDatasTask = collection.FindAsync(new BsonDocument());
        var datasAwated = await allDatasTask;

        object arrayString;

        foreach (var data in datasAwated.ToList())
        {
            UserData user = new UserData();

            user.id = (string)data.GetValue("_id");
            user.selectNum = (int)data.GetValue("selectNum");
            user.nickname = (string)data.GetValue("nickname");

            arrayString = (object)data.GetValue("score");
            user.score = ArraySplitSort(arrayString);

            totalScoreData.Add(user);
        }

        isProcessing = false;

    }






}
