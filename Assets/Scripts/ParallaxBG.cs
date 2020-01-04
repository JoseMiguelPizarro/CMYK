using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public float speedFactor;
    private Material material;
    private int tillingspeedHash;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        tillingspeedHash = Shader.PropertyToID("_tillingSpeed");
    }



    public void SetSpeed(float speed)
    {
        material.SetFloat(tillingspeedHash, speed * speedFactor);
    }
}
