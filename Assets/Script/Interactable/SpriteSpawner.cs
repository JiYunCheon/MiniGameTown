using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefab = null;
    private Vector3[] spawnSpot = null;
    [SerializeField] private int spotLenght = 0; 

    [SerializeField] private int repeat = 0;


    private void Start()
    {
        spawnSpot = new Vector3[spotLenght];

        for (int i = 0; i < spotLenght; i++)
        {
            spawnSpot[i] = new Vector3(transform.position.x + (-5f*i), transform.position.y, transform.position.z);
        }
    }


    public void SpawnStart()
    {
        GameObject obj = null;

        int randomIndex = 0;
        int randomPoint = 0;

        SoundManager.Inst.PlaySFX("SFX_Interact_Balloon");

        for (int i = 0; i < repeat; i++)
        {
            randomIndex = Random.Range(0, prefab.Length);
            randomPoint = Random.Range(0, spawnSpot.Length);

            obj = Instantiate(prefab[randomIndex], spawnSpot[randomPoint], prefab[randomIndex].transform.rotation);
        }
       
    }



}
