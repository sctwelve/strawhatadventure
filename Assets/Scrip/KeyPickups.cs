using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickups : MonoBehaviour
{

    [SerializeField] AudioClip KeyPickupSFX;
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){
            AudioSource.PlayClipAtPoint(KeyPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
