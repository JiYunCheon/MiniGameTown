using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Select : MonoBehaviour
{
    [SerializeField] protected Vector3 biggerScale;
    protected Vector3 originScale;

    private void Awake()
    {
        originScale = transform.localScale;
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
