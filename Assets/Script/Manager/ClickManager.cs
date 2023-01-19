using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    //레이어 마스크
    private int LayerMask = 1 << 6;
    private int LayerMask2 = 1 << 7;

    //이전 건물을 저장 할 변수
    private InteractionObject beforeHit;
    private Ground beforeGround;
    Vector3 saveHitPos = Vector3.zero;

    //다른곳에서 값을 볼 수 있도록 프로퍼티
    public InteractionObject GetBeforeHit { get { return beforeHit; } private set { } }

    [SerializeField] GameObject alphaPrefab = null;
    [SerializeField] GameObject prefab = null;
    GameObject preview;
    Ground ground = null;
    

    [Header("Material")]
    [SerializeField] private Texture texture = null;
    [SerializeField] private Shader alphaShader = null;


    void Update()
    {
        //건물이 선택되어 유아이가 켜져있을 경우
        if (GameManager.Inst.GetUiManager.GetSeleccCheck) return;

        if (Input.GetMouseButtonDown(0)&&!GameManager.Inst.GetBuildingMode)
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

        if(GameManager.Inst.GetBuildingMode)
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100,LayerMask2))
            {
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * 10, Color.red, 0.3f);

                if (hit.transform.gameObject.TryGetComponent<Ground>(out ground))
                {
                    if (preview == null && saveHitPos==Vector3.zero)
                    {
                        beforeGround = ground;
                        beforeGround.ChangeColor(Color.blue);

                        saveHitPos = ground.transform.position;
                        preview = Instantiate(alphaPrefab, saveHitPos, Quaternion.identity);
                    }
                    else if(ground.transform.position!=saveHitPos)
                    {
                        beforeGround.ChangeColor(Color.white);
                        beforeGround = ground;
                        beforeGround.ChangeColor(Color.blue);
                        saveHitPos = ground.transform.position;
                        preview.transform.position = saveHitPos;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(prefab, saveHitPos, Quaternion.identity);
                    Destroy(preview);
                    preview = null;
                    saveHitPos = Vector3.zero;
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
