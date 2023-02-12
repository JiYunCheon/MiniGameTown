using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

abstract public class Item : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] protected Image picture;

    private Excel myData = null;
    public Excel GetMyData { get { return myData; } private set { } }


    private void Start()
    {
        Initialized();

        SetByCount(0);
    }

    //각 초기화 할 것들 실행
    protected abstract void Initialized();

    //데이터 삽입 
    public virtual void SetMyData(Excel data) => myData = data;

    //현재 개수를 적용하여 유아이에 표시
    public virtual void SetByCount(int value)
    {
        GameManager.Inst.SetCount(myData.name,value);
    }

    

}
