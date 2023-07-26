using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public string menuSceneName = "Start Scene";
    public void BackMenu()
    {
        
        Debug.Log("Back to Menu");
        SceneManager.LoadScene(menuSceneName);
    }

}
