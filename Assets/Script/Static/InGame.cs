using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InGame
{
    

    public static bool IsAppInstalled(string bundleID)
    {
#if UNITY_ANDROID
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
            Debug.Log("exception" + ex.Message);
            //여기에서 앱이 설치 되지 않았을때의 예외처리.
        }

        return (launchIntent == null ? false : true);
#else

        Debug.LogError("Android에서만 지원되는 기능입니다.");
        return false;

#endif
    }


    public static void openApp(string bundleID)
    {
#if UNITY_ANDROID
        if(!IsAppInstalled(bundleID))
        {
            Debug.Log("앱이 깔려있지 않습니다");
            return;
        }

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleID);

        jo.Call("startActivity", intent);

#else
        Debug.LogError("Android에서만 지원되는 기능입니다.");
#endif
    }


}
