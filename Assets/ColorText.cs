using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ColorText : MonoBehaviour
{
    public TextMeshProUGUI colorText;
    public Image mask;


    public void SetText(ColorStatus c)
    {
        StopAllCoroutines();

        mask.DOFillAmount(1, .3f);

        colorText.SetText("wea");
        StartCoroutine(Wait(3));

        switch (c)
        {
            case ColorStatus.cyan:
                colorText.SetText("ONLY <color=#00ffffff>CYAN</color>");
                break;
            case ColorStatus.magenta:
                colorText.SetText("ONLY <color=#ff00ffff>MAGENTA</color>");
                break;
            case ColorStatus.yellow:
                colorText.SetText("ONLY <color=#ffff00ff>YELLOW</color>");
                break;
        }
    }

    IEnumerator Wait(float value)
    {
        yield return new WaitForSeconds(value);
        mask.DOFillAmount(0, .2f);
    }





}
