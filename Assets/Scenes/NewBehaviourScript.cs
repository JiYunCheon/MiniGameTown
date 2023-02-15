using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    string a = "-1.55";
    public AnimationCurve ac;
    public float t = 0;

     private void Awake()
    {
        Debug.Log(float.Parse(a)+1);
    }

    private void Update()
    {
        t+=Time.deltaTime;
        if (t >= 1)
            t = 0;

        float y = ac.Evaluate(t);
        transform.position = new Vector3(-31.8f, 5 + y, 35.37f);

        //Interactable prefab = Resources.Load<Interactable>($"Prefabs/{type}/{objectsName[i]}");
        
    }


}
