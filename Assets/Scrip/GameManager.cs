using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentHP;
    [SerializeField] public int maxHealth;
    private static GameManager instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Set this instance as the GameManager and mark it as not to be destroyed on scene change.
        instance = this;
        DontDestroyOnLoad(gameObject);
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
      SceneManager.LoadScene(0);
      Destroy(gameObject);      
    }

    public int GetCurrentHealth(){
      return currentHP;
    }

    public int GetMaxHealth(){
      return maxHealth;
    }
}
