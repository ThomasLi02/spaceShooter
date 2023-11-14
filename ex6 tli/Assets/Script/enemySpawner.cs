using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnRate = 5.0f; 

    private float nextSpawnTime;
    private float spawnXLimit;
    private float timeToIncreaseRate = 15.0f; 
    public GameController gameController; 


    void Start()
    {
        spawnXLimit = Camera.main.orthographicSize * Camera.main.aspect;

        nextSpawnTime = Time.time + spawnRate;
    }

    void Update()
    {

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
        float timeSinceGameStart = Time.time - GameController.gameStartTime;
        if (Mathf.Floor(timeSinceGameStart / timeToIncreaseRate) == 4)
        {
            spawnRate = 3.0f;
        }
        else
        {
            spawnRate = 5.0f - Mathf.Floor(timeSinceGameStart / timeToIncreaseRate); 

        }


}

void SpawnEnemy()
    {
        float randomXPosition = Random.Range(-spawnXLimit, spawnXLimit);
        float enemyHeight = enemyPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        float spawnYPosition = Camera.main.orthographicSize + enemyHeight / 2; 
        Vector2 spawnPosition = new Vector2(randomXPosition, spawnYPosition);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        newEnemy.transform.Rotate(new Vector3(0, 0, 180));
    }

}
