using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PictureObject : Select
{
    [SerializeField] private string packageName = null;
   
    public string GetPackageName { get { return packageName; } private set { } }

    public override void GameSelect()
    {
        GameManager.Inst.curGameName = packageName;

        if (GameManager.Inst.curGameName == "5.PlazaScene")
        {
            GameManager.Inst.curSceneNum = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("5.PlazaScene");
        }
        else
        {
            InGame.openApp(GameManager.Inst.curGameName);
        }
    }

    
}
