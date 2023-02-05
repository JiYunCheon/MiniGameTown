using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Transform obj;

    [SerializeField] Quaternion pos = Quaternion.identity;

    float a = 30;
    Vector3 dir;
    Vector3 dd;


     private void Awake()
    {
        dd = obj.position + new Vector3(0, 5f, 0);
    }

    private void Update()
    {


        transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(-dd),Time.deltaTime);
    }


}
