using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


//������ data class ���� �����
public class DataFormat
{
    public string objName;
    public Vector3 pos;

    public int checkValues;

    /// <summary>
    /// csv���� �о�� �����͸� DataFormat���� �ٲٴ� �Լ�
    /// </summary>
    /// <param name="datas"></param>
    public DataFormat(string[] datas)
    {
        objName = datas[0];
        pos = Vector3.right * float.Parse(datas[1]) + Vector3.up * float.Parse(datas[2]) + Vector3.forward * float.Parse(datas[3]);
    }

    public DataFormat(GameObject obj)
    {
        objName = obj.name;

        pos = obj.GetComponent<RectTransform>().position;
    }
    public DataFormat()
    {

    }
        


    /// <summary>
    /// DataFormat�� csv���Ͽ� �����Ҽ� �ִ� ���·� �ٲٴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public string toCSVString()
    {
        return objName + "," + pos.x.ToString() + "," + pos.y.ToString() + "," + pos.z.ToString();
    }

    //public string toCSVBoolArray()
    //{
    //    string answer = "";

    //    for (int i = 0; i < checkValues.Length; i++)
    //    {
    //        if (i == 0)
    //        {
    //            answer = checkValues[i].ToString();
    //        }
    //        else
    //            answer = answer + "," + checkValues[i].ToString();
    //    }

    //    return answer;
    //}

    public string toCSVBool()
    {
        return checkValues.ToString();
    }






    /// <summary>
    /// DataFormat�� ������� Gameobject�� �����ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    ///
    //public GameObject createObjFromData()
    //{
    //    Scriptable_Item data = Resources.Load<Scriptable_Item>("ScriptableObject/" + objName);
    //    GameObject temp = data.createObjectFromItem();
    //    temp.GetComponent<RectTransform>().position = pos;
    //    temp.name = objName;

    //    return temp;

    //}

}