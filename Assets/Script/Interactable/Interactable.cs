using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    private Data myData = null;

    protected new Renderer renderer = null;

    private Transform contentTr = null;

    private InventoryItem myInvenItem = null;
    [SerializeField] private Transform cameraPos = null;

    public Transform GetCameraPos { get { return cameraPos; } private set { } }

    public InventoryItem GetInventoryItem { get { return myInvenItem; } private set { } }

    public Data GetMyData { get { return myData; }private set { } }

    [Header("===ListCheck===")]
    public List<Ground> myGround = new List<Ground>();


    private void Start()
    {
        Initialized();

        CompareItem();
    }

    public virtual void SaveGround(List<Ground> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            myGround.Add(nodes[i]);
        }
    }

    private void CompareItem()
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
    public virtual void SetMyData(Data data) => myData = data;

    private void Initialized()
    {
        renderer = GetComponent<Renderer>();
        Debug.Log(renderer==null);
    }

    public void DownPos()
    {
        StartCoroutine(Down());
    }


    IEnumerator Down()
    {
        while (true)
        {
            transform.Translate(0, -0.2f, 0);

            if (transform.localPosition.y < 1.41f)
            {
                Vector3 pos = transform.localPosition;
                pos.y = 1.4f;
                transform.localPosition = pos;   
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
