using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    protected Object obj;

    public void GenericLoad<T>(string path) where T : Object => obj = Resources.Load<T>(path);

    protected T GetObj<T>() where T : Object => (T)obj;
  
    public abstract void Run();

}

