using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Excel
{
    public string name;

    public int maxCount;

    public int price;

    public int occupyPad;

    public string Info;

    public string spriteName;

    public string packageName;

    public string prefabName;

    public string alphaPrefabName;

    public int hvCheck;

    public OBJECT_TYPE myType;
}

[System.Serializable]
public class PlayerData
{
    public int gameMoney;

    public int find_B_Count;
    public string find_B_Name;


    public int memory_B_Count;
    public string memory_B_Name;


    public int puzzle_B_Count;
    public string puzzle_B_Name;


    public int balloon_B_Count;
    public string balloon_B_Name;


    public int cook_B_Count;
    public string cook_B_Name;


    public int myRoom_B_Count;
    public string myRoom_B_Name;


    public int appleTree_O_Count;
    public string appleTree_O_Name;


    public int cart_O_Count;
    public string cart_O_Name;


}