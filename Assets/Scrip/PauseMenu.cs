using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    public void resumeGame()
    {
        Time.timeScale = 1f; // Resume the game by setting time scale back to 1
        pauseCanvas.SetActive(false); // Hide the pause canvas
        Cursor.visible = false; // Hide the cursor
    }

    public void quitGame()
    {
        Time.timeScale = 1f; // Pause the game by setting time scale to 0
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void resetGame()
    {
        if (gameManager != null)
        {
            Time.timeScale = 1f; // Pause the game by setting time scale to 0
            gameManager.resetGame();
        }
    }

    public void mainmenuGame()
    {
        if (gameManager != null)
        {
            Time.timeScale = 1f; // Pause the game by setting time scale to 0
            gameManager.backMainMenu();
        }
    }
}
