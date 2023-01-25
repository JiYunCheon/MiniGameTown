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

        if (Input.GetMouseButtonDown(0)&&!GameManager.Inst.buildingMode)
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

        Debug.Log(GameManager.Inst.buildingMode);
        if(GameManager.Inst.buildingMode)
        {
            RaycastHit hit;
            //pad만 클릭
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100,LayerMask2))
            {
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * 10, Color.red, 0.3f);

                if (hit.transform.gameObject.TryGetComponent<Ground>(out ground))
                {
                    //첫번째 클릭일 경우
                    if (preview == null && saveHitPos==Vector3.zero)
                    {
                        //선택된 패드를 저장
                        beforeGround = ground;
                        //주변 색 변경
                        beforeGround.CommandAroundGround(9);
                        //선택된 곳의 포지션을 저장
                        saveHitPos = ground.transform.position;
                        //그 포지션에 알파 건물 생성
                        preview = Instantiate(alphaPrefab, saveHitPos, Quaternion.identity);
                    }
                    //레이의 힛의 포지션이 변경되었을 경우
                    else if (ground.transform.position!=saveHitPos)
                    {
                        Debug.Log("들어와");

                        Debug.Log(beforeGround.GetNodeList.Count);
                        //기존에 선택되었던 패드의 색을 변경
                        beforeGround.ColorSet(Color.white);
                        //이전 패드를 지금 선택된 패드로 초기화
                        beforeGround = ground;
                        //색을 변경
                        beforeGround.CommandAroundGround(9);
                        //이전 포지션을 지금 선택된 포지션으로 초기화
                        saveHitPos = ground.transform.position;
                        //초기화된 포지션으로 알파건물의 위치를 변경
                        preview.transform.position = saveHitPos;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (beforeGround.GetNodeList.Count == 9)
                    {

                        if(beforeGround.CompareNode(9))
                        {
                            beforeGround.OnBuilding(9);
                            //진짜 건물 생성
                            Instantiate(prefab, saveHitPos, Quaternion.identity);
                            //알파건물 제거
                            Destroy(preview);
                            //값 초기화
                            beforeGround = null;
                            preview = null;
                            saveHitPos = Vector3.zero;
                            //빌딩모드 끔
                            GameManager.Inst.ChangeMode(out GameManager.Inst.buildingMode, false);
                        }
                       
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
