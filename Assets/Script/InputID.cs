using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputID : MonoBehaviour
{

    public TextMeshProUGUI id = null;

    static InputID Inst;

    private void Awake()
    {
        Inst = this;
    }


}
