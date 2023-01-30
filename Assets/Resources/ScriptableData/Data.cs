using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="BuildingData",menuName = "ScriptableObject/BuildingData",order =1)]
public class Data : ScriptableObject
{
    [SerializeField] 
    private int price = 0;
    public int Price { get { return price; } private set { } }

    [SerializeField] 
    private int occupyPad = 0;
    public int OccupyPad { get { return occupyPad; } private set { } }

    [SerializeField] 
    private string gameName = null;
    public string GameName { get { return gameName; } private set { } }

    [SerializeField] 
    private Interactable prefab = null;
    public Interactable Prefab { get { return prefab; } private set { } }

    [SerializeField] 
    private PreviewObject alphaPrefab = null;
    public PreviewObject AlphaPrefab { get { return alphaPrefab; } private set { } }

    [SerializeField] 
    private GAMETYPE myType;
    public GAMETYPE MyType { get { return myType; } private set { } }

    [SerializeField] 
    private string spriteName = null;
    public string SpriteName { get { return spriteName; } private set { } }

    [SerializeField] 
    private string packageName = null;
    public string PackageName { get { return packageName; } private set { } }


}
