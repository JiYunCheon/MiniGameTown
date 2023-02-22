using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 마우스가 현재 ScrollView공간 위에 있는지 확인하는 class
/// </summary>
public class ScrollViewChecker : MonoBehaviour
{
    public Transform[] limits;
    Rect rect;

    private void Awake()
    {
        rect = new Rect(limits[0].position.x, limits[0].position.y, limits[1].position.x - limits[0].position.x, limits[1].position.y - limits[0].position.y);
    }
    private void Update()
    {
        if (rect.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition))) MyFarmManager.Inst.isOnScrollView = true;
        else MyFarmManager.Inst.isOnScrollView = false;
    }

}
