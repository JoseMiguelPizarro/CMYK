using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Transition : MonoBehaviour
{

    Material material;
    public Image image;
    private int disolveHash;
    public static Transition instance;


    private void Awake()
    {
        material = image.material;
        disolveHash = Shader.PropertyToID("_disolve");

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        FadeOut();
    }

    public void FadeIn(Action onComplete)
    {
        material.DOFloat(0, disolveHash, 1.5f).OnComplete(() => { onComplete?.Invoke(); material.SetFloat(disolveHash, 0); });
        
    }


    public void FadeOut(Action onComplete)
    {
        material.DOFloat(1.1f, disolveHash, 1.5f).OnComplete(() => onComplete?.Invoke());
    }


    public void FadeOut()
    {
        material.DOFloat(1.1f, disolveHash, 1);

    }

    public void FadeIn()
    {
        material.DOFloat(0, disolveHash, 1).OnComplete(()=>material.SetFloat(disolveHash,0));
    }

}
