using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentHP;
    [SerializeField] public int maxHealth;
    [SerializeField] public int currentKey;
    public GameObject pauseCanvas;
    public GameObject deathCanvas;
    private bool isPaused = false;

    private static GameManager instance;
    
    public AudioSource BGmusicForMainMenuSource; // AudioSource untuk Scene 0
    public AudioSource BGmusicForMainGameSource; // AudioSource untuk Scene 1
    public AudioSource BGmusicForStageSandCaveSource; // AudioSource untuk Scene 2
    public AudioSource BGmusicForCreditSceneSource; // AudioSource untuk Scene 3
    public Dictionary<int, AudioSource> sceneMusicMap;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        sceneMusicMap = new Dictionary<int, AudioSource>
        {
            { 0, BGmusicForMainMenuSource },
            { 1, BGmusicForMainGameSource },
            { 2, BGmusicForStageSandCaveSource },
            { 3, BGmusicForCreditSceneSource },

        };

        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBGMusic(SceneManager.GetActiveScene().buildIndex);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                TogglePause();
            }
        }
    }

    private void TogglePause()
    {
        // Check if the death canvas is active
        bool isDeathCanvasActive = deathCanvas.activeSelf;

        // Check if the game can be paused (death canvas is not active)
        if (!isDeathCanvasActive)
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f; // Pause the game by setting time scale to 0
                pauseCanvas.SetActive(true); // Show the pause canvas
            }
            else
            {
                Time.timeScale = 1f; // Resume the game by setting time scale back to 1
                pauseCanvas.SetActive(false); // Hide the pause canvas
            }
        }
    }

    public void AddKeys(int keyPicked){
      currentKey += keyPicked;
    }
    public int GetCurrentKey(){
      return currentKey;
    }

    public void DecreaseKeys()
    {
        currentKey -= 10;
    }
    
    private void PlayBGMusic(int sceneIndex)
    {
        if (sceneMusicMap.ContainsKey(sceneIndex))
        {
            AudioSource audioSource = sceneMusicMap[sceneIndex];
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
    
    private void StopBGMusic()
    {
        foreach (var audioSource in sceneMusicMap.Values)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
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
        int sceneIndex = scene.buildIndex;
        StopBGMusic();
        PlayBGMusic(sceneIndex);
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    public void UpdatePlayerHP(int playerHP)
    {
        currentHP = playerHP;
        Debug.Log(currentHP);
    }

    public void PlayerDeath(){
      Time.timeScale = 0f; // Pause the game by setting time scale to 0
      deathCanvas.SetActive(true); // Show the pause canvas
    //   SceneManager.LoadScene(0);
    //   Destroy(gameObject);      
    }

    public void resetGame()
    {
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }    
    public void backMainMenu()
    {
       SceneManager.LoadScene(0);
       Destroy(gameObject);
    }

    public int GetCurrentHealth(){
      return currentHP;
    }

    public int GetMaxHealth(){
      return maxHealth;
    }

    public int GetKeys(){
        return currentKey;
    }
    public static GameManager GetInstance()
    {
        return instance;
    }
}
