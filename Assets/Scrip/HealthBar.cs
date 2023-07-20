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
    }

    private void Update()
    {
        if (playerHealth != null && slider != null)
        {
            slider.value = playerHealth.currentHP;
        }
    }

    public void SetHealth(int currentHP, int maxHP)
    {
        if (slider != null)
        {
            slider.value = currentHP;
            slider.maxValue = maxHP;
        }
    }
}
