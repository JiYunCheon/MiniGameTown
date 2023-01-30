using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    [SerializeField] string sceneName;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(sceneName);
    }


}
