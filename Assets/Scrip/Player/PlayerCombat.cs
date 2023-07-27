using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackpoint;

    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public int attackDamage = 40;
    [SerializeField] AudioClip Hit1SFX;
    [SerializeField] AudioClip HitS2FX;

    public float specialAttackCooldown = 5f;
    float nextSpecialAttackTime = 0f;
    public int specialAttackDamage = 100;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextSpecialAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SpecialAttack();
                nextSpecialAttackTime = Time.time + specialAttackCooldown;
            }
        }
    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");
        AudioSource.PlayClipAtPoint(Hit1SFX, Camera.main.transform.position);

        // Detect enemies within the range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void SpecialAttack()
    {
        // Play a special attack animation
        animator.SetTrigger("SpecialAttack");
        AudioSource.PlayClipAtPoint(HitS2FX, Camera.main.transform.position);

        // Detect enemies within the range of special attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

        // Damage them with special attack damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(specialAttackDamage);
        }
    }

    //Check Player Range in action
    private void OnDrawGizmosSelected()
    {
        Gizmos.color =  Color.red;
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
