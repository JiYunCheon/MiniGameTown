using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControll : MonoBehaviour
{
    private float cameraAxisY = 0;
    private float cameraAxisX = 0;

    //�ڷ�ƾ ���� Ȯ�� üũ
    private bool moveProcessing = false;

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

    private bool posProcessing = false;
    private bool roProcessing = false;
    private Coroutine positionCorou;
    private Coroutine rotatiomCorou;

    private void Awake()
    {
        StartCoroutine(nameof(CamMoveStart));
    }


    private void Update()
    {
        //������Ʈ��  ���� ��ǲ���� ���ӸŴ������� �ϴ°� ���ƺ��� 
        //�ѹ��� ���� �� �� �ֵ���

        if (GameManager.Inst.GetClickManager.selectCheck || GameManager.Inst.buildingMode
            || EventSystem.current.IsPointerOverGameObject(GameManager.Inst.pointerID) == true) return;

        if (Input.GetMouseButtonDown(0))
        {
            moveProcessing = false;

            if(coroutine != null)
                StopCoroutine(coroutine);
            else if(positionCorou!=null || rotatiomCorou != null)
            {
                OriginPosMove();
            }

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

        if (Input.GetMouseButtonUp(0) && time<=0.15f)
        {
            GameManager.Inst.GetPlayer.PlayerDestination();
        }

    }


    private IEnumerator CamMoveStart()
    {
        if (moveProcessing == true) yield break;
        float count =2f;
        moveProcessing = true;

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
                moveProcessing = false;
                yield break;
            }

            
        }
    }

    //��庰�� ������ ����
    public void ChangeCameraSize()
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
            
            //������ �ϳ��� ����

            if(zoomCheck && value ==15f && Camera.main.orthographicSize > (value - 0.01f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if(zoomCheck && value == 7f && Camera.main.orthographicSize < (value + 0.01f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if (!zoomCheck && value == 7f && Camera.main.orthographicSize > (value - 0.01f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if(zoomCheck && value == 4f && Camera.main.orthographicSize < (value + 0.01f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if (zoomCheck && value == 13f && Camera.main.orthographicSize < (value + 0.01f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }

            yield return null;
        }

    }

    
    public void CameraPosMove(Building obj ,bool zoomCheck = true)
    {
        StopAllCoroutines();

        if (zoomCheck)
        {
            if(!posProcessing && !roProcessing)
            {
                originRo = transform.rotation;
                originPos = transform.position;
            }

            positionCorou = StartCoroutine(MoveCoroutine(obj.GetCameraPos.position));
            rotatiomCorou = StartCoroutine(RotantionCoroutine(obj.GetCameraPos.rotation));

            camSizeCoroutine = StartCoroutine(LerpCameraSize(4f));

        }
        else
        {

            positionCorou = StartCoroutine(MoveCoroutine(originPos));
            rotatiomCorou = StartCoroutine(RotantionCoroutine(originRo));

            camSizeCoroutine = StartCoroutine(LerpCameraSize(7f,false));
        }

    }

  

    //�ڷ�ƾ �ϳ��� ����

    private IEnumerator MoveCoroutine(Vector3 pos)
    {
        posProcessing = true;
        float time = 0;

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 0.7f);

            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;

            if (time >= 10f)
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
        float time = 0;
        while (true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

            yield return new WaitForFixedUpdate();
            time += Time.deltaTime;

            if (time >= 10f)
            {
                transform.rotation = rotation;
                roProcessing = false;
                yield break;
            }
        }

    }

    private void OriginPosMove()
    {
        StopAllCoroutines();

        if(originRo!=Quaternion.identity)
        {
            transform.rotation = originRo;
            rotatiomCorou = null;
        }
        
        if(originPos!=Vector3.zero)
        {
            transform.position = originPos;
            positionCorou = null;
        }
    }

  
}
