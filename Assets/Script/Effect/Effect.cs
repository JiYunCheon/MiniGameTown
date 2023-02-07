using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class Effect : MonoBehaviour
{
    public Action spriteAction;
    private string path = null;

    protected Sprite sprite;


    private void Awake()
    {
        spriteAction = SetSprite;
    }

    private void SetSprite()
    {
        sprite = Resources.Load<Sprite>(path);
    }


    public abstract void Run();

    public void SetPath(string path, Action action)
    {
        this.path = path;
        action();
    }

}
