using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas = null;

    private void SetCanvasRotation()
    {
        canvas.transform.LookAt(Camera.main.transform.position);

    }

}
