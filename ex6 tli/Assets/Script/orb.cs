using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orb : MonoBehaviour

{
    private Rigidbody2D rb;

    public AudioClip hitSound; 

    private AudioSource audioSource;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.AddComponent<AudioSource>(); 

    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {


            Destroy(gameObject);
        
            Destroy(collision.gameObject);
            ScoreKeeper.ScorePoints(5); 


        }
        if (collision.gameObject.CompareTag("enemyBullet"))
        {

            Destroy(gameObject);




        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
