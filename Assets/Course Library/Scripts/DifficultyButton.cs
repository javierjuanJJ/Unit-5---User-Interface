using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{

    private Button button;
    private GameManager gameManagerSpawnRate;
    public int difficulty = 1;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        gameManagerSpawnRate = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(SetDifficulty);
    }

    private void SetDifficulty()
    {
        gameManagerSpawnRate.StartGame(difficulty);
    }

}
