using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Select : MonoBehaviour
{
    public abstract void GameSelect();

    public void ChangeScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

}
