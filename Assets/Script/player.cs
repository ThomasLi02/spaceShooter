using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; 
    public int lives = 3; 
    public GameObject bodyPrefab; 


    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private GameObject bodyInstance; 
    private bool isInvincible = false; 
    private float invincibleEndTime; 

    public GameController gameController; 

    public AudioClip replace; 
    private AudioSource audioSource;



    void Start()
    {
        CalculateBounds();
        Respawn(); 

        audioSource = gameObject.AddComponent<AudioSource>(); 
    }

    void Update()
    {
        MovePlayer();
        CheckBounds();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (bodyInstance == null)
            {
                LeaveBody();
            }
            else
            {
                TeleportToBody();
            }
        }
        if (isInvincible && Time.time >= invincibleEndTime)
        {
            isInvincible = false;
            GetComponent<SpriteRenderer>().color = Color.white; 
        }
    }

    void CalculateBounds()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; 
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; 
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void CheckBounds()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        transform.position = viewPos;
    }

    public void LoseLife()
    {
        if (!isInvincible)
        {
            lives--; 
            ScoreKeeper.LoseLife(); 


            if (lives > 0)
            {
                Respawn();
                isInvincible = true;
                invincibleEndTime = Time.time + 3f; 
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); 


            }
            else
            {
                GameOver();
            }
        }
        
    }

    void LeaveBody()
    {
        bodyInstance = Instantiate(bodyPrefab, transform.position, Quaternion.identity);

        audioSource.PlayOneShot(replace);
    }

    void TeleportToBody()
    {
        transform.position = bodyInstance.transform.position;
        Destroy(bodyInstance); 
        bodyInstance = null; 

        audioSource.PlayOneShot(replace);
    }

    void Respawn()
    {
       
        transform.position = new Vector3(0, screenBounds.y * -1 + objectHeight, 0);
    }

    void GameOver()
    {
        gameObject.SetActive(false);
        
        if (gameController != null) 
        {
            gameController.ShowFailPanel();
        }
        else
        {
            Debug.LogError("GameController is not assigned in the Inspector of EndGoalScript.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            LoseLife();


        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            LoseLife();


        }
    }

}
