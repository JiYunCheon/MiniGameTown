using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginSceneController : MonoBehaviour
{
    [Header("HomeUI")]
    [SerializeField] private TMP_InputField inputfied_ID = null;
    [SerializeField] private TMP_InputField inputfied_Password = null;

    [Header("CreateID_UI")]
    [SerializeField] private GameObject create_ID_Board = null;
    [SerializeField] private TMP_InputField inputfied__CreateID = null;
    [SerializeField] private TMP_InputField inputfied__CreatePsw = null;
    [SerializeField] private TMP_InputField inputfied__CreateYear = null;
    [SerializeField] private TMP_InputField inputfied__NickName = null;
    [SerializeField] private TextMeshProUGUI id_StateText = null;

    [Header("Find_Password")]
    [SerializeField] private GameObject find_ID_Board = null;
    [SerializeField] private TMP_InputField inputfied__FindID = null;
    [SerializeField] private TMP_InputField inputfied__FindYear = null;
    [SerializeField] private TextMeshProUGUI find_Psw_StateText = null;



    private bool idCompateCheck = false;

    public void ChangeCreate_ID_State(bool check = true)
    {
        if (check)
        {
            id_StateText.text = "아이디 사용가능";
            id_StateText.color = Color.white;
            idCompateCheck = true;
        }
        else
        {
            id_StateText.text = "아이디 사용불가";
            id_StateText.color = Color.red;
            idCompateCheck = false;
        }

    }

    public void ChangeFind_ID_State(Color color, string word = "찾은 비밀번호 없음")
    {
        find_Psw_StateText.text = word;
        find_Psw_StateText.color = color;
    }

    public void OnClick_LogIn_Confirm()
    {
        DatabaseAccess.Inst.GetUserData(inputfied_ID.text, inputfied_Password.text);
    }

    

    public void Active_CreateIDBox(bool check = true)
    {
        create_ID_Board.SetActive(check);
    }

    public void Active_FindPswBox(bool check = true)
    {
        find_ID_Board.SetActive(check);
    }

    ////////////////////////////////////아이디 만들기창 버튼 이벤트 ///////////////////////////
    #region CreateID Btn Event

    public void OnClick_FindBox()
    {
        Active_FindPswBox();
    }

    public void OnClick_Find()
    {
        if(inputfied__FindID.text!="" && inputfied__FindYear.text!="")
         DatabaseAccess.Inst.ComparePsw_FromDatabase(inputfied__FindID.text, int.Parse(inputfied__FindYear.text));
        else
        {
            find_Psw_StateText.text = "빈 칸을 채워주세요";
        }
    }

    public void OnClick_FindExit()
    {
        inputfied__FindID.text = "";
        inputfied__FindYear.text = "";

        Active_FindPswBox(false);
    }

    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////




    ////////////////////////////////////아이디 만들기창 버튼 이벤트 ///////////////////////////
    #region CreateID Btn Event

    //아이디 만들기창 켜기
    public void OnClick_CreateID()
    {
        Active_CreateIDBox();
    }

    //아이디 중복 확인
    public void OnClick_IDConfirm()
    {
        DatabaseAccess.Inst.CompareID_FromDatabase(inputfied__CreateID.text);
    }

    //나가기 및 변수 초기화
    public void OnClick_CreateID_Exit()
    {
        idCompateCheck = false;
        id_StateText.color = Color.white;
        inputfied__CreateID.text = "";
        inputfied__CreatePsw.text = "";
        inputfied__CreateYear.text = "";

        Active_CreateIDBox(false);
    }

    //아이디 생성 로직
    public void OnClick_CreateID_Confirm()
    {
        if(idCompateCheck)
        {
            if(!CompareInputValueCheck())
            {
                id_StateText.text = "빈 칸을 채워주세요";
                id_StateText.color = Color.red;
            }
            else
            {
                Debug.Log(int.Parse(inputfied__CreateYear.text));
                DatabaseAccess.Inst.SaveUserData
                    (DatabaseAccess.Inst.BeginUserDataInit(inputfied__CreateID.text, inputfied__CreatePsw.text, 
                    int.Parse(inputfied__CreateYear.text), inputfied__NickName.text));
                OnClick_CreateID_Exit();
            }

        }
        else
        {
            id_StateText.text = "아이디 중복체크를 해주세요";
            id_StateText.color = Color.red;
        }

        idCompateCheck = false;
    }

    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////

    private bool CompareInputValueCheck()
    {
        string empty = "";
        if (inputfied__CreateID.text != empty &&
            inputfied__CreatePsw.text != empty &&
            inputfied__CreateYear.text != empty &&
            inputfied__NickName.text != empty)
            return true;

        return false;

    }

}
