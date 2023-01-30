using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    [SerializeField]
    private Data myData = null;
    public Data GetMyData { get { return myData; }private set { } }

    [Header("===ListCheck===")]
    public List<Ground> myGround = new List<Ground>();

    public virtual void SaveGround(List<Ground> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            myGround.Add(nodes[i]);
        }
    }
}
