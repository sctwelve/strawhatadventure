using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
    {
    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField]  int maxHealth = 100;

    public Animator animator;
    int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died!");

        animator.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

       
    }
}
