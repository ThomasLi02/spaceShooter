using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyOrb : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameController gameController; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
          

        }
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            Destroy(gameObject);
            ScoreKeeper.ScorePoints(1); // Increase the score


        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
