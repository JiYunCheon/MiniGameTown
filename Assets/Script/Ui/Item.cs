using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

abstract public class Item : MonoBehaviour
{
    [SerializeField] private Excel myData = null;
    [Header("Button")]
    [SerializeField] private Button priceBtn = null;
    public Excel GetMyData { get { return myData; } private set { } }

    [Header("Image")]
    [SerializeField] protected Image picture;

    protected int count = 0;


    private void Start()
    {
        Initialized();
        SetByCount(0);
        if(priceBtn != null)
            SoldOutCheck();
    }


    public void SoldOutCheck()
    {
        if(myData.myType==OBJECT_TYPE.BUILDING && myData.maxCount==0)
        {
             priceBtn.interactable = false;
        }
      
    }


    protected abstract void Initialized();

    //µ¥ÀÌÅ¸ »ðÀÔ 
    public virtual void SetMyData(Excel data) => myData = data;


    public virtual void SetByCount(int value)
    {
        GameManager.Inst.SetCount(myData.name,value);
    }

    public void ComparerMaxCount()
    {
        if (myData.myType == OBJECT_TYPE.BUILDING && myData.maxCount > 0)
        {
            myData.maxCount--;
            priceBtn.interactable = false;
        }
    }

}
