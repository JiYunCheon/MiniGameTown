using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float cameraAxisY = 0;
    private float cameraAxisX = 0;

    //코루틴 종료 확인 체크
    private bool check = false;


    private void FixedUpdate()
    {

        if(Input.GetMouseButtonDown(1))
        {
            check = false;
            StopAllCoroutines();
        }
        if (Input.GetMouseButton(1))
        {

            cameraAxisY = Input.GetAxis("Mouse Y");
            cameraAxisX = Input.GetAxis("Mouse X");

            //transform.Translate(-1 * 20 * cameraAxisX * Time.deltaTime, -20 * cameraAxisY * Time.deltaTime, 0);

            //Vector3 targetPos = transform.position;

            //targetPos.x = Mathf.Clamp(targetPos.x, -4f, 7f);
            //targetPos.y = Mathf.Clamp(targetPos.y, 17f, 18f);
            //targetPos.z = 40f;

            //transform.position = Vector3.Lerp(Camera.main.transform.position,targetPos,0.2f);

            StartCoroutine(nameof(CamMoveStart));
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



}
