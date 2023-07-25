using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    public Slider slider;

    private void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogWarning("HealthBar: PlayerHealth reference not set!");
        }

        // Get the GameManager instance
        GameManager gameManager = FindObjectOfType<GameManager>();

        // Set the health bar slider values based on the player's health retrieved from GameManager
        SetHealth(gameManager.GetCurrentHealth(), gameManager.GetMaxHealth());
    }

    // Update the health bar UI based on the player's current health
    public void SetHealth(int currentHP, int maxHP)
    {
        if (slider != null)
        {
            slider.value = currentHP;
            slider.maxValue = maxHP;
        }
    }
}
