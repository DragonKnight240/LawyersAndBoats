using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] float fadeTime = 2.0f;
    float currentFadeTime = 0;
    CanvasGroup cg;
    [SerializeField] bool useColour = false;
    Color color;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        if(GetComponent<TextMesh>())
        {
            color = GetComponent<TextMesh>().color;
        }
    }

    void Update()
    {
        currentFadeTime += Time.deltaTime;
        if(useColour)
        {
            color.a = (1 - (currentFadeTime / fadeTime));
            GetComponent<TextMesh>().color = color;
            if (color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            cg.alpha = (1 - (currentFadeTime / fadeTime));
            if (cg.alpha <= 0)
            {
                Destroy(gameObject);
            }
        }
        

    }
}
