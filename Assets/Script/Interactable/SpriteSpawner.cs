using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Vector3[] spawnSpot = null;
    [SerializeField] private float spawnInterval = 0;
    [SerializeField] private float stopTime = 0;

    List<GameObject> objectList = new List<GameObject>();

    public void SpawnStart()
    {
        StartCoroutine(TimeControll());
    }

    IEnumerator TimeControll()
    {
        WaitForSeconds scecound = new WaitForSeconds(stopTime);
        Coroutine moveCoroutine = StartCoroutine(RandomSpawn());

        yield return scecound;
        StopCoroutine(moveCoroutine);

        for (int i = 0; i < objectList.Count; i++)
        {
            Destroy(objectList[i]);
        }
    }



    IEnumerator RandomSpawn()
    {
        int randomPoint = 0;
        GameObject obj = null;
        WaitForSeconds scecound = new WaitForSeconds(spawnInterval);
        while(true)
        {
            randomPoint = Random.Range(0, spawnSpot.Length);
            obj = Instantiate(prefab, spawnSpot[randomPoint], prefab.transform.rotation);
            objectList.Add(obj);
            yield return scecound;
        }
    }




}
