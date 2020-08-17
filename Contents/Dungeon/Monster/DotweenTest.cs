using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DotweenTest : MonoBehaviour
{

    Tween tween;

    public Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        tween = gameObject.transform.DOMove(new Vector3(10, 10, 10), 5).SetLoops(2, LoopType.Yoyo).OnComplete(() => { Debug.Log("TTTT"); });
        //tween.Play();
    }


 
    // Update is called once per frame
    void Update()
    {

    }
}
