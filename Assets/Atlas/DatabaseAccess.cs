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

[Serializable]
public class UserData
{
    public string id;
    public string password;

    public string[] score;
    public int gamemoney;

    public string[] objname;
    public string[] posX;
    public string[] posY;
    public string[] posZ;
    public string[] rotY;
    public string[] grounds_Save;

    public string[] objectcount;
}

public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://CheonJiYun:wldbs3456@cluster0.yyfatob.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    [HideInInspector] public UserData loginUser = null;

    private bool isProcessing = false;

    public static DatabaseAccess Inst = null;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            loginUser = new UserData();

            loginUser.id = "First3";
            loginUser.password = "1234";

            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }




    private void Start()
    {
        database = client.GetDatabase("UserData");
        collection = database.GetCollection<BsonDocument>("UserInfo");

        //GameManager.Inst.myPlayerData = loginUser;

        GetUserData(loginUser.id, loginUser.password);
        
        StartCoroutine(IsProcessing());
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
        Debug.Log(loginUser.gamemoney);

    }


    //로그인 유저 데이터 가지고 오기
    public async Task<UserData> GetUserData_FromDatabase(string _id,string _password)
    {
        BsonDocument find = new BsonDocument { { "_id", _id },{ "password",_password } };

        var allDatasTask = collection.FindAsync(find);
        var datasAwated = await allDatasTask;

        string id = "";
        string password = "";
        object array = null;

        UserData user = new UserData();

        foreach (var data in datasAwated.ToList())
        {
            id = (string)data.GetValue("_id");
            password = (string)data.GetValue("password");

            user.id = id;
            user.password = password;
            user.gamemoney = (int)data.GetValue("gamemoney");

            array = (object)data.GetValue("objname");
            user.objname = ArraySplitSort(array);

            array = (object)data.GetValue("posX");
            user.posX= ArraySplitSort(array);

            array = (object)data.GetValue("posY");
            user.posY = ArraySplitSort(array);

            array = (object)data.GetValue("posZ");
            user.posZ = ArraySplitSort(array);

            array = (object)data.GetValue("rotY");
            user.rotY = ArraySplitSort(array);

            array = (object)data.GetValue("grounds_Save");
            user.grounds_Save = ArraySplitSort(array);

            array = (object)data.GetValue("objectcount");
            //스트링으로 변환된 배열들의 값을 배열에 넣음
            user.objectcount = ArraySplitSort(array);
        }

        loginUser = user;

        Debug.Log(loginUser.posX.Length);


        isProcessing = false;

        return user;
    }

    private string[] ArraySplitSort(object str)
    {
        string[] words = str.ToString().Split(",");
        char[] charArray = str.ToString().ToCharArray();

        bool indexCheck = false;

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


}
