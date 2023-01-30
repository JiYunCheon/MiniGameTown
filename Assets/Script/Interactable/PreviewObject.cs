using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{

    [SerializeField] private GameObject buildOption = null;

    Quaternion rotation = Quaternion.identity;
    private int count = 0;

    public void Active_BuildOption(bool active = true)
    {
        buildOption.SetActive(active);
    }


    #region Button Event

    public void OnClick_Confirm()
    {
        GameManager.Inst.GetClickManager.InstObject(rotation);
        GameManager.Inst.GetClickManager.choiceCheck = false;
        Active_BuildOption(false);
        GameManager.Inst.GetUiManager.DestroyItem();

        Destroy(this.gameObject);
    }

    public void OnClick_Rotation()
    {
        if (count == 0)
        {
            this.transform.Rotate(0, 50f,0 );
            rotation = Quaternion.Euler(0, 50f, 0);
            buildOption.transform.Rotate(0, -45f, 0);
        }
        else if(count==1)
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
        GameManager.Inst.GetClickManager.PadRefresh();
        Destroy(this.gameObject);

        GameManager.Inst.GetUiManager.On_Click_WatingMode();
    }

    #endregion
}
