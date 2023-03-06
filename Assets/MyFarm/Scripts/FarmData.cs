using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmData : MonoBehaviour
{
    public static FarmData Inst;
    public float score;
    //public PlayerInfo info; 

    [Header("MyItems")]
    public List<Scriptable_Item> myItemLis = new List<Scriptable_Item>();

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Inst == null) Inst = this;
        else Destroy(gameObject);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
