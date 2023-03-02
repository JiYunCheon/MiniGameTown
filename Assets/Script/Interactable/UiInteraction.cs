using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInteraction : Interactable
{
    [SerializeField] private GameObject uiObject = null;

    WaitUntil waitUntil = null;
    Coroutine activeUiCoroutine = null;

    private void Awake()
    {
        waitUntil = new WaitUntil(()=>GetEntrance.triggerCheck);
    }
    private void Active_Ui(bool check = true)
    {
        uiObject.SetActive(check);
    }

    public override void Select_InteractableObj()
    {
        Debug.Log("¤¾¤·");
        activeUiCoroutine = StartCoroutine(UiSequnce());
    }

    public override void DeSelect_InteractableObj()
    {
        StopCoroutine(activeUiCoroutine);
        Active_Ui(false);
    }

    IEnumerator UiSequnce()
    {
        yield return waitUntil;
        Active_Ui();
        yield return null;
    }


}
