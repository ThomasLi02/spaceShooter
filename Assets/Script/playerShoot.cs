using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject ballPrefab; 
    public float ballSpeed = 10f; 
    public Vector3 ballSpawnOffset = new Vector3(0, 1, 0);
    public AudioClip shootSound; // Reference to the sound effect

    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component to the game object


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }

    void ShootBall()
    {
        Vector3 ballSpawnPosition = transform.position + ballSpawnOffset;

        GameObject ball = Instantiate(ballPrefab, ballSpawnPosition, Quaternion.identity);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * ballSpeed; 
    }
}
