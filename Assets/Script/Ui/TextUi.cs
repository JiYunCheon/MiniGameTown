using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI explainText = null;
    [SerializeField] private Image character = null;


    private Excel myData = null;
    public virtual void SetMyData(Excel data) => myData = data;

    public void SetExplain()
    {
        string[] text = null;
        int random = 0;
        if (text==null)
        {
            text = new string[3];
            text[0] = myData.explain_1;
            text[1] = myData.explain_2;
            text[2] = myData.explain_3;
        }


        if (myData!=null)
        {
            random = Random.Range(0, text.Length);

            explainText.text = text[random];
            character.sprite = Resources.Load<Sprite>($"Explain_Image/{myData.character}");
        }
    }

}
