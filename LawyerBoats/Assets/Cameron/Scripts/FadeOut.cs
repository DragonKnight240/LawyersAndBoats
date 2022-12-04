using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] float fadeTime = 2.0f;
    float currentFadeTime = 0;
    CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        currentFadeTime += Time.deltaTime;
        cg.alpha = currentFadeTime / fadeTime;
        if (currentFadeTime >= fadeTime)
        {
            Destroy(gameObject);
        }
    }
}
