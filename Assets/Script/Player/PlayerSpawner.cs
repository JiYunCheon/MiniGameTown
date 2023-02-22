using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] player = null;
    [SerializeField] private Vector3[] spawnSpot = null;

    private Vector3 curSpot = Vector3.zero;

    private void Start()
    {
        SpawnPlayer(DatabaseAccess.Inst.loginUser.selectNum);
    }


    public void SpawnPlayer(int index)
    {
        Instantiate(player[index], spawnSpot[0], Quaternion.identity);
    }

}
