using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private float cameraAxisY = 0;
    private float cameraAxisX = 0;

    //코루틴 종료 확인 체크
    private bool check = false;

    float time = 0;

    Coroutine coroutine;

    private void Awake()
    {
        StartCoroutine(nameof(CamMoveStart));
    }

    private void Update()
    {
        if (GameManager.Inst.GetUiManager.GetSelecCheck || GameManager.Inst.buildingMode || GameManager.Inst.GetUiManager.GetUiCheck) return;

        //if (GameManager.Inst.buildingMode || GameManager.Inst.waitingMode) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            check = false;
            if(coroutine!=null)
            StopCoroutine(coroutine);
        }

        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (time > 0.5f)
            {
                cameraAxisY = Input.GetAxis("Mouse Y");
                cameraAxisX = Input.GetAxis("Mouse X");

                coroutine = StartCoroutine(nameof(CamMoveStart));
            }
            else if (time < 0.2f)
            {
                GameManager.Inst.GetPlayer.PlayerDestination();
            }

            time = 0;
        }


    }


    [SerializeField] private float xMin = 0;
    [SerializeField] private float xMax = 0;
    [SerializeField] private float yMin = 0;
    [SerializeField] private float yMax = 0;


    IEnumerator CamMoveStart()
    {
        if (check == true) yield break;
        float count =20;
        check = true;
        while(true)
        {
            count = Mathf.Lerp(count,0f,Time.deltaTime);
            yield return new WaitForEndOfFrame();

            transform.Translate(-1 * count * cameraAxisX * Time.deltaTime, -count * cameraAxisY * Time.deltaTime, 0);

            Vector3 targetPos = transform.position;

            targetPos.x = Mathf.Clamp(targetPos.x, xMin, xMax);
            targetPos.y = Mathf.Clamp(targetPos.y, yMin, yMax);
            targetPos.z = 40f;

            //transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 0.2f);
            transform.position = targetPos;

            if (count<5f)
            {
                //count = count/4;
                //while (true)
                //{
                //    count = Mathf.Lerp(count, 0f, Time.deltaTime);

                //    yield return new WaitForEndOfFrame();

                //    transform.Translate(count * cameraAxisX * Time.deltaTime, count * cameraAxisY * Time.deltaTime, 0);
                //    if(count < 0.5f)
                //    {
                //        break;
                //    }
                //}

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

    public void ChangCameraSize(bool mode)
    {
        if (mode == true)
        {
            StopAllCoroutines();

            StartCoroutine(LerpCameraSize(15f));
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(LerpCameraSize(7f));
        }
    }

    IEnumerator LerpCameraSize(float value)
    {

        while (true)
        {
            Camera.main.orthographicSize= Mathf.Lerp(Camera.main.orthographicSize, value, 1.8f*Time.deltaTime);
            
            if(value==15f && Camera.main.orthographicSize > (value - 0.2f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }
            else if(value == 7f && Camera.main.orthographicSize < (value + 0.2f))
            {
                Camera.main.orthographicSize = value;
                yield break;
            }

            yield return null;
        }

    }

  
}
