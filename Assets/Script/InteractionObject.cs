using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private Transform entrance = null;

    public Transform GetEntranceTr { get { return entrance; }private set { } }
}
