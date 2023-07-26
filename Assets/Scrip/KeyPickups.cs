using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickups : MonoBehaviour
{

    [SerializeField] AudioClip KeyPickupSFX;
    [SerializeField] int keyPickedNumber = 1;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){
            FindObjectOfType<GameManager>().AddKeys(keyPickedNumber);
            AudioSource.PlayClipAtPoint(KeyPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
