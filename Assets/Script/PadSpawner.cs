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

    //�е� ���� �� �� �е庰 ������ �Ҵ�
    private void GeneratePad()
    {
        pads = new Ground[hieght, width];

        //2���� �迭�� ������ ���� ���̷� �е带 ��ġ��
        for (int i = 0; i < hieght; i++)
        {
            for (int j = 0; j < width; j++)
            {
                pads[i, j] = Instantiate<Ground>(pad,this.transform);
                pads[i, j].transform.localPosition = new Vector3(j* interveal, transform.position.y,i* interveal);
            }
        }

        //��ġ�� �е忡 �ֺ� 9ĭ�� �е��� ������ ���� 
        for (int y = 0; y < hieght; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //�ֺ� 9ĭ �˻�
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        //�ֺ��� �е尡 ���� ��� �ѱ�� ���ǹ�
                        if(y+i>=0 && x+j>=0 && y + i <= hieght-1 && x + j <= width - 1)
                            pads[y, x].GetNodeList.Add(pads[y + i, x + j]);
                    }
                }

            }
        }

    }


}
