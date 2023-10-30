using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference from GAM-335: Have all the pieces needed when called upon!
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private System.Random rand = new System.Random();
    public float scoreBoost;
    private float oldRespawnTime;
    [Range(1f, 4f)]
    public float respawnTime;

    private bool playing;

    private float respawnCounter = 0f;

    private System.Random rnd = new System.Random();

    private void Awake()
    {
        oldRespawnTime = respawnTime; // Reset back to normal
        playing = false;
        if(gameManager != null)
        {
            Destroy(gameManager);
        }
        gameManager = this;
        DontDestroyOnLoad(gameManager);
    }

    public void Update()
    {
        if (!gameManager.IsPlaying())
            return;

        if(counterScript.GetCurrentDuration() <= 0f)
        {
            SetPlaying(false);
            return;
        }

        if (respawnCounter > 0f)
        {
            respawnCounter -= Time.deltaTime;
            return;
        }

        Vector3 randPos = Vector3.up;

        // Have enemies spawn at the corners of the space
        if(rnd.Next(0, 100) % 2 == 0)
        {
            randPos.x = rnd.Next(0, Mathf.FloorToInt(floor.transform.localScale.x) / 2);
        }
        else
        {
            randPos.x = rnd.Next(-Mathf.FloorToInt(floor.transform.localScale.x / 2), 0);
        }

        if (rnd.Next(0, 100) % 2 == 0)
        {
            randPos.z = rnd.Next(Mathf.FloorToInt((floor.transform.localScale.z - 20) / 2), Mathf.FloorToInt(floor.transform.localScale.z / 2));
        }
        else
        {
            randPos.z = rnd.Next(Mathf.FloorToInt(-floor.transform.localScale.z / 2), Mathf.FloorToInt((-floor.transform.localScale.z + 20) / 2));
        }

        Instantiate(enemy, randPos, Quaternion.identity);
        respawnCounter = respawnTime;
        
    }

    // Gain a reference to the scoring system and the player
    public static Player player
    {
        get
        {
            if(gameManager.m_player == null)
            {
                gameManager.m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            return gameManager.m_player;
        }
    }

    private Player m_player;

    public static Scoring scoreSystem
    {
        get
        {
            if (gameManager.m_scoringSystem == null)
            {
                gameManager.m_scoringSystem = GameObject.FindGameObjectWithTag("ScoringSystem").GetComponent<Scoring>();
            }

            return gameManager.m_scoringSystem;
        }
    }

    private Scoring m_scoringSystem;

    public static HealthBar healthBar
    {
        get
        {
            if (gameManager.m_healthBar == null)
            {
                gameManager.m_healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
            }

            return gameManager.m_healthBar;
        }
    }

    private HealthBar m_healthBar;

    public static Enemy enemy
    {
        get
        {
            if(gameManager.m_enemy == null)  // Get object, then script component
            {
                GameObject enemy = Resources.Load("Prefabs/Enemy") as GameObject;
                gameManager.m_enemy = enemy.GetComponent<Enemy>();
            }

            return gameManager.m_enemy;
        }
    }

    private Enemy m_enemy;

    public static GameObject bullet
    {
        get
        {
            if(gameManager.m_bullet == null)
            {
                gameManager.m_bullet = Resources.Load("Prefabs/Bullet") as GameObject;
            }

            return gameManager.m_bullet;
        }
    }

    private GameObject m_bullet;

    public static GameObject floor
    {
        get
        {
            if(gameManager.m_floor == null)
            {
                gameManager.m_floor = Resources.Load("Prefabs/Floor") as GameObject;
            }

            return gameManager.m_floor;
        }
    }

    private GameObject m_floor;

    // Testing purposes, are the positions accurate
    public static GameObject targetPoint
    {
        get
        {
            if(gameManager.m_target == null)
            {
                gameManager.m_target = Resources.Load("Prefabs/TargetPoint") as GameObject;
            }

            return gameManager.m_target;
        }
    }

    private GameObject m_target;

    public static CounterScript counterScript
    {
        get
        {
            if(gameManager.m_counterScript == null)
            {
                gameManager.m_counterScript = GameObject.FindGameObjectWithTag("TimeCounter").GetComponent<CounterScript>();
            }

            return gameManager.m_counterScript;
        }
    }

    private CounterScript m_counterScript;

    public void SetPlaying(bool toggle)
    {
        playing = toggle;
    }

    public bool IsPlaying()
    {
        return playing;
    }

    public void ResetGame()
    {
        player.SetCurrentScore(0f);
        player.SetEnemiesKilled(0);
        scoreSystem.SetScore(player.GetCurrentScore());
        scoreSystem.SetEnemiesKilled(player.GetEnemiesKilled());
        respawnTime = oldRespawnTime;
    }
}
