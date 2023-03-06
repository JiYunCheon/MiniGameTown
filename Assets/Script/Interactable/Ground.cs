using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Ground : MonoBehaviour
{
    new private Renderer renderer = null;
    [HideInInspector] public bool buildingCheck = false;

    //���� �е� ���� ����Ʈ
    private List<Ground> ground = new List<Ground>();

    //�ֺ� �е� ���� ����Ʈ
    private List<Ground> nodes;

    public List<Ground> GetNodeList { get { return nodes; } set { } }

    private void Awake()
    {
        nodes = new List<Ground>();
        renderer = GetComponent<Renderer>();
    }

    //���� �е��� �ֺ� �е带 �˻�
    public bool CompareNode(int occupyPad)
    {
        //�����ؾ��ϴ� �е�� ���� ������ �ִ� �е��� �� ������ ���� �ʴٸ� FALSE ����
        if (occupyPad != GetNodeList.Count) return false;

        //���� �ֺ� �е� �� �Ǽ��� �Ǿ��ִ� ���� �ִٸ� FALSE ����
        for (int i = 0; i < GetNodeList.Count; i++)
        {
            if (GetNodeList[i].buildingCheck) return false;
        }

        //�� �ܴ� TRUE ����
        return true;
    }
    
    //���� �ٲ����� ���� ���� �е��� ���� �ʱ�ȭ �Ѵ�
    public void Clear()
    {
        if (ground.Count>0)
        {
            for (int i = 0; i < ground.Count; i++)
            {
                if (ground[i].buildingCheck) continue;
                ground[i].renderer.material.color = Color.white;
            }
            ground.Clear();
        }
       
    }

    public void SetColor(int occupyPad, Color color)
    {
        if(!CompareNode(occupyPad))
        {
            //�е��� ���� ���������� ����
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                GetNodeList[i].renderer.material.color = Color.red;

                //������ �ٲ��� �е带 ����
                ground.Add(GetNodeList[i]);
            }
        }
        //�����ؾ��ϴ� �е�� ���� �������ִ� �е��� ���ڰ� ���ų� ������ �Ͼ������ �ٲپ�� �Ҷ�
        else if (occupyPad == GetNodeList.Count || color==Color.white )
        {
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                //�ǹ��� �������ִٸ� �ѱ�
                if (GetNodeList[i].buildingCheck)
                    continue;

                //���� ����
                GetNodeList[i].renderer.material.color = color;
            }
        }
        else
            //�� �ܴ� �е��� ������ ���������� �����Ѵ�.
            for (int i = 0; i < GetNodeList.Count; i++)
                GetNodeList[i].renderer.material.color = Color.red;
    }

    //���� �е��� ���¸� �ٲ۴�
    public void ChangePadState(bool check, Color color)
    {
        this.buildingCheck=check;
        this.renderer.material.color = color;
    }


}

