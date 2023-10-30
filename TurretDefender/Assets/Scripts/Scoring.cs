using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private TextMeshProUGUI enemiesKilledValue;
    private float startingScore;
    private int startingEnemiesKilled;
    // Set up starting values for UI
    void Start()
    {
        startingScore = GameManager.player.GetCurrentScore();
        SetScore(startingScore);

        startingEnemiesKilled = GameManager.player.GetEnemiesKilled();
        SetEnemiesKilled(startingEnemiesKilled);
    }

    // Update score and enemies killed for the UI
    public void SetScore(float newScore)
    {
        scoreValue.text = newScore.ToString();
        GameManager.player.SetCurrentScore(newScore);
    }

    public void SetEnemiesKilled(int value)
    {
        enemiesKilledValue.text = value.ToString();
        GameManager.player.SetEnemiesKilled(value);
    }
}
