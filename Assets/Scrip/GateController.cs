using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public int requiredKeys = 10;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get a reference to the player's key count script
            GameManager gameManager = FindObjectOfType<GameManager>();

            // Check if the GameManager exists and if the player has enough keys to open the door
            if (gameManager != null && gameManager.GetCurrentKey() >= requiredKeys)
            {
                Debug.Log("Door Opened!");
                gameManager.DecreaseKeys();
                // Load the new scene "Scene2"
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("Not enough keys to open the door.");
            }
        }
    }
}
