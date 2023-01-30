using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class Item : MonoBehaviour
{
    [SerializeField]
    private Data myData = null;
    public Data GetMyData { get { return myData; } private set { } }

    [Header("Image")]
    [SerializeField] 
    protected Image picture;


    private void Awake()
    {
        Initialized();
    }

    protected abstract void Initialized();

    public virtual void SetMyData(Data data) => myData = data;

}
