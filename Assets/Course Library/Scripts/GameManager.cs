using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    public List<GameObject> listPrefabs;
    public float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText, gameOverText, livesText;
    public Button restartButton;
    
    public int score, lives;
    private bool isGameActive, paused;

    public GameObject titleScreen, pauseScreen;
    
    public bool IsGameActive => isGameActive;
    
    
    public TimerScript timerScript;


    private void Awake()
    {
        paused = false;
    }
    
    void ChangePaused()
    {
        paused = !paused;
        pauseScreen.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void StartGame(int difficulty)
    {
        score = 0;
        isGameActive = true;
        scoreToAdd(0);
        livesToAdd(0);
        titleScreen.SetActive(false);
        spawnRate /= difficulty;
        timerScript.TimerOn1 = true;
        StartCoroutine(SpawnRate());
    }

    public void GameOver()
    {
        isGameActive = false;
        timerScript.TimerOn1 = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void scoreToAdd(int scoreNumber)
    {
        score += scoreNumber;
        scoreText.text = "Score: " + score;
    }
    
    public void livesToAdd(int scoreNumber)
    {
        lives += scoreNumber;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private IEnumerator SpawnRate()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int indexPrefab = Random.Range(0, listPrefabs.Count);
            Instantiate(listPrefabs[indexPrefab]);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }
}
