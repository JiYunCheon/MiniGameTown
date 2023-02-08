using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class Effect : MonoBehaviour
{
    protected UnityEngine.Object obj;

    public void GenericLoad<T>(string path) where T : UnityEngine.Object => obj = Resources.Load<T>(path);

    protected T GetObj<T>() where T : UnityEngine.Object => (T)obj;
  
    public abstract void Run();

}

