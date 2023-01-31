using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    [SerializeField]
    private Data myData = null;

    private Transform contentTr = null;

    private InventoryItem myInvenItem = null;
    public InventoryItem GetInventoryItem { get { return myInvenItem; } private set { } }

    public Data GetMyData { get { return myData; }private set { } }

    [Header("===ListCheck===")]
    public List<Ground> myGround = new List<Ground>();

    private void Start()
    {
        CompareItem();
    }

    public virtual void SaveGround(List<Ground> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            myGround.Add(nodes[i]);
        }
    }

    public virtual void CompareItem()
    {
        contentTr = GameManager.Inst.GetUiManager.GetInvenContentTr;

        foreach (Transform obj in contentTr)
        {
            if(obj.TryGetComponent<InventoryItem>(out InventoryItem item))
            {
                if(item.GetMyData==this.GetMyData)
                {
                    myInvenItem = item;
                }
            }
        }
    }

}
