using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFarmManager : MonoBehaviour
{
    public Canvas canvas;
    public Transform ScrollViewContent;

    public static MyFarmManager Inst;

    public GameObject curobj;

    public bool isOnScrollView;

    List<GameObject> setObjects = new List<GameObject>();
    List<GameObject> myItems = new List<GameObject>();
    private void Awake()
    {
        if (Inst == null) Inst = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        List<Scriptable_Item> lis = FarmData.Inst.myItemLis;


        for (int i = 0; i < lis.Count; i++)
        {
            GameObject temp = lis[i].createObjectFromItem();
            temp.transform.SetParent(ScrollViewContent);
            myItems.Add(temp);
        }

        //데이타 로드하기
    }

    public void startDragObject(GameObject obj)
    {
        curobj = obj;
        obj.transform.SetParent(canvas.transform);
        if (!setObjects.Contains(obj))
        {
            myItems.Remove(obj);
            setObjects.Add(obj);
        }
        else
        {
            setObjects.Remove(obj);
            myItems.Add(obj);
        }

    }

    public void DragObject()
    {

    }
    public void endDragObject(GameObject obj)
    {
        curobj = null;

        if (isOnScrollView)
        {
            if (setObjects.Contains(obj))
            {
                setObjects.Remove(obj);
                myItems.Add(obj);
            }
            else
            {
                myItems.Remove(obj);
                setObjects.Add(obj);
            }
            obj.transform.parent = ScrollViewContent;
        }
    }


   
}
