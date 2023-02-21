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
        if(myData!=null)
        {
            explainText.text = myData.explain;
            character.sprite = Resources.Load<Sprite>($"Explain_Image/{myData.character}");
        }
    }

}
