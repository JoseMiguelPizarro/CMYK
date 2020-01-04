using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private Material material;
    private int offsetIndex;
    private Vector4 offset;
    private Vector4 rightVector4 = new Vector4(1, 0, 0, 0);
    public Transform spawnTransform;
    private Vector3 spawnPoint;

    public float speed;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        offsetIndex = Shader.PropertyToID("_offset");
        offset = material.GetVector(offsetIndex);
        spawnPoint = spawnTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector4 deltaOffset = rightVector4 * GameManager.instance.speed * Time.deltaTime;
        offset += deltaOffset;
        material.SetVector(offsetIndex, offset);
    }

    internal void PlaceElement(Transform element, int index, int col)
    {
        Vector3 spawnPosition = spawnPoint;
        spawnPosition.z += index * 2;
        spawnPosition.x += col;
        element.transform.position = spawnPosition;
    }
}
