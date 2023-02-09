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
        //�����ϴ°� �� ������
        defaultMaterial = Resources.Load<Material>("Material & Texture/Colors_Alpha 1");
        redMaterial = Resources.Load<Material>("Material & Texture/Colors_Alpha 2");

        if (renderer == null)
            renderer = GetComponent<Renderer>();

    }

    //���� Ui Active ��Ʈ��
    public void Active_BuildOption(bool active = true)
    {
        buildOption.SetActive(active);
    }

    //�̸����� ��ü�� ���� ���� ����
    public void ChangeState(Ground ground, int occupyPad)
    {
        if (!ground.CompareNode(occupyPad))
            //������ ���׸���
            renderer.material = redMaterial;
        else
            //�⺻ ���׸���
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
