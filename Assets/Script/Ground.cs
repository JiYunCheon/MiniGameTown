using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Ground : MonoBehaviour
{

    [SerializeField] Ground[] aroundGround;
    [HideInInspector] public List<Ground> nodes;


    Renderer renderer = null;

    private void Awake()
    {
        nodes = new List<Ground>();
        renderer = GetComponent<Renderer>();
    }

    public void ChangeColor(Color color)
    {
        renderer.material.color = color;
    }

 



}
