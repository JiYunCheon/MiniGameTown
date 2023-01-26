using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
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

    Transform alphaPrefab = null;
    InteractionObject prefab = null;
    Transform preview;
    Ground ground = null;
    private int occupyPad = 0;



    [Header("Material")]
    [SerializeField] private Texture texture = null;
    [SerializeField] private Shader alphaShader = null;

    void Update()
    {
        //�ǹ��� ���õǾ� �����̰� �������� ���
        if (GameManager.Inst.GetUiManager.GetSelecCheck || EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true) return;

        if (Input.GetMouseButtonDown(0)&&!GameManager.Inst.buildingMode)
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
                        GameManager.Inst.GetUiManager.ChangeSelecChcek(false);
                        Interection(obj, true);
                    }
                }
            }

        }

        if(GameManager.Inst.buildingMode)
        {
            RaycastHit hit;
            //pad�� Ŭ��
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100,LayerMask2))
            {
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * 10, Color.red, 0.3f);

                if (hit.transform.gameObject.TryGetComponent<Ground>(out ground))
                {
                    //ù��° Ŭ���� ���
                    if (preview == null && saveHitPos==Vector3.zero)
                    {
                        //���õ� �е带 ����
                        beforeGround = ground;
                        //�ֺ� �� ����
                        beforeGround.CommandAroundGround(occupyPad);
                        //���õ� ���� �������� ����
                        saveHitPos = ground.transform.position;
                        //�� �����ǿ� ���� �ǹ� ����
                        preview = Instantiate<Transform>(alphaPrefab, saveHitPos, Quaternion.identity);
                    }
                    //������ ���� �������� ����Ǿ��� ���
                    else if (ground.transform.position!=saveHitPos)
                    {
                        //������ ���õǾ��� �е��� ���� ����
                        beforeGround.ColorSet(Color.white);
                        //���� �е带 ���� ���õ� �е�� �ʱ�ȭ
                        beforeGround = ground;
                        //���� ����
                        beforeGround.CommandAroundGround(occupyPad);
                        //���� �������� ���� ���õ� ���������� �ʱ�ȭ
                        saveHitPos = ground.transform.position;
                        //�ʱ�ȭ�� ���������� ���İǹ��� ��ġ�� ����
                        preview.transform.position = saveHitPos;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (beforeGround.GetNodeList.Count == occupyPad)
                    {

                        if(beforeGround.CompareNode(occupyPad))
                        {
                            beforeGround.OnBuilding(occupyPad);
                            //��¥ �ǹ� ����
                            Instantiate<InteractionObject>(prefab, saveHitPos+new Vector3(0,0,0.5f), Quaternion.identity);
                            //���İǹ� ����
                            Destroy(preview.gameObject);
                            //�� �ʱ�ȭ
                            beforeGround = null;
                            preview = null;
                            saveHitPos = Vector3.zero;

                            GameManager.Inst.GetUiManager.On_Click_WatingMode();
                        }
                       
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
        GameManager.Inst.GetUiManager.ChangeSelecChcek(check);
    }

    
    public void Refresh()
    {
        beforeHit.SetDefaultShader();
        beforeHit.SetSelectCheck(false);
        GameManager.Inst.GetUiManager.ChangeSelecChcek(false);
        beforeHit = null;
    }

    public void SetPrefab(Transform alphaPrefab ,InteractionObject obj , int occupyPad)
    {
        prefab = obj;
        this.occupyPad = occupyPad;
        this.alphaPrefab = alphaPrefab;
    }


}
