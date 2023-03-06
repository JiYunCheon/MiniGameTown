using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MyFarmManager : MonoBehaviour
{
    public Canvas canvas;
    public Transform ScrollViewContent;

    public static MyFarmManager Inst;

    public GameObject curobj;

    public bool isOnScrollView;

    [SerializeField] private Transform itemBox = null;

    List<GameObject> setObjects = new List<GameObject>();
    List<GameObject> myItems = new List<GameObject>();
    private void Awake()
    {
        if (Inst == null) Inst = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        loadObjects();
    }

    public void OnClick_FarmShop()
    {
        saveObjects();
        SceneManager.LoadScene("MyFarmShop");
    }

    public void Onclick_BaseTown()
    {
        saveObjects();
        SceneManager.LoadScene("2.BaseTown");
    }

    public void startDragObject(GameObject obj)
    {
        curobj = obj;
        obj.transform.SetParent(itemBox);
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


    [ContextMenu("SAVE")]
    public void saveObjects()
    {
        string[] objname = new string[itemBox.childCount];
        string[] posX = new string[itemBox.childCount];
        string[] posY = new string[itemBox.childCount];
        int index = 0;
        //붙여진 스티커 저장
        foreach (Transform item in itemBox)
        {
            objname[index] = item.name.Replace(" ", string.Empty);
            posX[index] = item.GetComponent<RectTransform>().position.x.ToString().Trim();
            posY[index] = item.GetComponent<RectTransform>().position.y.ToString().Trim();
            index++;
        }

        index = 0;
        DatabaseAccess.Inst.loginUser.farmobjname = objname;
        DatabaseAccess.Inst.loginUser.farmobjposX = posX;
        DatabaseAccess.Inst.loginUser.farmobjposY = posY;

        objname = new string[ScrollViewContent.childCount];

        //인벤토리에있는 스티커 저장
        foreach (Transform item in ScrollViewContent)
        {
            objname[index] = item.name.Trim();
            index++;
        }
        
        DatabaseAccess.Inst.loginUser.farminvenobjname = objname;

        DatabaseAccess.Inst.SetUserData_Replace_FromDatabase(DatabaseAccess.Inst.loginUser.id);
    }

    public void loadObjects()
    {
        //인벤토리 아이템 생성
        if (DatabaseAccess.Inst.loginUser.farminvenobjname != null &&
            DatabaseAccess.Inst.loginUser.farminvenobjname.Length > 0 && 
            DatabaseAccess.Inst.loginUser.farminvenobjname[0] != "")
        {
            for (int i = 0; i < DatabaseAccess.Inst.loginUser.farminvenobjname.Length; i++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("MyFarmObj"), ScrollViewContent);

                Debug.Log(DatabaseAccess.Inst.loginUser.farminvenobjname[i]);

                obj.name = DatabaseAccess.Inst.loginUser.farminvenobjname[i];
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{DatabaseAccess.Inst.loginUser.farminvenobjname[i].Trim()}");
                myItems.Add(obj);
            }
        }

        //붙여진 스티커 생성
        if (DatabaseAccess.Inst.loginUser.farmobjname != null &&
            DatabaseAccess.Inst.loginUser.farmobjname.Length > 0 && 
            DatabaseAccess.Inst.loginUser.farmobjname[0] != "")
        {
            for (int i = 0; i < DatabaseAccess.Inst.loginUser.farmobjname.Length; i++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("MyFarmObj"), itemBox);
                Debug.Log(DatabaseAccess.Inst.loginUser.farmobjname[i]);

                obj.name = DatabaseAccess.Inst.loginUser.farmobjname[i];

                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{DatabaseAccess.Inst.loginUser.farmobjname[i].Trim()}");
                Vector3 pos = new Vector3(float.Parse(DatabaseAccess.Inst.loginUser.farmobjposX[i]),
                    float.Parse(DatabaseAccess.Inst.loginUser.farmobjposY[i]), 0);
                obj.GetComponent<RectTransform>().position = pos;
                setObjects.Add(obj);
            }
        }

        //상점에서 구매한 스티커 생성
        if (FarmData.Inst.myItemLis.Count>0)
        {
            List<Scriptable_Item> lis = FarmData.Inst.myItemLis;

            for (int i = 0; i < lis.Count; i++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("MyFarmObj"), ScrollViewContent);
                obj.name = lis[i].name;
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{lis[i].name}");
                myItems.Add(obj);
            }

            FarmData.Inst.myItemLis.Clear();
        }
      
    }
}
