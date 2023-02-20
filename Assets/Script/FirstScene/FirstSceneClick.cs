using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneClick : MonoBehaviour
{
    private int layer = 1 << 7;
    private RaycastHit2D hit;
    private Select saveObj;

    public Select GetSaveObj { get { return saveObj; } private set { } }

    private void Update()
    {
        InputEvent();
    }

    private void InputEvent()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 100, layer);
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * 10, Color.red, 0.3f);

            if(hit.collider!=null)
                if (hit.transform.TryGetComponent<Select>(out Select obj))
                {
                    saveObj = obj;
                    saveObj.GameSelect();
                }
        }
    }
    
    

}
