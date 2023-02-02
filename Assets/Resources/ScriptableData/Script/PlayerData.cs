using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData", order = 2)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int gameMoney = 0;
    public int GameMoney { get { return gameMoney; } set { gameMoney = value; } }
    [SerializeField]
    private string _gameMoney = null;
    public string _GameMoney { get { return _gameMoney; } set { _gameMoney = value; } }



    [SerializeField]
    private int find_B_Count = 0;
    public int Find_B_Count { get { return find_B_Count; } set { find_B_Count = value; } }
    [SerializeField]
    private string _find_B_Count = null;
    public string _Find_B_Count { get { return _find_B_Count; } set { _find_B_Count = value; } }



    [SerializeField]
    private int memory_B_Count = 0;
    public int Memory_B_Count { get { return memory_B_Count; } set { memory_B_Count = value; } }
    [SerializeField]
    private string _memory_B_Count = null;
    public string _Memory_B_Count { get { return _memory_B_Count; } set { _memory_B_Count = value; } }



    [SerializeField]
    private int puzzle_B_Count = 0;
    public int Puzzle_B_Count { get { return puzzle_B_Count; } set { puzzle_B_Count = value; } }
    [SerializeField]
    private string _puzzle_B_Count = null;
    public string _Puzzle_B_Count { get { return _puzzle_B_Count; } set { _puzzle_B_Count = value; } }



    [SerializeField]
    private int balloon_B_Count = 0;
    public int Balloon_B_Count { get { return balloon_B_Count; } set { balloon_B_Count = value; } }
    [SerializeField]
    private string _balloon_B_Count = null;
    public string _Balloon_B_Count { get { return _balloon_B_Count; } set { _balloon_B_Count = value; } }


    [SerializeField]
    private int cook_B_Count = 0;
    public int Cook_B_Count { get { return cook_B_Count; } set { cook_B_Count = value; } }
    [SerializeField]
    private string _cook_B_Count = null;
    public string _Cook_B_Count { get { return _cook_B_Count; } set { _cook_B_Count = value; } }



    [SerializeField]
    private int myRoom_B_Count = 0;
    public int MyRoom_B_Count { get { return myRoom_B_Count; } set { myRoom_B_Count = value; } }
    [SerializeField]
    private string _myRoom_B_Count = null;
    public string _MyRoom_B_Count { get { return _myRoom_B_Count; } set { _myRoom_B_Count = value; } }



    [SerializeField]
    private int appleTree_O_Count = 0;
    public int AppleTree_O_Count { get { return appleTree_O_Count; } set { appleTree_O_Count = value; } }
    [SerializeField]
    private string _appleTree_O_Count = null;
    public string _AppleTree_O_Count { get { return _appleTree_O_Count; } set { _appleTree_O_Count = value; } }



    [SerializeField]
    private int cart_O_Count = 0;
    public int Cart_O_Count { get { return cart_O_Count; } set { cart_O_Count = value; } }
    [SerializeField]
    private string _cart_O_Count = null;
    public string _Cart_O_Count { get { return _cart_O_Count; } set { _cart_O_Count = value; } }





}
