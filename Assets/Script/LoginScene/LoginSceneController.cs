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

    private int savePswLength = 0;

    private void Update()
    {
        if(createCheck)
        {
            textLength = inputfied__CreateID.text.ToCharArray();
            if(textLength.Length > idLength)
            {
                idCheck = false;
                id_StateText.text = "���̵� �ʹ� ����!";
            }
            else if(textLength.Length <= idLength && textLength.Length>0)
            {
                if (idCompleteCheck && saveID== inputfied__CreateID.text)
                {
                    idCheck = true;
                    id_StateText.color = Color.green;
                    id_StateText.text = "���̵� ��밡��";
                }
                else if(!idCompleteCheck && CompareInputValueCheck())
                {
                    idCheck = false;
                    id_StateText.color = Color.red;
                    id_StateText.text = "���̵� �ߺ�Ȯ�� ���ּ���";
                }
                else if (saveID != inputfied__CreateID.text)
                {
                    idCheck = false;
                    id_StateText.color = Color.red;
                    id_StateText.text = "���̵� �ߺ�Ȯ�� ���ּ���";
                }
               
            }

            textLength = inputfied__CreatePsw.text.ToCharArray();

            if(textLength.Length > pswLength)
            {
                pswCheck = false;
                psw_StateText.text = "��й�ȣ�� �ʹ� ����";
            }
            else if (inputfied__CreatePsw.text != inputfied__CopyPsw.text)
            {
                pswCheck = false;

                psw_StateText.text = "��й�ȣ�� ���� �޶��!";
            }
            else if(inputfied__CreatePsw.text == inputfied__CopyPsw.text && textLength.Length>0)
            {
                pswCheck = true;
                psw_StateText.text = "��й�ȣ�� ���ƿ�";
            }

            textLength = inputfied__CreateYear.text.ToCharArray();

            if(textLength.Length!=6)
            {
                yearCheck = false;
                year_StateText.text = "������� 6�ڸ��� �Է��� �ּ���!";
            }
            if (textLength.Length == 6)
            {
                yearCheck = true;

                year_StateText.text = "������� �Է� �Ϸ�";
            }

            textLength = inputfied__NickName.text.ToCharArray();

            if (textLength.Length > nickNameLength)
            {
                nickNameCheck = false;

                nickName_StateText.text = "�г����� �ʹ� ����!";
            }
            else if(textLength.Length <= nickNameLength && textLength.Length > 0)
            {
                nickNameCheck=true;
                nickName_StateText.text = "�г��� �Է¿Ϸ�";
            }
            else
            {
                nickNameCheck = false;

                nickName_StateText.text = "�г����� �Է��� �ּ���...";
            }
        }


    }


   




    public void ChangeCreate_ID_State(bool check = true)
    {
        if (check)
        {
            saveID = inputfied__CreateID.text;

            idCompleteCheck = true;
        }
        else
        {
            saveID = inputfied__CreateID.text;

            id_StateText.color = Color.red;
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
        Active_FindPswBox();
    }

    public void OnClick_Find()
    {
        if(inputfied__FindID.text!="" && inputfied__FindYear.text!="")
         DatabaseAccess.Inst.ComparePsw_FromDatabase(inputfied__FindID.text, int.Parse(inputfied__FindYear.text));
        else
        {
            find_Psw_StateText.text = "�� ĭ�� ä���ּ���";
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




    ////////////////////////////////////���̵� �����â ��ư �̺�Ʈ ///////////////////////////
    #region CreateID Btn Event

    //���̵� �����â �ѱ�
    public void OnClick_CreateID()
    {
        Active_CreateIDBox();
    }

    //���̵� �ߺ� Ȯ��
    public void OnClick_IDConfirm()
    {
        DatabaseAccess.Inst.CompareID_FromDatabase(inputfied__CreateID.text);
    }

    //������ �� ���� �ʱ�ȭ
    public void OnClick_CreateID_Exit()
    {
        idCompleteCheck = false;
        id_StateText.color = Color.white;
        inputfied__CreateID.text = "";
        inputfied__CreatePsw.text = "";
        inputfied__CreateYear.text = "";

        Active_CreateIDBox(false);
    }

    //���̵� ���� ����
    public void OnClick_CreateID_Confirm()
    {
        id_StateText.color = Color.white;

        if (idCompleteCheck)
        {
            if(!CompareInputValueCheck())
            {
                id_StateText.text = "�� ĭ�� ä���ּ���";
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
