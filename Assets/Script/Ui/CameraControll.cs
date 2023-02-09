using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControll : MonoBehaviour
{
    private float cameraAxisY = 0;
    private float cameraAxisX = 0;

    //코루틴 종료 확인 체크
    private bool check = false;

    float time = 0;

    private Coroutine coroutine;
    private Coroutine camSizeCoroutine;

    private Vector3 modePos = new Vector3(14f,28f,56.2f);
    private Vector3 originPos = Vector3.zero;
    private Quaternion originRo = Quaternion.identity;
    private Vector3 moveRo = new Vector3(26.8f, 195, -9.6f);

    [SerializeField] private float xMin = 0;
    [SerializeField] private float xMax = 0;
    [SerializeField] private float yMin = 0;
    [SerializeField] private float yMax = 0;

    private void Awake()
    {
        StartCoroutine(nameof(CamMoveStart));
    }

    private void Update()
    {
        //업데이트에  들어가는 인풋들을 게임매니저에서 하는게 좋아보임 
        //한번에 관리 할 수 있도록

        if (GameManager.Inst.GetClickManager.selecCheck || GameManager.Inst.buildingMode || GameManager.Inst.GetUiManager.GetUiCheck
            || EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true) return;

        if (Input.GetMouseButtonDown(0))
        {
            check = false;
            if (coroutine != null)
                StopCoroutine(coroutine);
            time = 0;
        }

        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;

            cameraAxisY = Input.GetAxis("Mouse Y");
            cameraAxisX = Input.GetAxis("Mouse X");

            coroutine = StartCoroutine(nameof(CamMoveStart));

            return;
        }

        if (Input.GetMouseButtonUp(0) && time<=0.1f)
        {
            GameManager.Inst.GetPlayer.PlayerDestination();
        }

    }


    private IEnumerator CamMoveStart()
    {
        if (check == true) yield break;
        float count =2f;
        check = true;

        float xmin = -1;
        float ymin = -1;
            

        while (true)
        {
            count = Mathf.Lerp(count,0f, 0.5f * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
            transform.Translate(xmin * count * cameraAxisX * 0.5f*Time.fixedDeltaTime, ymin * count * cameraAxisY * Time.fixedDeltaTime, xmin * count * cameraAxisX * 0.5f * Time.fixedDeltaTime);

            Vector3 targetPos = transform.position;
            targetPos.x = Mathf.Clamp(targetPos.x, xMin, xMax);
            targetPos.y = Mathf.Clamp(targetPos.y, yMin, yMax);
            targetPos.z = 70f- Mathf.Clamp(targetPos.x, xMin, xMax);

            transform.position = targetPos;

            if (count<1f)
            {
                check = false;
                yield break;
            }

            
        }
    }

    public void ChangCameraSize()
    {
        transform.position = modePos;

        if (camSizeCoroutine != null)
            StopCoroutine(camSizeCoroutine);

        if (!GameManager.Inst.buildingMode && !GameManager.Inst.waitingMode)
        {
            camSizeCoroutine = StartCoroutine(LerpCameraSize(7f));
        }
        else if(GameManager.Inst.waitingMode)
        {
            camSizeCoroutine = StartCoroutine(LerpCameraSize(18f));
        }
        else if(GameManager.Inst.buildingMode)
        {
            camSizeCoroutine = StartCoroutine(LerpCameraSize(13f));
        }
    }

    IEnumerator LerpCameraSize(float value,bool zoomCheck =true)
    {
        while (true)
        {
            Camera.main.orthographicSize= Mathf.Lerp(Camera.main.orthographicSize, value, 1.8f *Time.deltaTime);
            
            if(zoomCheck && value ==15f && Camera.main.orthographicSize > (value - 0.1f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if(zoomCheck && value == 7f && Camera.main.orthographicSize < (value + 0.1f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if (!zoomCheck && value == 7f && Camera.main.orthographicSize > (value - 0.1f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if(zoomCheck && value == 4f && Camera.main.orthographicSize < (value + 0.1f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if (zoomCheck && value == 13f && Camera.main.orthographicSize < (value + 0.1f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }

            yield return null;
        }

    }




    private bool posProcessing = false;
    private bool roProcessing  = false;



    public void CameraPosMove(Building obj ,bool check = true)
    {
        StopAllCoroutines();

        if (check)
        {
            if(!posProcessing && !roProcessing)
            {
                originRo = transform.rotation;
                originPos = transform.position;
            }

            StartCoroutine(MoveCoroutine(GameManager.Inst.GetClickManager.GetCurHitObject.GetCameraPos.transform.position));
            StartCoroutine(RotantionCoroutine(GameManager.Inst.GetClickManager.GetCurHitObject.GetCameraPos.transform.rotation));

            camSizeCoroutine = StartCoroutine(LerpCameraSize(4f));

        }
        else
        {

            StartCoroutine(MoveCoroutine(originPos));
            StartCoroutine(RotantionCoroutine(originRo));

            camSizeCoroutine = StartCoroutine(LerpCameraSize(7f,false));
        }

    }

    private IEnumerator MoveCoroutine(Vector3 pos)
    {
        posProcessing = true;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 0.7f);

            yield return new WaitForFixedUpdate();
            if (Vector3.Distance(transform.position, pos) <= 0.2f)
            {
                transform.position = pos;
                posProcessing = false;
                yield break;
            }

        }
    }



    private IEnumerator RotantionCoroutine(Quaternion rotation)
    {
        roProcessing = true;
        float count = 0;
        while (true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

            yield return new WaitForFixedUpdate();
            count += Time.deltaTime;

            if (count >= 5f)
            {
                transform.rotation = rotation;
                roProcessing = false;
                yield break;
            }
        }

    }

  
}
