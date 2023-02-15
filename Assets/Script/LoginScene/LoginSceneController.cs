using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginSceneController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputfied_ID = null;
    [SerializeField] private TMP_InputField inputfied_Password = null;
    [SerializeField] private GameObject find_ID_Board = null;

   
    public void Onclick_Confirm()
    {
        DatabaseAccess.Inst.GetUserData(inputfied_ID.text, inputfied_Password.text);
    }

    public void Onclick_FindeId()
    {

    }

}
