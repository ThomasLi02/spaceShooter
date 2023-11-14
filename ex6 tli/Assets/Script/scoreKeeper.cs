using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Singleton;

    public AudioClip scoreSound; // Reference to the sound effect
    public AudioClip dieSound; // Reference to the sound effect


    private AudioSource audioSource; // Reference to the AudioSource component
    public GameController gameController; // Make sure to assign this in the Inspector


    public int Score;
    public int Lives;
    public float Level;


    private TMP_Text scoreDisplay;

    void Start()
    {
        Singleton = this;
        scoreDisplay = GetComponent<TMP_Text>();
        audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component to the game object
        Score = 0;
        Lives = 3;
        Level = 1;

    }
    private void Update()
    {
        float timeSinceGameStart = Time.time - GameController.gameStartTime;

        Level = 1 + Mathf.Floor(timeSinceGameStart / 15f);

        if (Level == 6)
        {
            gameController.ShowVictoryPanel();
            scoreDisplay.text = "Score: " + Score.ToString() + "\n Lives: " + Lives.ToString() + "\n Level: " + 5.ToString();

        }
        scoreDisplay.text = "Score: " + Score.ToString() + "\n Lives: " + Lives.ToString() + "\n Level: " + Level.ToString();
    }

    public static void ScorePoints(int points)
    {
        Singleton.ScorePointsInternal(points);
    }
    public static void LoseLife()
    {
        Singleton.LoseLifeInternal();

    }

    private void ScorePointsInternal(int delta)
    {
        Score += delta;
        scoreDisplay.text = "Score: " + Score.ToString() + "\n Lives: " + Lives.ToString() + "\n Level: " + Level.ToString();

        // Play the sound effect
        if (scoreSound != null)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }
    private void LoseLifeInternal()
    {
        if (scoreSound != null)
        {
            audioSource.PlayOneShot(dieSound);
        }

        Lives -= 1;
        if(Lives == 0)
        {
            Level = 1;
        }

        scoreDisplay.text = "Score: " + Score.ToString() + "\n Lives: " + Lives.ToString() + "\n Level: " + Level.ToString();

    }
}
