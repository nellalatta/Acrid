using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyWasp : Enemy
{
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackDamage = 2f;
    [SerializeField] private float knockbackForce = 5f;
    
    private Transform player;
    private float attackTimer;

    //private readonly int animFly = Animator.StringToHash("Enemy_Wasp_Fly_Right");

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected override void Update()
    {
        base.Update();
        attackTimer += Time.deltaTime;

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            if (distanceToPlayer <= attackRange && attackTimer >= attackCooldown)
            {
                MeleeAttack();
                attackTimer = 0f;
            }
        }
    }

    private void MeleeAttack()
    {
        if (player.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(attackDamage);
            Vector2 knockbackDirection = (player.position - transform.position).normalized;
            if (player.TryGetComponent<Rigidbody2D>(out Rigidbody2D playerRb))
            {
                playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    protected override void UpdateAnimation()
    {
        // TODO
    }
}
