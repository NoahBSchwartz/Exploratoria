using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 30;
    public bool timerIsRunning = false;
    public Text timeText;
    public GameObject timer;
    public GameObject endScreen; 
    public GameObject player;
    public GameObject enemy; 
    public Text winTime;
    public Text finalScore;
    public bool winScreen = false;
    static float timeSaver;
    private bool freezeTime = false;
    private float endScore;
    private float educationNum;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timer.GetComponent<Canvas>().enabled = false;
        //timer.SetActive(false);
        timeText.enabled = false;
        endScreen.SetActive(false);
    }

    void Update()
    {
        if ((timerIsRunning) && (player.transform.position.x != 2))
        {
            timer.GetComponent<Canvas>().enabled = true;
            timeText.enabled = true;
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                timeSaver += Time.deltaTime;
            }
            else
            {
                //time runs out
                enemy.SetActive(false);
                endScreen.SetActive(true);
                timeRemaining = 0;
                timerIsRunning = false;
                player.transform.position = new Vector3(40f, -2f, 0f);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if ((winScreen == true) && (freezeTime == false))
        {
            endScore += 3756f;//(GameObject.Find("EndScreen").GetComponent<LevelMovement>().finalScoreNum)/10f;
            educationNum = (GameObject.Find("EndScreen").GetComponent<Plaques>().educationScoreNum)*100f;
            Debug.Log(educationNum);
            endScore += educationNum;
            timeSaver = (500 - timeSaver) * 10;
            endScore += timeSaver;
            timeSaver = (int)timeSaver;
            endScore = (int)endScore;
            winTime.text = timeSaver.ToString();
            finalScore.text = endScore.ToString();
            freezeTime = true;
        }
        //timeText.text = seconds.ToString("00");//string.Format(seconds);
    }
}