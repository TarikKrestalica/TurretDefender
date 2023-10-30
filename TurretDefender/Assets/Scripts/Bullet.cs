using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    [Range(0, 50f)]
    [SerializeField] private float bulletSpeed;  // Limitations to speed: https://www.youtube.com/watch?v=PI0D0FjP4LA

    private void Start()  // Had the idea with rotation, but needed to account for rotation: https://forum.unity.com/threads/solved-bullets-always-shooting-in-one-direction.831940/
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * bulletSpeed, ForceMode.VelocityChange);

    }
    private void Update()
    {
        if (!GameManager.gameManager.gameObject.activeInHierarchy)
            return;

        // Bullet goes out of bounds
        if (Mathf.Abs(this.transform.position.x) >= GameManager.floor.transform.localScale.x || Mathf.Abs(this.transform.position.z) >= GameManager.floor.transform.localScale.z)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.gameManager.IsPlaying())
            return;

        if (collision.gameObject.tag == "Enemy")  // Increase score, indicate enemies killed!
        {
            Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Enemy>().isPopulated = false;

            float oldScore = GameManager.player.GetCurrentScore();
            GameManager.scoreSystem.SetScore(oldScore + GameManager.gameManager.scoreBoost);

             float currentScore = GameManager.player.GetCurrentScore();
             if (currentScore == Mathf.FloorToInt(GameManager.counterScript.GetLimit() / 30f) * GameManager.gameManager.scoreBoost)
             {
                GameManager.gameManager.respawnTime -= .5f;
             }
             else if (currentScore == Mathf.FloorToInt(GameManager.counterScript.GetLimit() / 15f) * GameManager.gameManager.scoreBoost)
             {
                GameManager.gameManager.respawnTime -= .5f;
             }
             else if (currentScore == Mathf.FloorToInt(GameManager.counterScript.GetLimit() / 10f) * GameManager.gameManager.scoreBoost)
             {
                GameManager.gameManager.respawnTime -= 1f;
             }
             else if (currentScore == Mathf.FloorToInt(GameManager.counterScript.GetLimit() / 8f) * GameManager.gameManager.scoreBoost)
             {
                GameManager.gameManager.respawnTime -= 1f;
             }

            // Decrease spawning time here! 
            int curEnemiesKilled = GameManager.player.GetEnemiesKilled();
            GameManager.scoreSystem.SetEnemiesKilled(curEnemiesKilled + 1);

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)  // When the bullet goes past the playspace
    {
        if (other.transform.tag == "Playspace")
        {
            Destroy(this.gameObject);
        }
    }
}
