using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    [SerializeField] string sceneName;

   
    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.Inst.CompareLoadScene())
        {
            GameManager.Inst.SaveData();
            GameManager.Inst.ListClear();
        }

        SceneManager.LoadScene(sceneName);
    }

}
