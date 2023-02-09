using UnityEngine;

public class PreviewObject : Interactable
{

    [SerializeField] private GameObject buildOption = null;
    private Material defaultMaterial = null;
    private Material redMaterial = null;

    Quaternion rotation = Quaternion.identity;
    private int count = 0;


    private void Awake()
    {
        defaultMaterial = Resources.Load<Material>("Material & Texture/Colors_Alpha 1");
        redMaterial = Resources.Load<Material>("Material & Texture/Colors_Alpha 2");

        if (renderer == null)
            renderer = GetComponent<Renderer>();

    }

    //편집 Ui Active 컨트롤
    public void Active_BuildOption(bool active = true)
    {
        buildOption.SetActive(active);
    }

    //미리보기 객체의 현재 상태 변경
    public void ChangeState(Ground ground, int occupyPad)
    {
        if (!ground.CompareNode(occupyPad))
            //빨간색 메테리얼
            renderer.material = redMaterial;
        else
            //기본 메테리얼
            renderer.material = defaultMaterial;
    }



    #region Button Event

    public void OnClick_Confirm()
    {
        if (!GameManager.Inst.GetClickManager.InstCompare())
        {
            GameManager.Inst.GetEffectManager.Inst_SpriteUiEffect(GameManager.Inst.CurMousePos(), "EffectImage/MakeFailed_Image");
            return;
        }

        GameManager.Inst.GetClickManager.InstObject(rotation);
        Active_BuildOption(false);

        Destroy(this.gameObject);
    }

    public void OnClick_Rotation()
    {
        if (count == 0)
        {
            this.transform.Rotate(0, 50f, 0);
            rotation = Quaternion.Euler(0, 50f, 0);
            buildOption.transform.Rotate(0, -45f, 0);
        }
        else if (count == 1)
        {
            this.transform.Rotate(0, -50f, 0);
            rotation = Quaternion.identity;
            buildOption.transform.Rotate(0, 45f, 0);
            count = 0;
            return;
        }
        count++;
    }

    public void OnClick_Exit()
    {
        GameManager.Inst.GetClickManager.GetCur_Inven_Item.SetByCount(1);


        GameManager.Inst.GetClickManager.PadRefresh();

        GameManager.Inst.GetUiManager.On_Click_WatingMode();

        Destroy(this.gameObject);
    }



    #endregion
}
