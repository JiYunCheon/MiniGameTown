using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Ground : MonoBehaviour
{
    private List<Ground> nodes;
    public List<Ground> GetNodeList { get { return nodes; } set { } }

    Renderer renderer = null;
    [HideInInspector] public bool buildingCheck = false;

    List<Ground> ground = new List<Ground>();

    private void Awake()
    {
        nodes = new List<Ground>();
        renderer = GetComponent<Renderer>();
    }

    public void ChangeColor(Color color, int occupyPad)
    {
        if (!CompareNode(occupyPad) && color!=Color.white )
        {
            SetColor(occupyPad, color);
            return;
        }

        SetColor(occupyPad, color);
      
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

    private void SetColor(int occupyPad, Color color)
    {
        if(!CompareNode(occupyPad))
        {
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                GetNodeList[i].renderer.material.color = Color.red;
                ground.Add(GetNodeList[i]);
            }
            return;
        }
      

        if (occupyPad == GetNodeList.Count || color==Color.white )
        {
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                if (GetNodeList[i].buildingCheck)
                    continue;

                GetNodeList[i].renderer.material.color = color;

            }
        }
        else
        {
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                GetNodeList[i].renderer.material.color = Color.red;
            }
        }

    }

    public void OnBuilding(int occupyPad)
    {
        if (occupyPad == GetNodeList.Count)
        {
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                GetNodeList[i].buildingCheck=true;
                GetNodeList[i].renderer.material.color = Color.red;
            }
        }

    }




}

