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
    public string[] shopmaxcount;
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
        database = client.GetDatabase("UserData");
        collection = database.GetCollection<BsonDocument>("UserInfo");

        //GetUserData(loginUser.id, loginUser.password);
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
        SceneManager.LoadScene("2DTown");

    }



    //�α��� ���� ������ ������ ����
    public async Task<UserData> GetUserData_FromDatabase(string _id,string _password)
    {
        BsonDocument find = new BsonDocument { { "_id", _id },{ "password",_password } };

        var allDatasTask = collection.FindAsync(find);
        var datasAwated = await allDatasTask;

        object arrayString = null;

        UserData user = new UserData();

        foreach (var data in datasAwated.ToList())
        {
            user.id = (string)data.GetValue("_id");
            user.password = (string)data.GetValue("password");
            user.gamemoney = (int)data.GetValue("gamemoney");

            arrayString = (object)data.GetValue("objname");
            //��Ʈ������ ��ȯ�� �迭���� ���� �迭�� ����
            user.objname = ArraySplitSort(arrayString);

            arrayString = (object)data.GetValue("posX");
            user.posX= ArraySplitSort(arrayString);

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

        isProcessing = false;

        if (loginUser.id == null)
        {
            Debug.Log("�α��� ����");

            return null;
        }

        Debug.Log("�α��� ����");

        StartCoroutine(IsProcessing());

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
