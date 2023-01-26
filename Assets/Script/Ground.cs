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


    private void Awake()
    {
        nodes = new List<Ground>();
        renderer = GetComponent<Renderer>();
    }

    public void ChangeColor(Color color)
    {
        if (buildingCheck)
        {
            renderer.material.color = Color.red;
            return;
        }
        renderer.material.color = color;
    }

    public void OnBuilding(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GetNodeList[i].ChangeColor(Color.red);
            GetNodeList[i].buildingCheck = true;
        }
    }
 
    public bool CompareNode(int count)
    {
        if (count > GetNodeList.Count)
        {
            count = GetNodeList.Count;
        }

        for (int i = 0; i < count; i++)
        {
            if (GetNodeList[i].buildingCheck == true)
                return false;
        }
        return true;
    }

    public void ColorSet(Color color)
    {
        for (int i = 0; i < GetNodeList.Count; i++)
        {
            GetNodeList[i].ChangeColor(color);
        }
    }

    //주변 땅에 명령
    public void CommandAroundGround(int count)
    {
        if(count > GetNodeList.Count)
        {
            ColorSet(Color.red);
            return;
        }

        if (!CompareNode(count))
        {
            ColorSet(Color.red);
        }
        else
        {
            ColorSet(Color.green);
        }

    }
}
