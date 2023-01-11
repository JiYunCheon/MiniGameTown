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
            //���⿡�� ���� ��ġ ���� �ʾ������� ����ó��.
        }

        return (launchIntent == null ? false : true);
#else

        Debug.LogError("Android������ �����Ǵ� ����Դϴ�.");
        return false;

#endif
    }


    public static void openApp(string bundleID)
    {
#if UNITY_ANDROID
        if(!IsAppInstalled(bundleID))
        {
            Debug.Log("���� ������� �ʽ��ϴ�");
            return;
        }

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage",bundleID);

        jo.Call("startActivity", intent);

#else
        Debug.LogError("Android������ �����Ǵ� ����Դϴ�.");
#endif
    }


}
