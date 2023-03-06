using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CommonUI : MonoBehaviour
{
    public GameObject UIPanel;

    bool SFXOn = true;
    bool BGMOn = true;

    [SerializeField] Sprite[] TogleImages;
    [SerializeField] Image Image_BGM;
    [SerializeField] Image Image_SFX;


    public static CommonUI Inst;
  
    public void Btn_Setting()
    {
        //�˾� ����
        UIPanel.SetActive(true);
        Time.timeScale = 0f;
        SoundManager.Inst.PlaySFX("SFX_ClickBtn");
    }

  

    public void Btn_Exit()
    {

        //Exit��ư �������� ������ ���⿡��
        Time.timeScale = 1f;
        UIPanel.SetActive(false);
        SoundManager.Inst.PlayBGM("SFX_ChangeScene");
        SceneManager.LoadScene("01. StartScene");
        SoundManager.Inst.PlaySFX("SFX_ClickBtn");
    }

    public void Btn_Help()
    {
        Debug.Log("��?����");
        SoundManager.Inst.PlaySFX("SFX_AllTouch");
    }

    public void Btn_BGM()
    {
        if (BGMOn)
        {
            SoundManager.Inst.setBGMVolume(0);
            BGMOn = false;
            Image_BGM.sprite = TogleImages[1];
        }
        else
        {
            SoundManager.Inst.setBGMVolume(1);
            BGMOn = true;
            Image_BGM.sprite = TogleImages[0];
        }
        SoundManager.Inst.PlaySFX("SFX_AllTouch");
    }

    public void Btn_SFX()
    {
        if (SFXOn)
        {
            SoundManager.Inst.setSFXVolume(0);
            SFXOn = false;
            Image_SFX.sprite = TogleImages[1];
        }
        else
        {
            SoundManager.Inst.setSFXVolume(1);
            SFXOn = true;
            Image_SFX.sprite = TogleImages[0];
        }
        SoundManager.Inst.PlaySFX("SFX_AllTouch");
    }

    public void Btn_Close()
    {
        UIPanel.SetActive(false); 
        Time.timeScale = 1f;
        SoundManager.Inst.PlaySFX("SFX_AllTouch");
    }


}
