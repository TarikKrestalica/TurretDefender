using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    [SerializeField] private GameObject GameOverDisplay;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManager.IsPlaying())
            return;

        if (GameManager.counterScript.GetCurrentDuration() <= 0f || GameManager.healthBar.widthOfHealthBar <= 0f) 
        {
            RemoveObjectsFromScene(GameObject.FindGameObjectsWithTag("Enemy"));
            RemoveObjectsFromScene(GameObject.FindGameObjectsWithTag("Bullet"));

            GameManager.gameManager.SetPlaying(false);
            GameManager.counterScript.SetCurrentDuration(GameManager.counterScript.GetLimit());
            GameManager.counterScript.SetStartingDuration(GameManager.counterScript.GetLimit());
            GameManager.healthBar.ResetHealthBar();

            score.text = GameManager.player.GetCurrentScore().ToString();
            enemiesKilled.text = GameManager.player.GetEnemiesKilled().ToString();

            for (int i = 0; i < this.transform.childCount; i++)  // Deactive some UI, need to modify health bar back to normal
            {
                if (this.transform.GetChild(i).gameObject.tag == "ScoringSystem" || this.transform.GetChild(i).gameObject.tag == "TimeCounter" || this.transform.GetChild(i).gameObject.tag == "HealthBar" || this.transform.GetChild(i).gameObject.tag == "Minimap")
                    this.transform.GetChild(i).gameObject.SetActive(false);
            }

            GameOverDisplay.SetActive(true);
        }
    }

    void RemoveObjectsFromScene(GameObject[] gameObjects)  // Knew I wanted to clear all enemies from game, but needed syntax help: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
