using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MyFarmObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform rt = null;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(null);
        MyFarmManager.Inst.startDragObject(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
       // Debug.Log("DRAG");
        rt.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
        //transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MyFarmManager.Inst.endDragObject(gameObject);
        
    }
   
}
