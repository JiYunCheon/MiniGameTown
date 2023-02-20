using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    [SerializeField] private Transform spawnTr = null;
    [SerializeField] private PlayerMove playerPrefab = null;
    private PlayerMove player=null;

    private void Start()
    {
        player = Instantiate<PlayerMove>(playerPrefab, spawnTr.position,Quaternion.identity);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.PlayerDestination();
        }
    }
}
