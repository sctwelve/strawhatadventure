using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
    {
    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField]  int maxHealth = 100;
    [SerializeField] AudioClip EnemyDeadSFX;
    private bool canMove = true;

    int currentHealth;
    [SerializeField] GameObject PointA1;
    [SerializeField] GameObject PointA2;
    private Transform currentPoint;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();
        currentPoint = PointA2.transform;
        
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (canMove){
            //control enemy pattern
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == PointA2.transform){
                myRigidbody.velocity = new Vector2(moveSpeed,0);
            }else{
                myRigidbody.velocity = new Vector2(-moveSpeed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA2.transform){
                currentPoint = PointA1.transform;
                flipSprites();
            }if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == PointA1.transform){
                currentPoint = PointA2.transform;
                flipSprites();
            }
        }else{
                myRigidbody.velocity = Vector2.zero;
        }


    }

    public void SetCanMove(bool canMoveValue)
    {
        // Update the canMove parameter with the provided value
        canMove = canMoveValue;
    }

    //flip enemy sprites every function called
    private void flipSprites(){
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    //show enemy pattern radius
    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(PointA1.transform.position,0.5f);
        Gizmos.DrawWireSphere(PointA2.transform.position,0.5f);
        Gizmos.DrawLine(PointA1.transform.position, PointA2.transform.position);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(EnemyDeadSFX, Camera.main.transform.position);
        Debug.Log("Enemy Died!");
        Destroy(gameObject);
    }
}
