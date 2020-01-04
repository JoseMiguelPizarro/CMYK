using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    public Transform p;
    private Vector3 baseScale;
    public float sinAmplitude;


    private void Start()
    {
        baseScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 newTransform = p.transform.position;
        newTransform.y = transform.position.y;
        transform.position = newTransform;



        Vector3 newScale = baseScale * (Mathf.Sin(Time.time * sinAmplitude) * .25f + 1);
        transform.localScale = new Vector3(newScale.x, newScale.z, 1);

    }
}
