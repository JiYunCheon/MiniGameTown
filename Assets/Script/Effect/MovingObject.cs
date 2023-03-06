using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private AnimationCurve myCurve = null;
    [SerializeField] private float speed = 0;
    [SerializeField] private float destroyPosZ = 0;
    private float curTime;
    float save = 0;
    private void Awake()
    {
        save = transform.position.x;
        moveSquence();
    }

    private void Update()
    {
        if(transform.position.z<destroyPosZ)
        {
            Destroy(this.gameObject);
        }
    }

    public void moveSquence()
    {
        StartCoroutine(moveCoroutine());
    }


    IEnumerator moveCoroutine()
    {
        while(true)
        {
            curTime += Time.deltaTime;
            if (curTime >= 4f)
            {
                curTime -= curTime;
            }

            transform.position = new Vector3(save + myCurve.Evaluate(curTime), transform.position.y, transform.position.z);
            transform.Translate(new Vector3(0, 0, 1f) * Time.deltaTime*speed);

            yield return null;  
        }

    }


}
