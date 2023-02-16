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

                if (padSpawners[i].deactiveCheck)
                {
                    for (int j = 0; j < padSpawners[i].savePad.Count; j++)
                    {
                        padSpawners[i].savePad[j].gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < padSpawners.Length; i++)
            {
               if(padSpawners[i].deactiveCheck)
                {
                    for (int j = 0; j < padSpawners[i].savePad.Count; j++)
                    {
                        padSpawners[i].savePad[j].gameObject.SetActive(false);
                    }
                }

                if (padSpawners[i].hideCheck)
                {
                    padSpawners[i].gameObject.SetActive(false);
                }
            }
        }
    }

}
