using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    //레이어 마스크
    private int LayerMask = 1 << 6;

    //이전 건물을 저장 할 변수
    private InteractionObject beforeHit;

    //다른곳에서 값을 볼 수 있도록 프로퍼티
    public InteractionObject GetBeforeHit { get { return beforeHit; } private set { } } 

    void Update()
    {
        //건물이 선택되어 유아이가 켜져있을 경우
        if (GameManager.Inst.GetUiManager.GetSeleccCheck) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            InteractionObject obj = null;

            //건물만 클릭으로 확인
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask))
            {
                //상호 작용이 가능한 오브젝트인지 확인
                if (hit.transform.gameObject.TryGetComponent<InteractionObject>(out obj))
                {
                    //이전 오브젝트가 없는 경우
                    if(beforeHit== null)
                        Interection(obj, true);
                    //이전 오브젝트와 다른 오브젝트를 클릭했을 경우
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

    //클릭 될때 호출 기능 들
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
