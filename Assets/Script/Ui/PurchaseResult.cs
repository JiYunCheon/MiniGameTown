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
        GameManager.Inst.GetUiManager.On_Click_WatingMode();

        //���ӸӴϸ� ����
        GameManager.Inst.SetCount(GameManager.Inst.GetPlayerData.gameMoney_Key, -curContent.GetMyData.price);

        //���ӸӴ� �����̿� ��ǥ��
        GameManager.Inst.GetUiManager.InputGameMoney(GameManager.Inst.GetPlayerData.gameMoney.ToString());

        //���� ������ �߰�
        curContent.GetItem.SetByCount(1);

        //maxī��Ʈ Ȯ��
        curContent.ComparerMaxCount();

        //������ �ı�
        Destroy(this.gameObject);
    }

    //������� �� ����
    public void OnClick_Cancel()
    {
        Destroy(this.gameObject);
    }

}
