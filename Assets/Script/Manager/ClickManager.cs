using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    //���̾� ����ũ
    private int LayerMask = 1 << 6;

    //���� �ǹ��� ���� �� ����
    private InteractionObject beforeHit;

    //�ٸ������� ���� �� �� �ֵ��� ������Ƽ
    public InteractionObject GetBeforeHit { get { return beforeHit; } private set { } } 

    void Update()
    {
        //�ǹ��� ���õǾ� �����̰� �������� ���
        if (GameManager.Inst.GetUiManager.GetSeleccCheck) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            InteractionObject obj = null;

            //�ǹ��� Ŭ������ Ȯ��
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask))
            {
                //��ȣ �ۿ��� ������ ������Ʈ���� Ȯ��
                if (hit.transform.gameObject.TryGetComponent<InteractionObject>(out obj))
                {
                    //���� ������Ʈ�� ���� ���
                    if(beforeHit== null)
                        Interection(obj, true);
                    //���� ������Ʈ�� �ٸ� ������Ʈ�� Ŭ������ ���
                    else if (beforeHit.transform.gameObject != hit.transform.transform.gameObject)
                    {
                        beforeHit.SetDefaultShader();
                        GameManager.Inst.GetUiManager.ChangeCheckValue(false);
                        Interection(obj, true);
                    }

                }
            }

        }
    }

    //Ŭ�� �ɶ� ȣ�� ��� ��
    private void Interection(InteractionObject obj , bool check)
    {
        beforeHit = obj;
        beforeHit.SetOutLineShader();
        beforeHit.SetSelectCheck(check);
        GameManager.Inst.curGameName = obj.GetPackageName;
        GameManager.Inst.GetUiManager.ChangeCheckValue(check);
    }

    
    public void Refresh()
    {
        beforeHit.SetDefaultShader();
        beforeHit.SetSelectCheck(false);
        GameManager.Inst.GetUiManager.ChangeCheckValue(false);
        beforeHit = null;
    }



}
