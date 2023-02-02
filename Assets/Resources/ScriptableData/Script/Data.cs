using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="BuildingData",menuName = "ScriptableObject/BuildingData",order =1)]
public class Data : ScriptableObject
{
    [SerializeField]
    private int maxCount = 0;
    public int MaxCount { get { return maxCount; } set { maxCount = value; } }

    [SerializeField] 
    private int price = 0;
    public int Price { get { return price; } private set { } }

    [SerializeField] 
    private int occupyPad = 0;
    public int OccupyPad { get { return occupyPad; } private set { } }

    [SerializeField] 
    private string gmaeInfo = null;
    public string GameInfo { get { return gmaeInfo; } private set { } }

    [SerializeField] 
    private string spriteName = null;
    public string SpriteName { get { return spriteName; } private set { } }

    [SerializeField] 
    private string packageName = null;
    public string PackageName { get { return packageName; } private set { } }

    [SerializeField]
    private string prefabName = null;
    public string PrefabName { get { return prefabName; } private set { } }

    [SerializeField]
    private string alphaPrefabName = null;
    public string AlphaPrefabName { get { return alphaPrefabName; } private set { } }

    [SerializeField]
    private string gameName = null;
    public string GameName { get { return gameName; } private set { } }

    [SerializeField]
    private OBJECT_TYPE myType;
    public OBJECT_TYPE MyType { get { return myType; } private set { } }
}
