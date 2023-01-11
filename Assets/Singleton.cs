using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected Singleton() { }
    static T inst;

    public static T Inst
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType(typeof(T)) as T;

                if (inst == null)
                {
                    inst = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                }
            }
            return inst;
        }
    }
}
