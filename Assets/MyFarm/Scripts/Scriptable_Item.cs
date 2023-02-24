using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum ITEMTYPE
{
    PET,
    PLANT,
    ORNAMENT,
    BACKGROUND
}


[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/ItemData")]
public class Scriptable_Item: ScriptableObject
{
    public Sprite sp;
    public int index;
    public int cost;
    public string itemName;
    public string description;
    public ITEMTYPE type;
    
    private void init(){}



    public GameObject createButtonFromItem()
    {
        GameObject obj = new GameObject();
        obj.name = "Btn" + itemName;
        obj.AddComponent<Image>().sprite=Resources.Load<Sprite>("MyFarmShop/ShopItem");

        GameObject itemImage = new GameObject();
        itemImage.AddComponent<Image>().sprite = sp;
        itemImage.transform.SetParent(obj.transform);

        Button_Item btnItm = obj.AddComponent<Button_Item>();
        btnItm.setButton(obj.AddComponent<Button>());
        btnItm.setData(this);

        GameObject child = new GameObject();

        TextMeshProUGUI text = child.AddComponent<TextMeshProUGUI>();
        child.transform.SetParent(obj.transform);
        text.text = $"АЁАн : {this.cost}";
        text.color = Color.black;
        text.fontSize = 25;
        text.alignment = (TextAlignmentOptions)TextAlignment.Left;
        text.rectTransform.Translate(new Vector3(0,-80f,0));

        return obj;
    }

    public GameObject createObjectFromItem()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("MyFarmObj"));
        obj.name = name;
        obj.GetComponent<Image>().sprite = sp;

        return obj;
    }
}
