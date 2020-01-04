using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColorBox : FloorElement, IPooleable
{
    public float speed;
    public GameObject particle;
    public ParticleSystem ps;
    private ParticleSystem.MainModule psm;
    public BoxCollider col;
    public MeshRenderer meshRenderer;

    private void Awake()
    {
        psm = ps.main;
    }

    private void Update()
    {
        transform.position += -Vector3.right*Time.deltaTime*GameManager.instance.speed;

        if (transform.position.x < GameManager.instance.limitPosition.x)
        {
            OnEnterPool();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>();

        if (p!= null)
        {
            p.HitColorBox();
            p.AddScore(10);
            
            Color psColor = Color.yellow;
            switch (p.currentColor)
            {
                case ColorStatus.cyan:
                    psColor = Color.cyan;
                    break;
                case ColorStatus.magenta:
                    psColor = Color.magenta;
                    break;
                case ColorStatus.yellow:
                    psColor = Color.yellow;
                    break;
                case ColorStatus.k:
                    break;
                default:
                    break;
            }


            OnEnterPool();
            particle.SetActive(true);
            psm.startColor = psColor;
        }

    }

    public new void OnEnterPool()
    {
        meshRenderer.enabled = false;
        col.enabled = false;
    }

    public new void OnLeavePool()
    {
        meshRenderer.enabled = true;
        col.enabled = true;
    }

    public new bool IsBeingUsed()
    {
        return col.enabled;
    }
}
