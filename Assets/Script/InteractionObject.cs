using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{

    [SerializeField] private Transform entrance = null;
    [SerializeField] private Texture texture = null;

    private Material outline = null;
    private Renderer renderer = null;

    public Transform GetEntranceTr { get { return entrance; } private set { } }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void ChangeShader(string materialName)
    {
        outline = new Material(Shader.Find(materialName));
        renderer.sharedMaterial = outline;
        renderer.sharedMaterial.mainTexture = texture;
        renderer.material.SetColor("_EmissionColor", Color.red*0.5f);
    }


}
