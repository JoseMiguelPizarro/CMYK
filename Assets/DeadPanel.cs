using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeadPanel : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    public void Retry()
    {
        Transition.instance.FadeIn(() => SceneManager.LoadScene(1));
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Retry();
        }

     else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Quit();
        }
    }

    public void DisplayPanel()
    {

        scoreText.text = Player.instance.score.ToString();
    }


    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
