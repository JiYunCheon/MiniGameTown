using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image picture;

    #region GameData

    private int occupyPad = 0;
    private InteractionObject prefab = null;
    private PreviewObject alphaPrefab = null;
    private GAMETYPE myType;

    #endregion

    public void ChangeImage(string imageName)
    {
        Debug.Log(imageName);
        picture.sprite = Resources.Load<Sprite>(imageName);
        Debug.Log(picture.name);
    }

    public void Initialized(PreviewObject alphaPrefab, InteractionObject prefab, int occupyPad , GAMETYPE type)
    {
        this.alphaPrefab = alphaPrefab;
        this.prefab = prefab;
        this.occupyPad = occupyPad;
        this.myType = type;
    }

    #region Button Event

    public void OnClick_Item()
    {
        GameManager.Inst.GetClickManager.SetPrefab(alphaPrefab, prefab, occupyPad);
        GameManager.Inst.GetUiManager.GetShopBoard.TypeChange(myType);
        GameManager.Inst.GetUiManager.SetItem(this);
        GameManager.Inst.GetUiManager.On_Click_BuildingMode();
    }

    #endregion

}
