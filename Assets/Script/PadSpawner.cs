using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

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
            }
        }

        for (int y = 0; y < hieght; y++)
        {
            for (int x = 0; x < width; x++)
            {

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if(y+i>=0 && x+j>=0 && y + i <= hieght-1 && x + j <= width - 1)
                        {
                            pads[y, x].GetNodeList.Add(pads[y + i, x + j]);
                        }
                    }
                }

            }
        }



    }


}
