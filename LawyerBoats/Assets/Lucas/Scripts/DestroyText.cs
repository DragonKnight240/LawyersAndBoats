using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyText : MonoBehaviour
{
    [SerializeField] private float secondsToDestroy;
    void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }
}
