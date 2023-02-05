using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadController : MonoBehaviour
{
    [SerializeField] private PadSpawner[] padSpawners = null;



    public void ActivePadByType(OBJECT_TYPE type)
    {
        if(type== OBJECT_TYPE.OBJECT)
        {
            for (int i = 0; i < padSpawners.Length; i++)
            {
                padSpawners[i].gameObject.SetActive(true);
            }
        }
        else
        {
            bool active = true;
            for (int i = 0; i < padSpawners.Length; i++)
            {
                if (i > 7)
                    active = false;

                padSpawners[i].gameObject.SetActive(active);
            }
        }
    }

}
