using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class FuelBar : MonoBehaviour
{
    public Image fuelBar;
    private Material material;
    private int blinkHash;
    private int blinkSpeedHash;
    public float barFillSpeed;
    public float blinkThreshold = .3f;
    public float maxBlinkSpeed = 40;
    public float minBlinkSpeed = 25;

    private void Awake()
    {
        material = fuelBar.material;
        blinkHash = Shader.PropertyToID("_blink");
        blinkSpeedHash = Shader.PropertyToID("_blinkSpeed");
    }


    public void SetFillRate(float rate)
    {
        if (rate ==0)
        {
            fuelBar.fillAmount = 0;
            return;
        }


        fuelBar.fillAmount = Mathf.Lerp(fuelBar.fillAmount, rate, Time.deltaTime * barFillSpeed);

        if (rate < blinkThreshold)
        {
            material.SetFloat(blinkHash, 1);
            float blinkSpeed = Mathf.Lerp(maxBlinkSpeed, minBlinkSpeed, rate / blinkThreshold);
            material.SetFloat(blinkSpeedHash, blinkSpeed);
        }
        else
        {
            material.SetFloat(blinkHash, 0);

        }
    }
}
