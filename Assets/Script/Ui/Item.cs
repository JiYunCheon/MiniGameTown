using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

abstract public class Item : MonoBehaviour
{
    [SerializeField]
    private Data myData = null;

    public int count = 0;

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
    protected virtual void SetCount(int _value)
    {
        if (GameManager.Inst.datas.TryGetValue(GetMyData.GameName, out int value))
        {
            count = value + _value;
            GameManager.Inst.datas[GetMyData.GameName] = count;
        }
    }

}
