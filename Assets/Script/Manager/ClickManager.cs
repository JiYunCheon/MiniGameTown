using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    private int LayerMask;
    private InteractionObject beforeHit;

    private void Awake()
    {
        LayerMask = 1 << 6;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            InteractionObject obj;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask))
            {
                if (hit.transform.gameObject.TryGetComponent<InteractionObject>(out obj))
                {
                    if(beforeHit== null)
                    {
                        beforeHit = obj;
                        obj.ChangeShader("Draw/OutlineShader");
                    }
                    else if (beforeHit.transform.gameObject != hit.transform.transform.gameObject)
                    {
                        beforeHit.ChangeShader("Draw/Default");
                        beforeHit = obj;
                        obj.ChangeShader("Draw/OutlineShader");
                    }
                    else if(beforeHit.transform.gameObject == hit.transform.transform.gameObject)
                    {
                        obj.ChangeShader("Draw/Default");
                        beforeHit = null;
                    }
                    

                }
            }
        }
        
    }
}
