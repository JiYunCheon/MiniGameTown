using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObject : MonoBehaviour
{

    [SerializeField] float flyInterval = 0;
    [SerializeField] float flyTime = 0;
    [SerializeField] float stopTime = 0;

    [SerializeField] float rotationInterval = 0;
    [SerializeField] float rotationSpeed = 0;
    private void Start()
    {
        StartCoroutine(StartFly());
        StartCoroutine(StartRotation());
    }


    IEnumerator StartFly()
    {
        float time = 0;

        while(true)
        {
            while (true)
            {
                if (time > flyTime)
                    break;

                time += Time.deltaTime;

                transform.Translate(Vector3.up* flyInterval);

                yield return new WaitForFixedUpdate();

            }

            yield return new WaitForSeconds(stopTime);

            time = 0;

            while (true)
            {
                if (time > flyTime)
                    break;

                time += Time.deltaTime;

                transform.Translate(Vector3.up * -1* flyInterval);

                yield return new WaitForFixedUpdate();
            }

            time = 0;

            yield return new WaitForSeconds(stopTime);

        }


    }

    IEnumerator StartRotation()
    {
        while (true)
        {

            transform.Rotate(Vector3.up* rotationSpeed);

            yield return new WaitForSeconds(Time.deltaTime* rotationInterval);
        }

    }


}
