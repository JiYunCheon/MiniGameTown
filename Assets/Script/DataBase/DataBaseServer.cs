using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Runtime.InteropServices;
using System.Reflection;

public class UserInfo : IComparable
{
    public string id;
    public string pwd;

    public string[] score;
    public int gamemoney;

    public string[] objname;
    public string[] posX;
    public string[] posY;
    public string[] posZ;
    public string[] rotY;
    public string[] grounds_Save;

    public string[] objectCount;

    public float curScore;


    public int CompareTo(object obj)
    {
        UserInfo target = (UserInfo)obj;

        /*
         * 점수 같을 때 예외처리
        if(curScore.CompareTo(target.curScore) ==0)
        {
            if(i)
        }
        else return curScore.CompareTo(target.curScore);
        */
        return curScore.CompareTo(target.curScore);
    }

}
public enum GAMETYPE
{
    FINDPICTURE,
    MemoryCard
}

public class DataBaseServer : MonoBehaviour
{
    [Header("======Server Info======")]
    [SerializeField] private string severURL = null;
    [SerializeField] private int port = 0;

    #region Member

    //cur User Info
    [HideInInspector] public UserInfo loginUser = null;
    //All UserList
    [HideInInspector] public List<UserInfo> userlist = null;
    //Overlap CheckValue
    [HideInInspector] public bool isProcessing = false;

    [HideInInspector] public bool callResult = false;

    [HideInInspector] public bool isLogin = false;

    [HideInInspector] public bool instDone = false;

    [HideInInspector] public int curRank = 0;

    #endregion

    public static DataBaseServer Inst = null;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            loginUser = new UserInfo();
            loginUser.id = "7";
            loginUser.pwd = "1234";

            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    //FindPicture Save Score
    public void SaveScore()
    {
        if (isProcessing) return;

        isProcessing = true;
        string jsonData = JsonUtility.ToJson(loginUser);
        Debug.Log(jsonData);

        StartCoroutine(ProcessSaveScore(jsonData));
    }

    private IEnumerator ProcessSaveScore(string jsonData)
    {
        string targetURL = severURL + ":" + port + "/saveuserscore";

        using (UnityWebRequest request = UnityWebRequest.Post(targetURL, jsonData))
        {

            byte[] jsonTOSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

            request.uploadHandler = new UploadHandlerRaw(jsonTOSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                UnityEngine.Debug.Log(request.error);
            }
            else
            {
                UnityEngine.Debug.Log(request.downloadHandler.text);
            }

            isProcessing = false;
        }
    }


    //FindPicture Save Score
    public void Login()
    {
        if (isProcessing) return;

        isProcessing = true;

        string jsonData = JsonUtility.ToJson(loginUser);

        StartCoroutine(ProcessLogin(jsonData));
    }

    private IEnumerator ProcessLogin(string jsonData)
    {
        string targetURL = severURL + ":" + port + "/Userlogin";

        using (UnityWebRequest request = UnityWebRequest.Post(targetURL, jsonData))
        {

            byte[] jsonTOSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

            request.uploadHandler = new UploadHandlerRaw(jsonTOSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();


            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                UnityEngine.Debug.Log(request.error);
            }
            else
            {

                Debug.Log(request.downloadHandler.text);
                loginUser = JsonConvert.DeserializeObject<UserInfo>(request.downloadHandler.text);

                GameManager.Inst.objectsName = loginUser.objname;
                GameManager.Inst.objectsPos = new Vector3[loginUser.objname.Length];
                GameManager.Inst.objectsRot = new Vector3[loginUser.objname.Length];
                GameManager.Inst.grounds_Info = new int[loginUser.objname.Length];


                int number = 0;
                GameManager.Inst.myPlayerData.objectCount=new int[loginUser.objectCount.Length];

                for (int i = 0; i < GameManager.Inst.myPlayerData.objectCount.Length; i++)
                {
                    if(!int.TryParse(loginUser.objectCount[i], out number))
                    {
                        GameManager.Inst.myPlayerData.objectCount[i] = number;
                    }
                    else
                    {
                        GameManager.Inst.myPlayerData.objectCount[i] = int.Parse(loginUser.objectCount[i]);
                    }
                }
                GameManager.Inst.myPlayerData.gameMoney = loginUser.gamemoney;



                for (int i = 0; i < loginUser.objname.Length; i++)
                {
                    GameManager.Inst.objectsPos[i].x = float.Parse(loginUser.posX[i]);
                    GameManager.Inst.objectsPos[i].y = float.Parse(loginUser.posY[i]);
                    GameManager.Inst.objectsPos[i].z = float.Parse(loginUser.posZ[i]);

                    GameManager.Inst.objectsRot[i].y= float.Parse(loginUser.rotY[i]);
                }

            }

            isProcessing = false;
        }
    }
    //Call Method
    public void Call_GetUserInfo()
    {
        StartCoroutine(GetUserInfo());
    }

    private IEnumerator GetUserInfo()
    {
        string targetURL = severURL + ":" + port + "/getuserlist";

        using (UnityWebRequest request = UnityWebRequest.Get(targetURL))
        {
            yield return request.SendWebRequest();

            userlist = JsonConvert.DeserializeObject<List<UserInfo>>(request.downloadHandler.text);


            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                UnityEngine.Debug.Log(request.error);
            }
            else
            {
                Debug.Log(userlist[0].id);
                for (int i = 0; i < userlist.Count; i++)
                {
                    for (int j = 0; j < userlist[i].score.Length; j++)
                    {
                        Debug.Log(userlist[i].score[j]);
                    }
                }
            }

            isProcessing = false;

            yield return null;
        }
    }

 















    public int CalulateUserRank()
    {
        for (int i = 0; i < userlist.Count; i++)
        {
            if (loginUser.id == userlist[i].id)
            {
                loginUser = userlist[i];

                return curRank = i + 1;
            }
        }

        return 0;
    }


}


