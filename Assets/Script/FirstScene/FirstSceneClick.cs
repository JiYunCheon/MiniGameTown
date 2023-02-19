using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneClick : MonoBehaviour
{
    private int layer = 1 << 7;
    private RaycastHit2D hit;
    private Select saveObj;

    private Vector3 biggerScale;
    private Vector3 originScale;

    private void Awake()
    {
        biggerScale = new Vector3(2, 2, 1);
        originScale = new Vector3(1.5f, 1.5f, 1);
    }

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
                    saveObj.ChangeScale(biggerScale);
                    saveObj.GameSelect();
                }
        }
    }
    
    public void Refresh()
    {
        saveObj.ChangeScale(originScale);
    }

}
