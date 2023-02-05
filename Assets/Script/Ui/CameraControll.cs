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

    private void Awake()
    {
        StartCoroutine(nameof(CamMoveStart));
    }

    private void Update()
    {

        if (GameManager.Inst.GetUiManager.GetSelecCheck || GameManager.Inst.buildingMode || GameManager.Inst.GetUiManager.GetUiCheck
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


    [SerializeField] private float xMin = 0;
    [SerializeField] private float xMax = 0;
    [SerializeField] private float yMin = 0;
    [SerializeField] private float yMax = 0;


    IEnumerator CamMoveStart()
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

    IEnumerator CamMoveStop()
    {
        float count = 2;

        while(true)
        {
            count = Mathf.Lerp(count, 0f, Time.deltaTime);
            yield return new WaitForEndOfFrame();
            transform.Translate(count * cameraAxisX * Time.deltaTime, count * cameraAxisY * Time.deltaTime, 0);

            if(count<0.5f)
            {
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

            yield return null;
        }

    }

    private Vector3 originPos = Vector3.zero;
    private Quaternion originRo = Quaternion.identity;
    private Vector3 moveRo = new Vector3(26.8f,195,-9.6f);


    public void CameraPosMove(Building obj ,bool check = true)
    {

        StopAllCoroutines();

        if (check)
        {
            originRo = transform.rotation;
            originPos = transform.position;

            //transform.position = pos + new Vector3(4,10f,20f);
            StartCoroutine(MoveCoroutine(GameManager.Inst.GetClickManager.GetBeforeHit));
            StartCoroutine(RotantionCoroutine(GameManager.Inst.GetClickManager.GetBeforeHit));

            camSizeCoroutine = StartCoroutine(LerpCameraSize(4f));

        }
        else
        {
            transform.rotation = originRo;

            StartCoroutine(MoveCoroutine(obj));
            StartCoroutine(RotantionCoroutine(GameManager.Inst.GetClickManager.GetBeforeHit));

            camSizeCoroutine = StartCoroutine(LerpCameraSize(7f,false));
        }

    }

    private IEnumerator MoveCoroutine(Building obj)
    {
        while (true)
        {
            transform.position=Vector3.MoveTowards(transform.position, obj.GetCameraPos.position, 0.7f);
            
            yield return new WaitForFixedUpdate();
            if (Vector3.Distance(transform.position, obj.GetCameraPos.position) <= 0.2f)
            {
                transform.position = obj.GetCameraPos.position;

                yield break;
            }
            
        }
    }

    private IEnumerator RotantionCoroutine(Building obj )
    {
        float count = 0;
        while (true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, obj.GetCameraPos.rotation, Time.deltaTime);

            yield return new WaitForFixedUpdate();
            count += Time.deltaTime;

            if (count>=20f)
            {
                Debug.Log("gd");
                transform.rotation = obj.GetCameraPos.rotation;

                yield break;
            }

        }
    }

  
}
