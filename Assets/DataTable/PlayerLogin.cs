using UnityEngine;
using System.IO;
using System.Text;

public class PlayerLogin
{
    /// <summary>
    /// ������ ������ path�� �ҷ��ɴϴ�.
    /// </summary>
    /// <returns></returns>
    static string getFilePath()
    {
        string path = "/storage/emulated/0/PlayerData.csv";

        //string[] tok = path.Split("/");

        //StringBuilder sb = new StringBuilder();

        //for (int i = 0; i < tok.Length - 2; i++)
        //{
        //    sb.Append(tok[i]);
        //    sb.Append("/");
        //}
        //sb.Append("PlayerData.csv");

        //Debug.Log(sb.ToString());

        return path;
    }
    /// <summary>
    /// persistentDataPath�� PlayerData�κ��� Player�� Name�� �����ɴϴ�. �̹� �����Ǿ� �ִ� ������ �����ٸ�, null�� ��ȯ�մϴ�.
    /// </summary>
    /// <returns></returns>
    public static string getPlayerData()
    {
        string path = getFilePath();

        Debug.Log(path);
        TextReader tr;

        try
        {
            tr = new StreamReader(path);

            //ù��°�� colomn ���� �����̶� 2��°�Ÿ� ������
            string[] tok = tr.ReadLine().Split(",");

            tr.Close();

            return tok[1];
        }
        catch
        {
            Debug.Log("noData");
            return "testPlayer";
        }


    }
    /// <summary>
    /// PlayerData.csv ���Ͽ� �÷��̾� �̸��� �����մϴ�.
    /// </summary>
    /// <param name="name"></param>
    public static void setPlayedData(string name)
    {
        string path = getFilePath();

        TextWriter tw = new StreamWriter(path);

        tw.WriteLine("PlayerName" + "," + name);
        Debug.Log("Save Complete");

        tw.Close();
    }

    /// <summary>
    /// ���� ����Ǿ��ִ� PlayerData.csv�� �����մϴ�.
    /// </summary>
    public static void deletePlayerData()
    {
        string path = getFilePath();

        File.Delete(path);

    }
}