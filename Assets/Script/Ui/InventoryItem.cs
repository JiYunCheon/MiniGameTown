using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class InventoryItem : Item
{
    [SerializeField] private TextMeshProUGUI countText = null;

    //�̹����� �����Ϳ� ���� �ٲ�
    protected override void Initialized()
    {
        picture.sprite = Resources.Load<Sprite>($"Shop&Inventory_Image/Item_Image/{GetMyData.spriteName}");
    }

    //�����Ϳ� ī��Ʈ�� ��ų� ���ϰ� ���������� ǥ���ϰ� �ؽ�Ʈ�� ǥ��
    public override void SetByCount(int _value)
    {
        Debug.Log(_value);
        int count = GameManager.Inst.TrySetValue(countIndex, _value);

        if (count <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        countText.text = count.ToString();
    }


    #region Button Event

    //�������� ������ �� ����
    public void OnClick_Item()
    {
        GameManager.Inst.GetClickManager.SetInfo(this,GetMyData);

        GameManager.Inst.GetUiManager.On_Click_BuildingMode();

    }

    #endregion

}
