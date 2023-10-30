using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Limitations to speed: https://www.youtube.com/watch?v=PI0D0FjP4LA
    [Range(0, 100f)]
    [SerializeField] private float rotationSpeed;
    private float currentScore;
    private int enemiesKilled;
    private Vector3 mousePosition;

    void Start()
    {
        currentScore = 0f;
        enemiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation by childing the camera under the player
        Vector3 curRotation = this.transform.localEulerAngles;
        if (Input.GetKey(KeyCode.A))
        {
            curRotation.y -= rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            curRotation.y += rotationSpeed * Time.deltaTime;
        }

        this.transform.localEulerAngles = curRotation;

        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.gameManager.IsPlaying())
                return;

            Instantiate(GameManager.bullet, GameManager.player.transform.position + GameManager.player.transform.forward * 1.2f, GameManager.player.transform.rotation);
        }

    }

    public void SetCurrentScore(float aValue)
    {
        currentScore = aValue;
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public void SetEnemiesKilled(int value)
    {
        enemiesKilled = value;
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public Vector3 GetFiredMousePosition()
    {
        return mousePosition;
    }
}


