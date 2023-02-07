using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

abstract public class Item : MonoBehaviour
{
    [SerializeField] private Excel myData = null;

    public Excel GetMyData { get { return myData; } private set { } }

    [Header("Image")]
    [SerializeField] protected Image picture;

    protected int count = 0;

    private void Start()
    {
        Initialized();
        SetByCount(0);
    }


    protected abstract void Initialized();

    //µ¥ÀÌÅ¸ »ðÀÔ 
    public virtual void SetMyData(Excel data) => myData = data;


    protected virtual void SetByCount(int value)
    {
        GameManager.Inst.SetCount(myData.name,value);
    }


}
