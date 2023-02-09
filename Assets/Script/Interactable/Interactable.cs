using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    private Excel myData = null;

    protected new Renderer renderer = null;

    private Transform contentTr = null;

    private InventoryItem myInvenItem = null;
    [SerializeField] private Transform cameraPos = null;
    [SerializeField] private Transform nameTr = null;
    public Transform GetCameraPos { get { return cameraPos; } private set { } }

    public InventoryItem GetInventoryItem { get { return myInvenItem; } private set { } }

    public Excel GetMyData { get { return myData; }private set { } }

    [Header("===ListCheck===")]
    public List<Ground> myGround = new List<Ground>();


    private void Start()
    {

        Initialized();

        CompareItem();
    }

    //주변패드의 정보를 저장
    public virtual void SaveGround(List<Ground> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            myGround.Add(nodes[i]);
        }
    }

    //자신의 인벤 아이템을 찾음
    //데이터만 바꿔주면 되니까 필요 없을 듯
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

    public virtual void SetMyData(Excel data) => myData = data;


    private void Initialized()
    {
        renderer = GetComponent<Renderer>();
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

    //생성된 건물의 로테이션에 따른 이름 회전값 변경
    public void NameRotate(float rotationY)
    {
        if(nameTr != null)
            nameTr.Rotate(0, -rotationY, 0);
    }

    public void Active_Name(bool check = true)
    {
        if (nameTr != null)
            nameTr.gameObject.SetActive(check);
    }


    public void ChangeState(bool state , Color color)
    {
        for (int i = 0; i < myGround.Count; i++)
        {
            myGround[i].ChangePadState(state,color);
        }
    }

    protected virtual void CompleteEffect()
    {
        GameManager.Inst.GetEffectManager.Inst_SpriteEffect(this.transform.position+ new Vector3(0,3.5f,0), "EffectImage/MakeComplete_Image");
    }
    public virtual void Select_InteractableObj() { }
    public virtual void DeSelect_Select_InteractableObj() { }
}
