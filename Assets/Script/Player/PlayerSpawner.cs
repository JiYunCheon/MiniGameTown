using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] player = null;
    [SerializeField] private Vector3[] spawnSpot = null;


    private void Start()
    {
        ChangeCamPosByScene(DatabaseAccess.Inst.loginUser.selectNum);

    }

    private void ChangeCamPosByScene(int index)
    {

        if (GameManager.Inst.curSceneNum == 1 && SceneManager.GetActiveScene().buildIndex == 2 && spawnSpot.Length == 3)
        {
            Instantiate(player[index], spawnSpot[0], Quaternion.identity);
        }
        else if (GameManager.Inst.curSceneNum == 3 && SceneManager.GetActiveScene().buildIndex == 2 && spawnSpot.Length == 3)
        {
            Instantiate(player[index], spawnSpot[1], Quaternion.identity);
        }
        else if (GameManager.Inst.curSceneNum == 4 && SceneManager.GetActiveScene().buildIndex == 2 && spawnSpot.Length == 3)
        {
            Instantiate(player[index], spawnSpot[2], Quaternion.identity);
        }
        else if (GameManager.Inst.curSceneNum == 2 && SceneManager.GetActiveScene().buildIndex == 3 && spawnSpot.Length == 2)
        {
            Instantiate(player[index], spawnSpot[0], Quaternion.identity);
        }
        else if (GameManager.Inst.curSceneNum == 4 && SceneManager.GetActiveScene().buildIndex == 3 && spawnSpot.Length == 2)
        {
            Instantiate(player[index], spawnSpot[1], Quaternion.identity);
        }
        else if (GameManager.Inst.curSceneNum == 2 && SceneManager.GetActiveScene().buildIndex == 4 && spawnSpot.Length == 2)
        {
            Instantiate(player[index], spawnSpot[1], Quaternion.identity);
        }
        else if (GameManager.Inst.curSceneNum == 3 && SceneManager.GetActiveScene().buildIndex == 4 && spawnSpot.Length == 2)
        {
            Instantiate(player[index], spawnSpot[0], Quaternion.identity);
        }

    }

}
