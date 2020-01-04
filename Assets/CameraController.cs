using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{

    public Camera main;

    public static CameraController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

  
    public void Shake()
    {
        main.DOShakePosition(.1f,.45f,3,40).SetEase(Ease.InOutCubic);
    }

}
