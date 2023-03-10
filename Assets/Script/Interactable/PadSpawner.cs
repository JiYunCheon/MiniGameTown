using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField] private int beginX =0;
    [SerializeField] private int endX = 0;
    [SerializeField] private int beginY = 0;
    [SerializeField] private int endY = 0;

    [SerializeField] private int saveStartX = 0;
    [SerializeField] private int saveEndX = 0;
    [SerializeField] private int saveStartY = 0;
    [SerializeField] private int saveEndY = 0;

    [SerializeField] public bool deactiveCheck = false;

    public bool hideCheck = false;
    //설치될 패드의 2차원 배열
    private Ground[,] pads;
    [HideInInspector] public List<Ground> savePad = new List<Ground>();

    private void Awake()
    {
        GeneratePad();
    }

    private void OnEnable()
    {
        if(GameManager.Inst.GetClickManager.GetCurData!=null)
        {
            SetPadNode(GameManager.Inst.GetClickManager.GetCurData.occupyPad, GameManager.Inst.GetClickManager.GetCurData.hvCheck);
        }
    }
    private void OnDisable()
    {
        PadNodeClear();
    }


    //패드 생성 후 각 패드별 데이터 할당
    public void SetPadNode(int occupyPad , int hvCheck = 0)
    {
        if (occupyPad == 9 && hvCheck==0)
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
        else if (occupyPad == 4 && hvCheck == 0)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = -1; j < 1; j++)
                        {
                            if (y + i >= 0 && x + j >= 0 && y - i <= hieght - 1 && x - j <= width)
                            {
                                pads[y, x].GetNodeList.Add(pads[y + i, x + j]);
                            }
                        }
                    }

                }
            }
        }
        else if (occupyPad == 2 && hvCheck == 0)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    for (int j = -1; j < 1; j++)
                    {
                        if (x + j >= 0 && x - j <= width)
                        {
                            pads[y, x].GetNodeList.Add(pads[y, x + j]);
                        }
                    }

                }
            }
        }
        else if (occupyPad == 1 && hvCheck == 0)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pads[y, x].GetNodeList.Add(pads[y, x]);
                }
            }
        }
        else if (occupyPad == 3 && hvCheck == 0)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (x+j>=0&&x+j<=width-1)
                        {
                            pads[y, x].GetNodeList.Add(pads[y, x + j]);
                        }
                    }
                }
            }
        }
        else if (occupyPad == 3 && hvCheck >= 1)
        {
            for (int y = 0; y < hieght; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (y + j >= 0 && y + j <= hieght - 1)
                        {
                            pads[y, x].GetNodeList.Add(pads[y+j, x]);
                        }
                    }
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

        //2차원 배열로 설정된 높이 넓이로 패드를 설치함
        for (int i = 0; i < hieght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                pads[i, j] = Instantiate<Ground>(pad, this.transform);
                pads[i, j].transform.localPosition = new Vector3(j * interveal, transform.position.y, i * interveal);
                pads[i, j].name = $"{i},{j}";

                if (!hideCheck && deactiveCheck && j >= saveStartX && j <= saveEndX && i >= saveStartY && i <= saveEndY)
                {
                    savePad.Add(pads[i, j]);
                }

                GameManager.Inst.grounds.Add(pads[i, j]);
            }
        }

        if (deactiveCheck)
            DeActivePad();
    }

    private void DeActivePad()
    {
        for (int y = beginY; y < endY; y++)
        {
            for (int x = beginX; x < endX; x++)
            {
                pads[y, x].ChangePadState(true,Color.red);
                pads[y, x].gameObject.SetActive(false);
            }
        }


    }
}
