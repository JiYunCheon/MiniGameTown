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
        if (count == 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (GetNodeList[i] == this)
                {
                    GetNodeList[i].ChangeColor(Color.red);
                    GetNodeList[i].buildingCheck = true;
                    return;
                }
            }
        }
        else if (count == 2)
        {
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (GetNodeList[i] == this)
                {
                    GetNodeList[i].ChangeColor(Color.red);
                    GetNodeList[i].buildingCheck = true;
                    index = i;
                    break;
                }
            }

            if (GetNodeList[index + 3] != null)
            {
                GetNodeList[index + 3].ChangeColor(Color.red);
                GetNodeList[index + 3].buildingCheck = true;
                return;
            }

        }
        else if (count == 4)
        {
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (GetNodeList[i] == this)
                {
                    GetNodeList[i].ChangeColor(Color.red);
                    GetNodeList[i].buildingCheck = true;
                    index = i;
                    break;
                }
            }

            if (GetNodeList[index + 4] != null)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (i == 2) continue;

                    GetNodeList[index + i].ChangeColor(Color.red);
                    GetNodeList[index + i].buildingCheck = true;
                }

                return;
            }
        }
        else if (count == 9)
        {
            for (int i = 0; i < count; i++)
            {
                GetNodeList[i].ChangeColor(Color.red);
                GetNodeList[i].buildingCheck = true;
            }
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

    public void ColorSet(Color color, int count)
    {
        if (count > GetNodeList.Count) count = GetNodeList.Count;

        //리스트 카운트가 인덱스 +a보다 클때

        if (count == 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (GetNodeList[i] == this)
                {
                    GetNodeList[i].ChangeColor(Color.green);
                    return;
                }
            }
        }
        else if (count == 2)
        {
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (GetNodeList[i] == this)
                {
                    GetNodeList[i].ChangeColor(Color.green);
                    index = i;
                    break;
                }
            }

            if (GetNodeList[index + 3] != null)
            {
                GetNodeList[index + 3].ChangeColor(Color.green);
                return;
            }

        }
        else if (count == 4)
        {
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (GetNodeList[i] == this)
                {
                    GetNodeList[i].ChangeColor(Color.green);
                    index = i;
                    break;
                }
            }

            if (GetNodeList[index + 4] != null)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (i == 2) continue;

                    GetNodeList[index + i].ChangeColor(Color.green);
                }

                return;
            }
        }
        else if (count == 9)
        {
            for (int i = 0; i < count; i++)
            {
                GetNodeList[i].ChangeColor(Color.green);
            }
        }
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
            ColorSet(Color.red, count);
            return;
        }

        if (!CompareNode(count))
        {
            ColorSet(Color.red, count);
        }
        else
        {
            ColorSet(Color.green, count);
        }

    }
}
