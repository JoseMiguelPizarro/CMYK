using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aerial : MonoBehaviour
{
    public static Aerial instance;
    private Material material;
    private int colorHash;
    public Transform player;
    private float startY;
    private float Ydistance;
    private int barriersBroken;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        material = GetComponent<MeshRenderer>().material;
        colorHash = Shader.PropertyToID("_mainColor");

        startY = transform.position.y;
        Ydistance = transform.position.y - player.position.y;
    }

    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = Ydistance + player.position.y;

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2);
    }


    public void SetColor(Color color)
    {
        material.SetColor(colorHash, color);
    }
}
