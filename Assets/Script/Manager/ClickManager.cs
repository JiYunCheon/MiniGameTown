using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    private int LayerMask;
    private InteractionObject beforeHit;

    public InteractionObject GetBeforeHit { get { return beforeHit; }private set { } }
    private void Awake()
    {
        LayerMask = 1 << 6;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            InteractionObject obj = null;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask))
            {
                if (hit.transform.gameObject.TryGetComponent<InteractionObject>(out obj))
                {
                    if(beforeHit== null)
                    {
                        beforeHit = obj;
                        obj.SetOutLineShader();
                        obj.SetSelectCheck(true);
                    }
                    else if (beforeHit.transform.gameObject != hit.transform.transform.gameObject)
                    {
                        beforeHit.SetDefaultShader();
                        beforeHit.SetSelectCheck(false);

                        beforeHit = obj;

                        obj.SetOutLineShader();
                        obj.SetSelectCheck(true);
                    }
                    else if(beforeHit.transform.gameObject == hit.transform.transform.gameObject)
                    {
                        obj.SetDefaultShader();
                        obj.SetSelectCheck(false);
                        beforeHit = null;
                    }
                }

            }

        }
        
    }
}
