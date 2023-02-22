using System.Collections;
using System.Collections.Generic;
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
        obj.AddComponent<Image>().color = new Color(0.87f, 0.74f, 0.3f);

        GameObject itemImage = new GameObject();
        itemImage.AddComponent<Image>().sprite = sp;
        itemImage.transform.SetParent(obj.transform);

        Button_Item btnItm = obj.AddComponent<Button_Item>();
        btnItm.setButton(obj.AddComponent<Button>());
        btnItm.setData(this);



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
