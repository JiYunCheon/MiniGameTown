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
            id_StateText.text = "���̵� �ʹ� ����!";
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
                id_StateText.text = "���̵� �ߺ�Ȯ�� ���ּ���";
            }
            else if (saveID != inputfied__CreateID.text)
            {
                idCheck = false;
                id_StateText.color = red;
                id_StateText.text = "���̵� �ߺ�Ȯ�� ���ּ���";
            }
        }
        else if(textLength.Length ==0)
        {
            idCheck = false;
            id_StateText.color = red;
            id_StateText.text = "���̵� �Է��� �ּ���";
        }

    }

    public void OnChangeValue_CreatePsw()
    {
        textLength = inputfied__CreatePsw.text.ToCharArray();

        if (textLength.Length > pswLength)
        {
            pswCheck = false;
            psw_StateText.color = red;
            psw_StateText.text = "��й�ȣ�� �ʹ� ����";
        }
        else if (inputfied__CreatePsw.text != inputfied__CopyPsw.text)
        {
            pswCheck = false;
            psw_StateText.color = red;
            psw_StateText.text = "��й�ȣ�� ���� �޶��!";
        }
        else if (inputfied__CreatePsw.text == inputfied__CopyPsw.text && textLength.Length > 0)
        {
            pswCheck = true;
            psw_StateText.color = green;

            psw_StateText.text = "��й�ȣ�� ���ƿ�";
        }
        else if (textLength.Length == 0)
        {
            pswCheck = false;
            psw_StateText.color = red;
            psw_StateText.text = "��й�ȣ�� �Է��� �ּ���";
        }

    }

    public void OnChangeValue_CreateYear()
    {
        textLength = inputfied__CreateYear.text.ToCharArray();

        if (textLength.Length != 6)
        {
            yearCheck = false;
            year_StateText.color = red;
            year_StateText.text = "������� 6�ڸ��� �Է��� �ּ���!";
        }
        if (textLength.Length == 6)
        {
            yearCheck = true;
            year_StateText.color = green;
            year_StateText.text = "������� �Է� �Ϸ�";
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

            nickName_StateText.text = "�г����� �ʹ� ����!";
        }
        else if (textLength.Length <= nickNameLength && textLength.Length > 0)
        {
            nickNameCheck = true;
            nickName_StateText.color = green;

            nickName_StateText.text = "�г��� �Է¿Ϸ�";
        }
        else
        {
            nickNameCheck = false;
            nickName_StateText.color = red;
            nickName_StateText.text = "�г����� �Է��� �ּ���";
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

            idCheck = true;
            id_StateText.color = green;
            id_StateText.text = "���̵� ��밡��";
            idCompleteCheck = true;
        }
        else
        {
            saveID = inputfied__CreateID.text;
            idCheck = false;

            id_StateText.color = red;
            id_StateText.text = "���̵� ��� �Ұ�";
            idCompleteCheck = false;
        }

    }

    public void ChangeFind_ID_State(Color color, string word = "ã�� ��й�ȣ ����")
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

    ////////////////////////////////////���̵� �����â ��ư �̺�Ʈ ///////////////////////////
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
            find_Psw_StateText.text = "�� ĭ�� ä���ּ���";
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




    ////////////////////////////////////���̵� �����â ��ư �̺�Ʈ ///////////////////////////
    #region CreateID Btn Event

    //���̵� �����â �ѱ�
    public void OnClick_CreateID()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        Active_CreateIDBox();
    }

    //���̵� �ߺ� Ȯ��
    public void OnClick_IDConfirm()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        if(inputfied__CreateID.text=="")
        {
            idCheck = false;
            id_StateText.color = red;
            id_StateText.text = "���̵� �Է��� �ּ���";
            return;
        }

        DatabaseAccess.Inst.CompareID_FromDatabase(inputfied__CreateID.text);
    }

    //������ �� ���� �ʱ�ȭ
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

    //���̵� ���� ����
    public void OnClick_CreateID_Confirm()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        if (idCompleteCheck)
        {
            if(!CompareInputValueCheck())
            {
                id_StateText.text = "�Է��Ͻ� ������ �ٽ��ѹ� Ȯ���� �ּ���";
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
