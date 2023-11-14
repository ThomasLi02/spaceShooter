using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject victoryPanel; 
    public TMP_Text victoryScoreText;
    public bool stopGame = false;
    public static float gameStartTime = 0f;




    private void Update()  
    {
        if (stopGame && Input.GetKeyDown(KeyCode.Y))
        {
            RestartGame();
        }
    }


    public void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true);
        this.gameObject.SetActive(true);
    

        
        Time.timeScale = 0;
        if (victoryScoreText != null && ScoreKeeper.Singleton != null)
        {
            victoryScoreText.text = "Congratulations! \n"+"Score: " + ScoreKeeper.Singleton.Score.ToString();
            victoryScoreText.text += "\nPress Y to restart";
            stopGame = true;
        }
        else
        {
            Debug.LogError("Victory Score Text or ScoreKeeper Singleton not assigned/set properly!");
        }
    }
    public void ShowFailPanel()
    {
        victoryPanel.SetActive(true);
        this.gameObject.SetActive(true);

        Time.timeScale = 0;
        if (victoryScoreText != null && ScoreKeeper.Singleton != null)
        {
            victoryScoreText.text = "You have died to the enemy\n" + "Score: 0";
            victoryScoreText.text += "\nPress Y to restart";
            stopGame = true;
        }
        else
        {
            Debug.LogError("Victory Score Text or ScoreKeeper Singleton not assigned/set properly!");
        }
    }




    public void RestartGame()
    {
        Time.timeScale = 1;
        gameStartTime = Time.time;

        stopGame = false;



        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}


