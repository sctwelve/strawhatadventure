using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{
     [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    
    [Header("Ranged Attack Parameters")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] shadowMagic;
    
    
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    
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
                anim.SetTrigger("rangedAttack");

                // Call the RangedAttack method to launch the projectile
                RangedAttack();
            }
        }
        else
        {
            // Resume movement when the player is not in sight
            canMove = true;
            enemy.SetCanMove(canMove);
        }
    }


    //Setup Enemy projectile
    private void RangedAttack(){
        cooldownTimer = 0;
        shadowMagic[FindFireball()].transform.position = firepoint.position;
        shadowMagic[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }

    private int FindFireball()
    {
        for (int i = 0; i < shadowMagic.Length; i++)
        {
            if (!shadowMagic[i].activeInHierarchy)
                return i;
        }
        return 0;
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
