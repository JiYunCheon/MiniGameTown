using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PadSpawner : MonoBehaviour
{
    [SerializeField] private int hieght = 0;
    [SerializeField] private int width = 0;
    [SerializeField] Ground pad;
    [SerializeField] float interveal = 0;

    Ground[,] pads;


    private void Awake()
    {
        GeneratePad();
    }


    private void GeneratePad()
    {
        Debug.Log("gd");
        pads = new Ground[hieght, width];

        for (int i = 0; i < hieght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                pads[i, j] = Instantiate<Ground>(pad,this.transform);
                pads[i, j].transform.localPosition = new Vector3(j* interveal, transform.position.y,i* interveal);

                //if(hieght==0)

            }
        }









    }


}
