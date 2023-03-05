using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseResult : MonoBehaviour
{
    [SerializeField] private float waitSecond = 0;
    [SerializeField] GameObject successWindow = null;
    [SerializeField] GameObject resultWindow = null;

    private ContentItem curContent = null;



    private void OnEnable()
    {
        successWindow.SetActive(true);
        resultWindow.SetActive(false);
    }


    //���� ����Ʈ�� ������
    public virtual void SetMyContent(ContentItem content) => curContent = content;

    //���ż����� �����
    public void OnClick_PurchaseSuccess()
    {
        StartCoroutine(WaitForSecond(waitSecond));
    }


    IEnumerator WaitForSecond(float waitSecond)
    {
        SoundManager.Inst.PlaySFX("SFX_PurChase");
        successWindow.SetActive(false);
        resultWindow.SetActive(true);

        yield return new WaitForSeconds(waitSecond);

        //�������� �̵�
        GameManager.Inst.GetUiManager.On_Click_WaitingMode();

        //���ӸӴϸ� ����
        GameManager.Inst.TrySetGameMoney(-curContent.GetMyData.price);

        //���ӸӴ� �����̿� ��ǥ��
        GameManager.Inst.GetUiManager.InputGameMoney(DatabaseAccess.Inst.loginUser.gamemoney.ToString());

        //���� ������ �߰�
        curContent.GetItem.SetByCount(1);

        //maxī��Ʈ Ȯ��
        curContent.ComparerMaxCount();

        gameObject.SetActive(false);

    }



    //������� �� ����
    public void OnClick_Cancel()
    {
        SoundManager.Inst.PlaySFX("SFX_AllTouch");

        gameObject.SetActive(false);
    }

}
