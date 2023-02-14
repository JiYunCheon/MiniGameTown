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

    public string[] objectcount;
}


public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://CheonJiYun:wldbs3456@cluster0.yyfatob.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    UserData loginUser = null;

    private bool isProcessing = false;

    private void Start()
    {
        database = client.GetDatabase("UserData");
        collection = database.GetCollection<BsonDocument>("UserInfo");

        GetUserData("First2",1234.ToString());

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
        Debug.Log(loginUser.objectcount[0]);
        Debug.Log(loginUser.objectcount[1]);

    }


    //모든 유저 데이터 가지고 오기
    public async Task<UserData> GetUserData_FromDatabase(string _id,string _password)
    {
        BsonDocument find = new BsonDocument { { "_id", _id },{ "password",_password } };

        var allDatasTask = collection.FindAsync(find);
        var datasAwated = await allDatasTask;

        string id = "";
        string password = "";
        object objCount = null;

        UserData user = new UserData();

        foreach (var data in datasAwated.ToList())
        {
            id = (string)data.GetValue("_id");
            password = (string)data.GetValue("password");
            objCount = (object)data.GetValue("objectcount");

            user.id = id;
            user.password = password;

            //스트링으로 변환된 배열들의 값을 배열에 넣음
            user.objectcount = ArraySplitSort(objCount);
        }

        loginUser = user;

        isProcessing = false;

        return user;
    }

    private string[] ArraySplitSort(object str)
    {
        string[] words = null;

        words = str.ToString().Split(",");
        words[0] = words[0].Split("[")[1];
        words[words.Length - 1] = words[words.Length - 1].Split("]")[0];

        return words;
    }
}
