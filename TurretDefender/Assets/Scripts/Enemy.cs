using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, 8f)]
    [SerializeField] private float enemySpeedThreshold;
    float randSpeed;
    public bool isPopulated = false;
    private System.Random rand = new System.Random();

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManager.IsPlaying())
            return;

        if (!isPopulated)
        {
            randSpeed = rand.Next((int)enemySpeedThreshold, (int)(enemySpeedThreshold * 1.5));
            isPopulated = true;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, GameManager.player.transform.position, randSpeed * Time.deltaTime);
    }

    // If the enemy collides with the player, player's health bar decreases
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.healthBar.AlterHealthBar(30);
            GameManager.player.GetComponent<AudioSource>().Play();
            isPopulated = false;
            Destroy(this.gameObject);
        }
    }
}
