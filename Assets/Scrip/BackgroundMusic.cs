using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic backgroundMusic;

    private void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Di sini kita menghentikan musik saat berpindah scene.
        // Anda dapat menyesuaikan kode ini sesuai dengan kebutuhan pemutaran musik Anda.
        // Sebagai contoh, jika Anda menggunakan AudioSource, cukup panggil Stop() pada AudioSource.
        // Jika Anda menggunakan AudioManager atau sistem pemutaran musik lainnya, gunakan metode yang sesuai.

        // Stop musik saat berpindah scene.
        StopMusic();
    }

    private void StopMusic()
    {
        // Implementasikan logika untuk menghentikan musik di sini.
        // Misalnya, jika Anda menggunakan AudioSource, caranya seperti ini:
        // GetComponent<AudioSource>().Stop();
    }
}
