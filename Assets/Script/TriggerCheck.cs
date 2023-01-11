using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMove player = null;

        if (other.gameObject.TryGetComponent<PlayerMove>(out player))
        {
            GameManager.Inst.GetUiManager.ActiveObj();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMove player = null;

        if (other.gameObject.TryGetComponent<PlayerMove>(out player))
        {
            GameManager.Inst.GetUiManager.ActiveObj(false);
        }
    }

}
