using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] private int maxHP = 100;
    public HealthBar healthBar;
    private GameManager gameManager;
    public int currentHP;

    void Start()
    {
        // Find the GameManager in the scene and store a reference to it
        gameManager = FindObjectOfType<GameManager>();

        // Rest of your code...
        currentHP = gameManager.GetCurrentHealth();
        UpdateHealthBar(currentHP);
    }

    // Fungsi untuk menerima kerusakan (damage) dari player
    public void TakeDamage(float damage)
    {
        // Mengurangi jumlah HP saat ini dengan jumlah kerusakan yang diterima
        currentHP -= Mathf.RoundToInt(damage);

        // Memastikan nilai HP saat ini tidak kurang dari 0 dan tidak lebih besar dari HP maksimum
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        // Memperbarui tampilan health bar
        UpdateHealthBar(currentHP);

        // Memeriksa apakah HP saat ini sudah habis (kurang dari atau sama dengan 0)
        if (currentHP <= 0)
        {
            // Panggil fungsi Die() jika HP habis untuk menangani kematian player
            Die();
        }
    }

    // Fungsi untuk memperbarui tampilan health bar
    private void UpdateHealthBar(int nowHP)
    {
        if (healthBar != null)
        {
            // Mengatur nilai health bar berdasarkan HP saat ini dan HP maksimum
            gameManager.UpdatePlayerHP(nowHP);

            healthBar.SetHealth(nowHP, maxHP);
        }
    }

    // Fungsi untuk menangani kematian player
    private void Die()
    {
        // Tambahkan perilaku saat player mati di sini, misalnya efek visual atau perubahan permainan
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
                gameManager.PlayerDeath();
        }
    }
}
