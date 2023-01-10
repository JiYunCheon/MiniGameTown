using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private Shader shader;
    private Material outline;

    private int LayerMask;
    Renderer renderer;
    List<Material>materials=new List<Material>();
    [SerializeField] private Texture texture;

    private void Awake()
    {
        outline = new Material(Shader.Find("Draw/OutlineShader"));
        LayerMask = 1 << 6;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask))
            {
                Debug.DrawRay(Camera.main.transform.position, hit.point, Color.red,100);
                Debug.Log(hit.transform.name);
                
                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
                renderer = hit.transform.gameObject.GetComponent<Renderer>();
                renderer.sharedMaterial = outline;
                renderer.sharedMaterial.mainTexture = texture;
            }
        }
        
    }
}
