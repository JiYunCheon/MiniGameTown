using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    //���̾� ����ũ
    private int LayerMask = 1 << 6;
    private int LayerMask2 = 1 << 7;

    //���� �ǹ��� ���� �� ����
    private InteractionObject beforeHit;
    private Ground beforeGround;
    Vector3 saveHitPos = Vector3.zero;

    //�ٸ������� ���� �� �� �ֵ��� ������Ƽ
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
        //�ǹ��� ���õǾ� �����̰� �������� ���
        if (GameManager.Inst.GetUiManager.GetSeleccCheck) return;

        if (Input.GetMouseButtonDown(0)&&!GameManager.Inst.GetBuildingMode)
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
