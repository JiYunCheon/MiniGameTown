using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserProfile : MonoBehaviour
{
    [SerializeField] private Sprite[] userProfile = null;
    [SerializeField] private Image profile = null;
    [SerializeField] private TextMeshProUGUI nickNameText = null;

    private void Start()
    {
        SetProfile(DatabaseAccess.Inst.loginUser.selectNum);
        UserInfo(DatabaseAccess.Inst.loginUser.nickname);
    }

    public void SetProfile(int index)
    {
        profile.sprite = userProfile[index];
    }

    public void UserInfo(string nickName)
    {
        this.nickNameText.text = nickName;
    }
}
