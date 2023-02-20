using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    [SerializeField] string sceneName;

   
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Inst.ListClear();

        SceneManager.LoadScene(sceneName);
    }

}
