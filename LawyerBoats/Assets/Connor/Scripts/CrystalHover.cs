using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrystalHover : MonoBehaviour
{

    public float startPos;
    public float endPos;
    public float oscillate = 2.00f;

    private void Start()
    {
        transform.DOLocalMoveY(endPos, oscillate, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    void Update()
    {
        //if (transform.position.y <= startPos)
        //{
        //    transform.DOLocalMoveY(endPos, 1, false);
        //}
        //if (transform.position.y >= endPos)
        //{
        //    transform.DOLocalMoveY(startPos, 1, false);
        //}
    }
}
