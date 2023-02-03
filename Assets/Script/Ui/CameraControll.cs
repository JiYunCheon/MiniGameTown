using System.Collections;
using UnityEngine;

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
        if (GameManager.Inst.GetUiManager.GetSelecCheck || GameManager.Inst.buildingMode || GameManager.Inst.GetUiManager.GetUiCheck) return;

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

    IEnumerator LerpCameraSize(float value)
    {

        while (true)
        {
            Camera.main.orthographicSize= Mathf.Lerp(Camera.main.orthographicSize, value, 1.8f *Time.deltaTime);
            
            if(value==15f && Camera.main.orthographicSize > (value - 0.1f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if(value == 7f && Camera.main.orthographicSize < (value + 0.1f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }

            yield return null;
        }

    }

  
}
