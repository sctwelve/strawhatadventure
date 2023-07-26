using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeySavedBar : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI keyText;

    private void Update()
    {
                GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                // Retrieve the current key from the GameManager and display it in the keyText.
                int currentKeys = gameManager.GetCurrentKey();
                keyText.text = currentKeys.ToString() + "/10";
            }
    }

}
