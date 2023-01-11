using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private Transform entrance = null;

    [Header("Material")]
    [SerializeField] private Texture texture = null;
    [SerializeField] private Material material = null;
    private Material outline = null;
    private Renderer renderer = null;

    private bool selectCheck = false;

    public Transform GetEntranceTr { get { return entrance; } private set { } }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        outline = new Material(Shader.Find("Draw/OutlineShader"));
    }

    //아웃라인 쉐이더 적용
    public void SetOutLineShader()
    {
        renderer.sharedMaterial = outline;
        renderer.sharedMaterial.mainTexture = texture;
    }

    //디폴트 쉐이더 적용
    public void SetDefaultShader()
    {
        renderer.sharedMaterial = material;
    }

    public void SetSelectCheck(bool check)
    {
        selectCheck = check;
    }

}
