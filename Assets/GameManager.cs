using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;



public class UnityIntEvent:UnityEvent<int> { }

[System.Serializable]
public class UnityFloatEvent:UnityEvent<float> { }
public class GameManager : MonoBehaviour
{

    public float speed = 1;
    public static GameManager instance;

    internal void GoalReached()
    {
        onGoalReached?.Invoke();
    }

    public ColorBox colorbox;
    public barrier barrier;
    public Floor floor;
    public ColorStatus nextColor;
    public Transform limitTransform;
    public Vector3 limitPosition;
    private Queue<ColorStatus> colorsQueue = new Queue<ColorStatus>();
    public UnityEvent onSpeedUp;
    public UnityEvent onHighSpeed;
    public UnityEvent onGoalReached;
    public UnityFloatEvent onSetSpeed;
    public int score;
    private bool playerIsDead;
    public GameObject speedUpText;
    public ColorText colorText;
    private bool goalReached;
    public GameObject tutorialCanvas;
    private bool started;


    public Texture2D level;

    public List<ColorElement> elements;

    int currentColumn;
    int currentChunck = 15;
    float timer;

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

        limitPosition = limitTransform.position;
    }


    private void Start()
    {
        onSetSpeed?.Invoke(speed);
        MusicManager.instance.PlayNormalClip();
    }


    private void Update()
    {

        if (playerIsDead || goalReached)
        {
            return;
        }

        timer += Time.deltaTime * speed;
        if (timer > 1)
        {
            timer = 0;
            currentChunck++;
        }


        if (currentChunck >= 15)
        {
            currentChunck = 0;

            for (int j = 0; j < 16; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    Color pixelColor = level.GetPixel(currentColumn, i);
                    GenerateElement(pixelColor, i, j);
                }
                currentColumn++;
            }
        }

    }



 
    public ColorStatus AddNextColor()
    {

        int next = UnityEngine.Random.Range(0, 3);
        ColorStatus cs = (ColorStatus)next;
        if (colorsQueue.Count == 0)
        {
            nextColor = cs;
            colorsQueue.Enqueue(cs);
            colorText.SetText(nextColor);
        }
        else
        {
            colorsQueue.Enqueue(cs);
        }

        return cs;
    }


    public void SetNextColor()
    {
        if (colorsQueue.Count > 0)
        {
            colorsQueue.Dequeue();
            if (colorsQueue.Count > 0)
            {
                nextColor = colorsQueue.Peek();

                colorText.SetText(nextColor);
            }
        }
    }


    
    private void GenerateElement(Color pixelColor, int index, int col)
    {


        if (pixelColor.a == 0)
        {
            return;
        }


        if (pixelColor == Color.red)
        {
            SpeedUp();
            return;
        }

        if (pixelColor == Color.yellow)
        {
            goalReached = true;
        }


        foreach (var element in elements)
        {
            if (pixelColor == element.color)
            {
                if (element.color == Color.cyan)
                {
                    ColorStatus c = AddNextColor();
                    SpawnBarriers(index, col, element.gameObject, c);
                }
                else
                {
                    FloorElement go = ObjectPooler.instance.GetObject(element.gameObject);
                    go.SetColor(ColorStatus.k);
                    floor.PlaceElement(go.transform, index, col);
                }

                return;
            }
        }
    }


    public void onPlayerDie()
    {
        playerIsDead = true;
        speed = 0;
        onSetSpeed?.Invoke(0);
    }


    public void SpeedUp()
    {
        onSpeedUp?.Invoke();
        speedUpText.SetActive(true);
        speed += .5f;
        StartCoroutine(WaitSpeedupText(3));
        onSetSpeed?.Invoke(speed);
        
        if (speed>7)
        {
            onHighSpeed?.Invoke();
        }
    }


    private void SpawnBarriers(int coloredBarrierPos, int col, FloorElement bar, ColorStatus color)
    {

        if (!started)
        {
            tutorialCanvas.SetActive(false);
            started = true;    
        }

        barrier coloredBarrier = ObjectPooler.instance.GetObject(bar as barrier);

        for (int i = 0; i < 3; i++)
        {
            if (i == coloredBarrierPos)
            {
                coloredBarrier.SetColor(color);
                floor.PlaceElement(coloredBarrier.transform, i, col);

            }
            else
            {
                barrier barrier = ObjectPooler.instance.GetObject(bar as barrier);
                barrier.SetColor(ColorStatus.k);
                coloredBarrier.Subscribe(barrier);
                floor.PlaceElement(barrier.transform, i, col);
            }
        }
    }


    IEnumerator WaitSpeedupText(float time)
    {
        yield return new WaitForSeconds(time);
        speedUpText.SetActive(false);

    }
}
