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

    //설치될 패드의 2차원 배열
    private Ground[,] pads;

    private void Awake()
    {
        GeneratePad();
    }

    //패드 생성 후 각 패드별 데이터 할당
    private void GeneratePad()
    {
        pads = new Ground[hieght, width];

        //2차원 배열로 설정된 높이 넓이로 패드를 설치함
        for (int i = 0; i < hieght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                pads[i, j] = Instantiate<Ground>(pad,this.transform);
                pads[i, j].transform.localPosition = new Vector3(j* interveal, transform.position.y,i* interveal);
            }
        }

        //설치된 패드에 주변 9칸의 패드의 정보를 넣음 
        for (int y = 0; y < hieght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //주변 9칸 검사
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        //주변에 패드가 없을 경우 넘기는 조건문
                        if(y+i>=0 && x+j>=0 && y + i <= hieght-1 && x + j <= width - 1)
                            pads[y, x].GetNodeList.Add(pads[y + i, x + j]);
                    }
                }

            }
        }

    }


}
