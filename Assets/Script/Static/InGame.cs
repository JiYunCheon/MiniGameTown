using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InGame
{
    public static bool IsAppInstalled(string bundleID)
    {

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;

        //if the app is installed, no errors. Else, doesn't get past next line
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleID);
        }
        catch (Exception ex)
        {
            Debug.LogError("exception" + ex.Message);
            //여기에서 앱이 설치 되지 않았을때의 예외처리.
        }

        return (launchIntent == null ? false : true);

    }


    public static void openApp(string bundleID)
    {
        if(!IsAppInstalled(bundleID))
        {
            Debug.LogError("앱이 깔려있지 않습니다");
            return;
        }

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleID);

        jo.Call("startActivity", intent);

        //Application.Quit();

    }

    public static void ExitGame()
    {
        Application.Quit();
    }

}
