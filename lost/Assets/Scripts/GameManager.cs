using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float timedelay = 2f;
    public PlayerMovement movement;
    public GameObject completeLevel;
    public GameObject pauseLevel;
    public GameObject loseLevel;

    bool isPaused = false;
    public void Exit()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            //Debug.Log("END");
            movement.enabled = false;
            loseLevel.SetActive(true);
            Invoke("Restart",timedelay);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        completeLevel.SetActive(true);
        movement.enabled = false;
    }

    void LoseLevel()
    {
        completeLevel.SetActive(true);
    }

    public void pause()
    {
        if (isPaused) 
        {
            pauseLevel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            pauseLevel.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        
        
    }
}
