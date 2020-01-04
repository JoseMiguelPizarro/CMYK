using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;
public class Player : MonoBehaviour
{

    public float moveRadius;
    public ColorStatus currentColor;
    private Material material;
    private int colorIndex = 0; //cyan
    public Ease easemove;
    private float moveDelayCunter;
    public float moveDelay = .2f;
    private int nextDirection;
    private bool wantToMove;
    public Vector3 punchVector = new Vector3(1.1f, 1, .9f);
    public float fuel = 100;
    public float fuelDecreaseRate = 10;
    public FuelBar fuelbar;
    public float sinefloatFrecuency;
    private float baseY;
    private bool dead;
    public UnityEvent onMorir;
    public TextMeshProUGUI scoretextmesh;
    public int score;
    public ParticleSystem trail;
    private ParticleSystem.MainModule psm;
    public AudioClip boxSound;
    public AudioClip barrierSound;
    public AudioClip deadSound;
    public AudioSource audioS;

    public static Player instance;

    private void Awake()
    {
        instance = this;    
    }

    int destroyedBarriers;

    private float _currentFuel;
    private float CurrentFuel
    {
        get => _currentFuel; set
        {
            if (value > fuel)
            {
                _currentFuel = fuel;
            }
            else
            {
                _currentFuel = value;
            }

            if (value < 0)
            {
                dead = true;
                onMorir?.Invoke();
                _currentFuel = 0;
                fuelbar.SetFillRate(0);
            }
        }
    }


    int currentZone = 0; //0 middle -1 down 1 top;
    bool moving;


    // Start is called before the first frame update
    void Start()
    {
        psm = trail.main;
        baseY = transform.position.y;
        CurrentFuel = fuel;
        material = GetComponent<Renderer>().material;
       // this.transform.DOMoveY(1f, .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
    }


    private void SetNextColor()
    {
        colorIndex = (colorIndex + 1) % 3;
        currentColor = (ColorStatus)(colorIndex);

        SetColor(currentColor);

    }

    public void HitColorBox()
    {
        AddFuel(20);
        SetNextColor();
        audioS.clip = boxSound;
        audioS.Play();
    }


    private void SetColor(ColorStatus c)
    {
        switch (c)
        {
            case ColorStatus.cyan:
                material.DOColor(Color.cyan, "_mainColor", .5f);
                psm.startColor = Color.cyan;
                break;
            case ColorStatus.magenta:
                material.DOColor(Color.magenta, "_mainColor", .5f);
                psm.startColor = Color.magenta;

                break;
            case ColorStatus.yellow:
                material.DOColor(Color.yellow, "_mainColor", .5f);
                psm.startColor = Color.yellow;
                break;
            default:
                material.DOColor(Color.white, "_mainColor", .5f);
                psm.startColor = Color.white;
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (dead )
        {
            return;
        }

        float ypos =  Mathf.Sin(Time.time * sinefloatFrecuency)*.25f +baseY;
        transform.position = new Vector3(transform.position.x, ypos, transform.position.z);

        ReadInput();
        CurrentFuel -= Time.deltaTime * fuelDecreaseRate;
        if (wantToMove && !moving)
        {
            wantToMove = false;
            Move(nextDirection);
        }

        float fillRatio = CurrentFuel / fuel;

        if (CurrentFuel < .01f)
        {

            fuelbar.SetFillRate(0);
        }
        else
        {
            fuelbar.SetFillRate(fillRatio);
        }
    }


    public void AddScore(int amount)
    {
        score += amount;
        scoretextmesh.text = score.ToString();
    }

    public void DestroyBarrier()
    {
        AddScore(100);
        destroyedBarriers += 1;
        audioS.clip = barrierSound;
        audioS.Play();
        if (destroyedBarriers%2==0)
        {
            GameManager.instance.SpeedUp();
        }
    }

    public void Die()
    {
        audioS.clip = deadSound;
        audioS.Play();
        onMorir?.Invoke();
        dead = true;
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            nextDirection = -1;
            Move(-1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            nextDirection = 1;
            Move(1);
        }
    }


    public void AddFuel(float amount)
    {
        CurrentFuel += amount;
    }

    private void Move(int dir)
    {
        if (moving)
        {
            wantToMove = true;
            return;
        }

        if (dir < 0 && currentZone > -1)
        {
            moving = true;

            currentZone -= 1;
            transform.DOPunchScale(punchVector, .2f).SetEase(easemove);
            transform.DOMoveZ(transform.position.z + moveRadius * dir, .2f).SetEase(easemove).OnComplete(() => moving = false);
        }
        else if (dir > 0 && currentZone < 1)
        {
            moving = true;
            transform.DOPunchScale(punchVector, .2f).SetEase(easemove);
            currentZone += 1;
            transform.DOMoveZ(transform.position.z + moveRadius * dir, .2f).SetEase(easemove).OnComplete(() => moving = false);

        }
    }
}

public enum ColorStatus { cyan, magenta, yellow, k }
