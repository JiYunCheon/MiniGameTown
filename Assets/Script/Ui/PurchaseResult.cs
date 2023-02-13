using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseResult : MonoBehaviour
{
    private ContentItem curContent = null;

    //���� ����Ʈ�� ������
    public virtual void SetMyContent(ContentItem content) => curContent = content;

    //���ż����� �����
    public void OnClick_PurchaseSuccess()
    {
        //�������� �̵�
        GameManager.Inst.GetUiManager.On_Click_WaitingMode();

        //���ӸӴϸ� ����
        GameManager.Inst.TrySetGameMoney(-curContent.GetMyData.price);

        //���ӸӴ� �����̿� ��ǥ��
        GameManager.Inst.GetUiManager.InputGameMoney(GameManager.Inst.GetPlayerData.gamemoney.ToString());

        //���� ������ �߰�
        curContent.GetItem.SetByCount(1);

        //maxī��Ʈ Ȯ��
        curContent.ComparerMaxCount();

        gameObject.SetActive(false);

    }

    //������� �� ����
    public void OnClick_Cancel()
    {
        gameObject.SetActive(false);
    }

}
