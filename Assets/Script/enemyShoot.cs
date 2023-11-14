using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float shootingRate = 1.0f; 
    public float bulletSpeed = 5.0f; 
    public Vector2 bulletSpawnOffset = new Vector2(0, -1.5f); 
    public float moveSpeed = 0.6f;

    private float nextShotTime;
    private float timeToIncreaseRate = 15.0f; 
    public float increasedShootingRate = 0.5f; 
    private Vector2 moveDirection = new Vector2(5, -1);



    public GameController gameController; 

    private float spriteWidth;


    void Start()
    {
        nextShotTime = Time.time + shootingRate;
        float timeSinceGameStart = Time.time - GameController.gameStartTime;

        shootingRate =  1 - Mathf.Floor(timeSinceGameStart / timeToIncreaseRate) / 5f;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);
        if (Time.time > nextShotTime)
        {
            ShootAtPlayer();
            nextShotTime = Time.time + shootingRate; 
        }
        float timeSinceGameStart = Time.time - GameController.gameStartTime;

        if (Mathf.Floor(timeSinceGameStart / timeToIncreaseRate) == 4)
        {
            shootingRate = 0.5f; // Time between the shots


            if (IsAtRightEdgeOfScreen())
            {
                moveDirection = new Vector2(-5, -1f);
            }
            else if (IsAtLeftEdgeOfScreen())
            {
                moveDirection = new Vector2(5, -1f);

            }
            transform.Translate(moveDirection * 0.7f*moveSpeed * Time.deltaTime, Space.World);

        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);

        }

    }

    bool IsAtRightEdgeOfScreen()
    {
        float rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float enemyRightEdge = transform.position.x + spriteWidth;
        return enemyRightEdge >= rightEdge;
    }
    bool IsAtLeftEdgeOfScreen()
    {
        float leftScreenBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float enemyLeftEdge = transform.position.x - spriteWidth;
        return enemyLeftEdge <= leftScreenBound;
    }





    void ShootAtPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) 
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 bulletSpawnPosition = transform.position + (Vector3)bulletSpawnOffset;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
            Vector2 direction = (playerPosition - transform.position).normalized;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);


        }
    }

}
