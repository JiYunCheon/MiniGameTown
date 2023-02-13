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
    [HideInInspector] public int countIndex = 0;


    private Excel myData = null;
    public Excel GetMyData { get { return myData; } private set { } }


    private void Start()
    {
        Initialized();

        SetByCount(0);
    }

    //�� �ʱ�ȭ �� �͵� ����
    protected abstract void Initialized();

    //������ ���� 
    public virtual void SetMyData(Excel data) => myData = data;

    //���� ������ �����Ͽ� �����̿� ǥ��
    public virtual void SetByCount(int value)
    {
        GameManager.Inst.TrySetValue(countIndex, value);
    }

    

}
