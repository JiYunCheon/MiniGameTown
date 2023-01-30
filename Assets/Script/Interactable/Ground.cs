using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Ground : MonoBehaviour
{
    new private Renderer renderer = null;
    [HideInInspector] public bool buildingCheck = false;

    //이전 패드 저장 리스트
    private List<Ground> ground = new List<Ground>();

    //주변 패드 저장 리스트
    private List<Ground> nodes;

    public List<Ground> GetNodeList { get { return nodes; } set { } }


    private void Awake()
    {
        nodes = new List<Ground>();
        renderer = GetComponent<Renderer>();
    }

    public bool CompareNode(int occupyPad)
    {
        for (int i = 0; i < GetNodeList.Count; i++)
        {
            if (GetNodeList[i].buildingCheck) return false;
        }

        return true;
    }
    
    public void Clear(int occupyPad)
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
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                GetNodeList[i].renderer.material.color = Color.red;
                ground.Add(GetNodeList[i]);
            }
        }
        else if (occupyPad == GetNodeList.Count || color==Color.white )
        {
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                if (GetNodeList[i].buildingCheck)
                    continue;

                GetNodeList[i].renderer.material.color = color;
            }
        }
        else
            for (int i = 0; i < GetNodeList.Count; i++)
                GetNodeList[i].renderer.material.color = Color.red;
    }

    public void ChangeBuildingState(bool check, Color color)
    {
        this.buildingCheck=check;
        this.renderer.material.color = color;
    }


}

