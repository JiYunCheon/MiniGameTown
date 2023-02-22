using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    public CharacterRotater[] characters;
    public GameObject[] BtnGroups;

    public GameObject panel_Confirm;

    //�Ѱܾ��ϴ� ������
    public int charIdx = -1;

    public Image[] btnImage;

    Color disableColor = new Color(0, 0, 0, 0.5f);

    public Animator Txt_InfoAnim;

    public void Btn_SelectCharacter(int idx)
    {
        charIdx = idx;

        switch(charIdx)
        {
            case 0:
                btnImage[0].color = Color.clear;
                btnImage[1].color = disableColor;

                BtnGroups[0].SetActive(true);
                BtnGroups[1].SetActive(false);
                break;
            case 1:
                btnImage[0].color = disableColor;
                btnImage[1].color = Color.clear;

                BtnGroups[0].SetActive(false);
                BtnGroups[1].SetActive(true);
                break;
        }

    }
    public void Btn_RotationStart_Char1(int dir)
    {
        characters[0].startSpin(dir);
    }
    public void Btn_RotationStart_Char2(int dir)
    {
        characters[1].startSpin(dir);
    }


    public void Btn_RotationStop(int index)
    {
        characters[index].stopSpin();
    }

    public void Btn_Confirm()
    {
        if(charIdx == -1)
        {
            Txt_InfoAnim.SetTrigger("showDescription");
            return;
        }
        panel_Confirm.SetActive(true);
    }

    public void Btn_Confirm_Yes()
    {
        //���⼭ ���� ������ ���������� ���ư��ºκ�

        DatabaseAccess.Inst.loginUser.selectNum = charIdx;
        DatabaseAccess.Inst.SetUserData_Replace_FromDatabase(DatabaseAccess.Inst.loginUser.id);

        SceneManager.LoadScene("2.BaseTown");
    }
    public void Btn_Confirm_No()
    {
        panel_Confirm.SetActive(false);
    }
}
