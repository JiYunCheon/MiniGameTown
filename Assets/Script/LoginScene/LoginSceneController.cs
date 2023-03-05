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
    [SerializeField] private TMP_InputField inputfied__CopyPsw = null;
    [SerializeField] private TMP_InputField inputfied__CreateYear = null;
    [SerializeField] private TMP_InputField inputfied__NickName = null;
    [SerializeField] private TextMeshProUGUI id_StateText = null;
    [SerializeField] private TextMeshProUGUI psw_StateText = null;
    [SerializeField] private TextMeshProUGUI year_StateText = null;
    [SerializeField] private TextMeshProUGUI nickName_StateText = null;


    [Header("Find_Password")]
    [SerializeField] private GameObject find_ID_Board = null;
    [SerializeField] private TMP_InputField inputfied__FindID = null;
    [SerializeField] private TMP_InputField inputfied__FindYear = null;
    [SerializeField] private TextMeshProUGUI find_Psw_StateText = null;

    [Header("LoginFailed")]
    [SerializeField] private GameObject loginFailed = null;


    private bool idCompleteCheck = false;
    private bool createCheck = false;
    private bool idCheck = false;
    private bool pswCheck = false;
    private bool yearCheck = false;
    private bool nickNameCheck = false;


    private char[] textLength = null;
    [SerializeField] private int idLength = 0;
    [SerializeField] private int pswLength = 0;
    [SerializeField] private int nickNameLength = 0;
    string saveID = null;

    [HideInInspector]public Color green;
    [HideInInspector] public Color red;


    private void Awake()
    {
        green = new Color(27 / 255f, 85 / 255f, 24 / 255f);
        red = new Color(128 / 255f, 40 / 255f, 30 / 255f);

    }



    public void OnChangeValue_CreateID()
    {
        textLength = inputfied__CreateID.text.ToCharArray();
        Debug.Log(idCheck);
        if (textLength.Length > idLength)
        {
            idCheck = false;
            id_StateText.color = red;
            id_StateText.text = "아이디가 너무 길어요!";
        }
        else if (textLength.Length <= idLength && textLength.Length > 0)
        {
            if (idCompleteCheck && saveID == inputfied__CreateID.text)
            {
                idCheck = true;
            }
            else if (!idCompleteCheck && CompareInputValueCheck())
            {
                idCheck = false;
                id_StateText.color = red;
                id_StateText.text = "아이디 중복확인 해주세요";
            }
            else if (saveID != inputfied__CreateID.text)
            {
                idCheck = false;
                id_StateText.color = red;
                id_StateText.text = "아이디 중복확인 해주세요";
            }
        }
        else if(textLength.Length ==0)
        {
            idCheck = false;
            id_StateText.color = red;
            id_StateText.text = "아이디를 입력해 주세요";
        }

    }

    public void OnChangeValue_CreatePsw()
    {
        textLength = inputfied__CreatePsw.text.ToCharArray();

        if (textLength.Length > pswLength)
        {
            pswCheck = false;
            psw_StateText.color = red;
            psw_StateText.text = "비밀번호가 너무 길어요";
        }
        else if (inputfied__CreatePsw.text != inputfied__CopyPsw.text)
        {
            pswCheck = false;
            psw_StateText.color = red;
            psw_StateText.text = "비밀번호가 서로 달라요!";
        }
        else if (inputfied__CreatePsw.text == inputfied__CopyPsw.text && textLength.Length > 0)
        {
            pswCheck = true;
            psw_StateText.color = green;

            psw_StateText.text = "비밀번호가 같아요";
        }
        else if (textLength.Length == 0)
        {
            pswCheck = false;
            psw_StateText.color = red;
            psw_StateText.text = "비밀번호를 입력해 주세요";
        }

    }

    public void OnChangeValue_CreateYear()
    {
        textLength = inputfied__CreateYear.text.ToCharArray();

        if (textLength.Length != 6)
        {
            yearCheck = false;
            year_StateText.color = red;
            year_StateText.text = "생년월일 6자리를 입력해 주세요!";
        }
        if (textLength.Length == 6)
        {
            yearCheck = true;
            year_StateText.color = green;
            year_StateText.text = "생년월일 입력 완료";
        }
    }

    public void OnChangeValue_NickName()
    {
        textLength = inputfied__NickName.text.ToCharArray();
        nickName_StateText.color = Color.white;

        if (textLength.Length > nickNameLength)
        {
            nickNameCheck = false;
            nickName_StateText.color = red;

            nickName_StateText.text = "닉네임이 너무 길어요!";
        }
        else if (textLength.Length <= nickNameLength && textLength.Length > 0)
        {
            nickNameCheck = true;
            nickName_StateText.color = green;

            nickName_StateText.text = "닉네임 입력완료";
        }
        else
        {
            nickNameCheck = false;
            nickName_StateText.color = red;
            nickName_StateText.text = "닉네임을 입력해 주세요";
        }
    }




    public void Active_LoginFaied(bool activeSelf = true)
    {
        loginFailed.SetActive(activeSelf);
    }
   
    public void OnClick_LoginFailedExit()
    {
        Active_LoginFaied(false);
    }



    public void ChangeCreate_ID_State(bool check = true)
    {
        if (check)
        {
            saveID = inputfied__CreateID.text;

            id_StateText.color = green;
            id_StateText.text = "아이디 사용가능";
            idCompleteCheck = true;
        }
        else
        {
            saveID = inputfied__CreateID.text;

            id_StateText.color = red;
            id_StateText.text = "아이디 사용 불가";
            idCompleteCheck = false;
        }

    }

    public void ChangeFind_ID_State(Color color, string word = "찾은 비밀번호 없음")
    {
        find_Psw_StateText.text = word;
        find_Psw_StateText.color = color;
    }

    public void OnClick_LogIn_Confirm()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");
        DatabaseAccess.Inst.GetUserData(inputfied_ID.text, inputfied_Password.text);
    }

    

    public void Active_CreateIDBox(bool check = true)
    {
        createCheck = check;
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
        SoundManager.Inst.PlaySFX("SFX_AllTouch");
        Active_FindPswBox();
    }

    public void OnClick_Find()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        if (inputfied__FindID.text!="" && inputfied__FindYear.text!="")
         DatabaseAccess.Inst.ComparePsw_FromDatabase(inputfied__FindID.text, int.Parse(inputfied__FindYear.text));
        else
        {
            find_Psw_StateText.text = "빈 칸을 채워주세요";
        }
    }

    public void OnClick_FindExit()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        inputfied__FindID.text = "";
        inputfied__FindYear.text = "";
        find_Psw_StateText.text = "";

        Active_FindPswBox(false);
    }

    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////




    ////////////////////////////////////아이디 만들기창 버튼 이벤트 ///////////////////////////
    #region CreateID Btn Event

    //아이디 만들기창 켜기
    public void OnClick_CreateID()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        Active_CreateIDBox();
    }

    //아이디 중복 확인
    public void OnClick_IDConfirm()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        if(inputfied__CreateID.text=="")
        {
            idCheck = false;
            id_StateText.color = red;
            id_StateText.text = "아이디를 입력해 주세요";
            return;
        }

        DatabaseAccess.Inst.CompareID_FromDatabase(inputfied__CreateID.text);
    }

    //나가기 및 변수 초기화
    public void OnClick_CreateID_Exit()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        idCompleteCheck = false;

        inputfied__CreateID.text = "";
        inputfied__CreatePsw.text = "";
        inputfied__CopyPsw.text = "";
        inputfied__CreateYear.text = "";
        inputfied__NickName.text = "";
        id_StateText.text = "";
        psw_StateText.text = "";
        year_StateText.text = "";
        nickName_StateText.text = "";

        id_StateText.color = Color.white;
        psw_StateText.color = Color.white;
        year_StateText.color = Color.white;
        nickName_StateText.color = Color.white;

        Active_CreateIDBox(false);
    }

    //아이디 생성 로직
    public void OnClick_CreateID_Confirm()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        if (idCompleteCheck)
        {
            if(!CompareInputValueCheck())
            {
                id_StateText.text = "입력하신 정보를 다시한번 확인해 주세요";
                id_StateText.color = red;
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
       

        idCompleteCheck = false;
    }



    #endregion
    ///////////////////////////////////////////////////////////////////////////////////////

    private bool CompareInputValueCheck()
    {
        string empty = "";
        if (inputfied__CreateID.text != empty &&
            inputfied__CreatePsw.text != empty &&
            inputfied__CreateYear.text != empty &&
            inputfied__NickName.text != empty&&
            idCheck&&pswCheck&&yearCheck&&nickNameCheck)
            return true;

        return false;

    }

}
