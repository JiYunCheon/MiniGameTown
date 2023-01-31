using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVUTILS : MonoBehaviour
{
    public static void saveData(List<DataFormat> saveLis, string fileName)
    {
        string filePath = Application.dataPath + "\\" + fileName;

        TextWriter tw = new StreamWriter(filePath, false);

        //Property 넣기
        tw.WriteLine("details");

        for (int i = 0; i < saveLis.Count; i++)
        {
            tw.WriteLine(saveLis[i].toCSVBool());
        }

        tw.Close();
    }

    public static List<DataFormat> loadData(string fileName)
    {
        //저장할 Path 설정
        string filePath = Application.dataPath + "\\" + fileName;

        //불러온 데이터가 저장될 List
        List<DataFormat> loadLis = new List<DataFormat>();
        TextReader tr;
        try
        {
            //파일을 읽어올 Stream
            tr = new StreamReader(filePath);
        }
        catch
        {
            return null;
        }


        string line = tr.ReadLine();
        string[] tok = line.Split(",");

        string[] properties = new string[tok.Length];
        tok.CopyTo(properties, 0);

        while (line != null)
        {
            line = tr.ReadLine();
            if (line == null) break;

            tok = line.Split(",");

            loadLis.Add(new


                (tok));
        }


        tr.Close();
        return loadLis;
    }
}