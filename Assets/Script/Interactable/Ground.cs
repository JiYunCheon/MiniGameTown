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

    //현재 패드의 주변 패드를 검사
    public bool CompareNode(int occupyPad)
    {
        //차지해야하는 패드와 현재 가지고 있는 패드의 총 개수가 같지 않다면 FALSE 리턴
        if (occupyPad != GetNodeList.Count) return false;

        //현재 주변 패드 중 건설이 되어있는 곳이 있다면 FALSE 리턴
        for (int i = 0; i < GetNodeList.Count; i++)
        {
            if (GetNodeList[i].buildingCheck) return false;
        }

        //그 외는 TRUE 리턴
        return true;
    }
    
    //색이 바꿔지지 않은 이전 패드의 색을 초기화 한다
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
            //패드의 색을 빨간색으로 변경
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                GetNodeList[i].renderer.material.color = Color.red;

                //색깔을 바꿔준 패드를 저장
                ground.Add(GetNodeList[i]);
            }
        }
        //차지해야하는 패드와 현재 가지고있는 패드의 숫자가 같거나 색깔을 하얀색으로 바꾸어야 할때
        else if (occupyPad == GetNodeList.Count || color==Color.white )
        {
            for (int i = 0; i < GetNodeList.Count; i++)
            {
                //건물이 지어져있다면 넘김
                if (GetNodeList[i].buildingCheck)
                    continue;

                //색을 변경
                GetNodeList[i].renderer.material.color = color;
            }
        }
        else
            //그 외는 패드의 색깔을 빨간색으로 변경한다.
            for (int i = 0; i < GetNodeList.Count; i++)
                GetNodeList[i].renderer.material.color = Color.red;
    }

    //현재 패드의 상태를 바꾼다
    public void ChangePadState(bool check, Color color)
    {
        this.buildingCheck=check;
        this.renderer.material.color = color;
    }


}

