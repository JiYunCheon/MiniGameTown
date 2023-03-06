using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    public bool isSpinning;

    public int spinDir = 1;

    public void startSpin(int spinDir)
    {
        isSpinning = true;
        this.spinDir = spinDir;
    }

    public void stopSpin()
    {
        isSpinning = false;
    }
    private void Update()
    {
        if(isSpinning)
        {
            transform.Rotate(spinDir * Vector3.up * Time.deltaTime * (360f / 5f));
        }
    }
}
