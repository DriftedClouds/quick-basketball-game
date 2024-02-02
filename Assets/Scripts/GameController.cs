using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //TODO
    //Save a high score
    //Change image of the arrow
    //Find a way to make the experience smoother if player hits space extremely quickly twice (set a minimum speed)
    //Add option to convert controls for y axis

    public Text timerText;
    public Text CounterText;
    public GameObject mainScreen;
    public GameObject restartScreen;

    private int fullGameTime = 30;
    private int timeLeft;
    private bool isGameActive = false;
    private float timerRate = 1.0f;
    private float multiplier = 1.0f;
    private float baseScore = 10.0f;
    private float baseMultiplier = 1.0f;
    private float score = 0;


    private PanelManager panelManager;

    // Start is called before the first frame update
    void Start()
    {
        panelManager = GameObject.Find("Panel Manager").GetComponent<PanelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("multiplier is " + multiplier);
    }


    public void StartGame()
    {
        //If this is a restart, then remove the restart screen from player view. If this is a first start, then this does nothing.
        restartScreen.SetActive(false);

        timeLeft = fullGameTime;
        score = 0;
        CounterText.text = "Score : " + score;

        mainScreen.SetActive(false);
        isGameActive = true;
        panelManager.SpawnNewPanel();
        StartCoroutine(TimerCountdown());
    }

    IEnumerator TimerCountdown()
    {
        timerText.text = "Time : " + timeLeft;
        while (isGameActive)
        {
            yield return new WaitForSeconds(timerRate);

            timeLeft--;

            if (isGameActive)
            {
                timerText.text = "Time : " + timeLeft;
            }


            if (timeLeft <= 0)
            {
                GameOver();
            }
        }
    }

    
    private void GameOver()
    {
        //gameOverText.gameObject.SetActive(true);
        //restartButton.gameObject.SetActive(true);
        isGameActive = false;
        restartScreen.gameObject.SetActive(true);
    }

    public bool CheckIfGameActive()
    {
        return isGameActive;
    }

    public void SetMultiplier(float panelMultiplier)
    {
        multiplier = panelMultiplier;
    }

    public void ResetMultiplier()
    {
        multiplier = baseMultiplier;
    }

    public void ScoreBasket()
    {

        //Basket should not be counted if the game is not currently active
        if (CheckIfGameActive())
        {
            score += baseScore * multiplier;
            CounterText.text = "Score : " + score;
            ResetMultiplier();
            //Remove this if panels are done automatically
            panelManager.SpawnNewPanel();
        }
    }


    // Restart game by reloading the scene
    //public void RestartGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
}
