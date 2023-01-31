using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class PadSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private Ground pad;
    [Header("Area Info")]
    [SerializeField] private int hieght = 0;
    [SerializeField] private int width = 0;
    [SerializeField] float interveal = 0;

    
    //��ġ�� �е��� 2���� �迭
    private Ground[,] pads;

    private void Awake()
    {
        GeneratePad();
    }

    private void OnEnable()
    {
        SetPadNode(GameManager.Inst.GetClickManager.GetOccupyPad);
    }
    private void OnDisable()
    {
        PadNodeClear();
    }


    //�е� ���� �� �� �е庰 ������ �Ҵ�
    public void SetPadNode(int occupyPad)
    {
        if (occupyPad == 9)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (y + i >= 0 && x + j >= 0 && y + i <= hieght - 1 && x + j <= width - 1)
                                pads[y, x].GetNodeList.Add(pads[y + i, x + j]);
                        }
                    }

                }
            }
        }
        else if (occupyPad == 4)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = -1; j < 1; j++)
                        {
                            if (y + i >= 0 && x + j >= 0 && y - i <= hieght - 1 && x - j <= width - 1)
                            {
                                pads[y, x].GetNodeList.Add(pads[y + i, x + j]);
                            }
                        }
                    }

                }
            }
        }
        else if (occupyPad == 2)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    for (int j = -1; j < 1; j++)
                    {
                        if (x + j >= 0 && x - j <= width - 1)
                        {
                            pads[y, x].GetNodeList.Add(pads[y, x + j]);
                        }
                    }

                }
            }
        }
        else if (occupyPad == 1)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pads[y, x].GetNodeList.Add(pads[y, x]);
                }
            }
        }
    }

    private void PadNodeClear()
    {
        for (int i = 0; i < hieght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                pads[i, j].GetNodeList.Clear();
            }
        }
    }

    private void GeneratePad()
    {
        pads = new Ground[hieght, width];

        //2���� �迭�� ������ ���� ���̷� �е带 ��ġ��
        for (int i = 0; i < hieght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                pads[i, j] = Instantiate<Ground>(pad, this.transform);
                pads[i, j].transform.localPosition = new Vector3(j * interveal, transform.position.y, i * interveal);
                pads[i, j].name = $"{i},{j}";
                GameManager.Inst.grounds.Add(pads[i, j]);
            }
        }
    }


}
