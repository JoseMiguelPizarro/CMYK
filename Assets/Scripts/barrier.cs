using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class barrier : FloorElement, IPooleable
{

    public MeshRenderer meshRenderer;
    public BoxCollider col;
    private Material material;
    public float speed;
    public ColorStatus colorStatus;
    private int colorHash, disolveHash;
    private barrier[] _peers = new barrier[2];
    private Action onDestroy;
    public ParticleSystem ps;
    private ParticleSystem.MainModule psm;
    private bool inited;


    // Start is called before the first frame update


    void Start()
    {
        psm = ps.main;
        material = meshRenderer.material;
        colorHash = Shader.PropertyToID("_color");
        disolveHash = Shader.PropertyToID("_disolve");
    }

    public void SetPeer(barrier p, int i)
    {
        _peers[i] = p;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -Vector3.right * GameManager.instance.speed * Time.deltaTime;
        if (transform.position.x < GameManager.instance.limitPosition.x)
        {
            OnEnterPool();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>();

        if (p)
        {
            if (p.currentColor == colorStatus)
            {
                DestroyBarrier();
                p.DestroyBarrier();
            }
            else
            {
                p.Die();
            }
        }
    }

    public override void SetColor(ColorStatus colorStatus)
    {

        this.colorStatus = colorStatus;


        switch (colorStatus)
        {
            case ColorStatus.cyan:
                material.SetColor(colorHash, Color.cyan);
                psm.startColor = Color.cyan;
                break;
            case ColorStatus.magenta:
                material.SetColor(colorHash, Color.magenta);
                psm.startColor = Color.magenta;

                break;
            case ColorStatus.yellow:
                material.SetColor(colorHash, Color.yellow);
                psm.startColor = Color.yellow;

                break;
            case ColorStatus.k:
                material.SetColor(colorHash, Color.black);
                psm.startColor = Color.black;

                break;
        }
    }


    private void DestroyBarrier()
    {
        CameraController.instance.Shake();
        GameManager.instance.SetNextColor();
        ps.gameObject.SetActive(true);
        onDestroy?.Invoke();
        onDestroy = null;
        OnEnterPool();
    }

    public void Subscribe(barrier b)
    {
        onDestroy += b.OnEnterPool;
    }


    public new void OnEnterPool()
    {
        col.enabled = false;
        material.DOFloat(1, disolveHash, .3f).OnComplete(() => ActivateMeshAndCollider(false));
    }

    private void ActivateMeshAndCollider(bool value)
    {
        meshRenderer.enabled = value;
        col.enabled = value;
    }



    public new void OnLeavePool()
    {
        if (!inited)
        {
            inited = true;
            disolveHash = Shader.PropertyToID("_disolve");
            colorHash = Shader.PropertyToID("_color");
            material = meshRenderer.material;
            psm = ps.main;
        }


        material.SetFloat(disolveHash, 0);
        ActivateMeshAndCollider(true);
    }

    public new bool IsBeingUsed()
    {
        return meshRenderer.enabled;
    }
}
