using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float TimeLeft;
    private bool TimerOn;
    private GameManager gameManagerSpawnRate;
    
    public TextMeshProUGUI TimerTxt;

    public bool TimerOn1
    {
        get => TimerOn;
        set => TimerOn = value;
    }

    private void Awake()
    {
        gameManagerSpawnRate = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
                gameManagerSpawnRate.GameOver();
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}