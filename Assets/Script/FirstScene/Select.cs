using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Select : MonoBehaviour
{
    protected Vector3 originScale;
    protected Vector3 biggerScale;

    private void Awake()
    {
        originScale = new Vector3(1.5f, 1.5f, 1);
        biggerScale = new Vector3(2, 2, 1);

    }
    public abstract void GameSelect();

    public void ChangeScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
    public void Refresh()
    {
        ChangeScale(originScale);
    }

}
