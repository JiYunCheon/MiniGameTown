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
    public string gameMoney_Key;

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


    public int cat_Black_O_Count;
    public string cat_Black_O_Name;


    public int cat_White_O_Count;
    public string cat_White_O_Name;


    public int fence_End_Short_O_Count;
    public string fence_End_Short_O_Name;


    public int fence_End_Vertical_O_Count;
    public string fence_End_Vertical_O_Name;


    public int fence_End_Count;
    public string fence_End_O_Name;


    public int flower_1x_Orange_Count;
    public string flower_1x_Orange_Name;


    public int flower_1x_Purple_Count;
    public string flower_1x_Purple_Name;


    public int flowers_BlueLight_Count;
    public string flowers_BlueLight_Name;


    public int flowers_Pink_Count;
    public string flowers_Pink_Name;


    public int garbageCan_Blue_Count;
    public string garbageCan_Blue_Name;


    public int garbageCan_Red_Count;
    public string garbageCan_Red_Name;


    public int lantern_Path_Count;
    public string lantern_Path_Name;


    public int lantern_Small_Count;
    public string lantern_Small_Name;


    public int pineTree_Bright_Count;
    public string pineTree_Bright_Name;


    public int pineTree_Snow_1_Count;
    public string pineTree_Snow_1_Name;


    public int pineTree_Snow_2_Count;
    public string pineTree_Snow_2_Name;


    public int tree_Fruits_Plums_Count;
    public string Tree_Fruits_Plums_Name;


    public int trunk_Count;
    public string trunk_Name;

    public int trunk_x3_Count;
    public string trunk_x3_Name;

    public int umbrella_Purple_Count;
    public string umbrella_Purple_Name;


    public int umbrella_Red_Count;
    public string umbrella_Red_Name;


    public int balloonStand_Count;
    public string balloonStand_Name;



    public int TrySetValue(string key, int value)
    {
        int calValue = -1;

        switch(key)
        {
            case "GameMoney":
                calValue = Calulation(ref gameMoney,value);
                break;

            case "FindPicture":
                calValue = Calulation(ref find_B_Count, value);
                break;

            case "MemoryCard":
                calValue = Calulation(ref memory_B_Count, value);
                break;

            case "Puzzle":
                calValue = Calulation(ref puzzle_B_Count, value);
                break;

            case "Balloon":
                calValue = Calulation(ref balloon_B_Count, value);
                break;
            case "Cook":
                calValue = Calulation(ref cook_B_Count, value);
                break;

            case "MyRoom":
                calValue = Calulation(ref myRoom_B_Count, value);
                break;

            case "AppleTree":
                calValue = Calulation(ref appleTree_O_Count, value);
                break;

            case "Cart":
                calValue = Calulation(ref cart_O_Count, value);
                break;

            case "Cat_Black":
                calValue = Calulation(ref cat_Black_O_Count, value);
                break;

            case "Cat_White":
                calValue = Calulation(ref cat_White_O_Count, value);
                break;

            case "Fence_End_Short":
                calValue = Calulation(ref fence_End_Short_O_Count, value);
                break;

            case "Fence_End_Vertical":
                calValue = Calulation(ref fence_End_Vertical_O_Count, value);
                break;

            case "Fence_End":
                calValue = Calulation(ref fence_End_Count, value);
                break;

            case "Flower_1x_Orange":
                calValue = Calulation(ref flower_1x_Orange_Count, value);
                break;

            case "Flower_1x_Purple":
                calValue = Calulation(ref flower_1x_Purple_Count, value);
                break;

            case "Flowers_BlueLight":
                calValue = Calulation(ref flowers_BlueLight_Count, value);
                break;

            case "Flowers_Pink":
                calValue = Calulation(ref flowers_Pink_Count, value);
                break;

            case "GarbageCan_Blue":
                calValue = Calulation(ref garbageCan_Blue_Count, value);
                break;

            case "GarbageCan_Red":
                calValue = Calulation(ref garbageCan_Red_Count, value);
                break;

            case "Lantern_Path":
                calValue = Calulation(ref lantern_Path_Count, value);
                break;

            case "Lantern_Small":
                calValue = Calulation(ref lantern_Small_Count, value);
                break;

            case "PineTree_Bright":
                calValue = Calulation(ref pineTree_Bright_Count, value);
                break;

            case "PineTree_Snow_1":
                calValue = Calulation(ref pineTree_Snow_1_Count, value);
                break;

            case "PineTree_Snow_2":
                calValue = Calulation(ref pineTree_Snow_2_Count, value);
                break;

            case "Tree_Fruits_Plums":
                calValue = Calulation(ref tree_Fruits_Plums_Count, value);
                break;

            case "Trunk":
                calValue = Calulation(ref trunk_Count, value);
                break;

            case "Trunk_x3":
                calValue = Calulation(ref trunk_x3_Count, value);
                break;

            case "Umbrella_Purple":
                calValue = Calulation(ref umbrella_Purple_Count, value);
                break;

            case "Umbrella_Red":
                calValue = Calulation(ref umbrella_Red_Count, value);
                break;

            case "BalloonStand":
                calValue = Calulation(ref balloonStand_Count, value);
                break;
        }

        if (calValue == -1)
            Debug.LogError("OMG ::: No Data");

        return calValue;
    }


    private int Calulation(ref int value, int _value)
    {
        value += _value;
        if (value < 0)
            value = 0;

        return value;
    }








}