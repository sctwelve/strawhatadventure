using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : MonoBehaviour
{
     [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;

    
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] AudioClip HitSFX;

     //References
    private Animator anim;
    public Enemy enemy;
    private bool canMove = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //attack only when player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                // Stop the movement when attacking
                canMove = false;

                // Call a method on the attack script to inform it about the movement status
                enemy.SetCanMove(canMove);

                // Attack logic
                cooldownTimer = 0;
                AudioSource.PlayClipAtPoint(HitSFX, Camera.main.transform.position);
                anim.SetTrigger("meeleAttack"); // Mengubah "meeleAttack" menjadi "attackTrigger"
            }
        }
        else
        {
            // Resume movement when the player is not in sight
            canMove = true;
            enemy.SetCanMove(canMove);
        }
    }

    // Fungsi yang akan dipanggil dari animasi untuk memberikan damage ke pemain
    public void DealDamageToPlayer()
    {
        // Temukan pemain dengan tag "Player"
        Collider2D playerCollider = Physics2D.OverlapBox(
            boxCollider2D.bounds.center,
            new Vector2(boxCollider2D.bounds.size.x * attackRange, boxCollider2D.bounds.size.y),
            0,
            playerLayer
        );

        // Jika pemain ada dalam jangkauan sentuhan, berikan damage ke pemain
        if (playerCollider != null)
        {
            // Peroleh komponen PlayerHealth dari pemain
            PlayerHealth playerHealth = playerCollider.GetComponent<PlayerHealth>();

            // Jika ditemukan script PlayerHealth, berikan damage kepada pemain
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    ///Enemy Sight Range
    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z)
            ,0,Vector2.left, 0 ,playerLayer);
        return hit.collider != null;
    }

    /// Draw Enemy Sight on editor
    private void OnDrawGizmos()
    {
        Gizmos.color =  Color.red;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider2D.bounds.size.x * range, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
        


    }
}
