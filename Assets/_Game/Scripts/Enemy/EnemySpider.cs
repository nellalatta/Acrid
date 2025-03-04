using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float shootingRange = 5f;
    [SerializeField] private float shootCooldown = 2f;
    [SerializeField] private int resourcesOnDeath = 100;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D enemyCollider;
    // [SerializeField] private float moveSpeed = 2f;

    private enum Directions { LEFT, RIGHT }
    private float currentHealth;
    private Transform player;
    private ResourceManager resourceManager;
    private Weapon weapon;
    private float shootTimer;
    private Vector2 moveDir = Vector2.zero;
    private Directions facingDirection = Directions.RIGHT;
    

    private readonly int animMove = Animator.StringToHash("Enemy_Spider_Walk_Right");
    private readonly int animIdle = Animator.StringToHash("Enemy_Spider_Idle_Right");

    private Vector2 colliderOffsetRight;
    private Vector2 colliderOffsetLeft => new Vector2(-colliderOffsetRight.x, colliderOffsetRight.y);

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, maxHealth);

        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        weapon = GetComponent<Weapon>(); 

        resourceManager = FindObjectOfType<ResourceManager>();
        colliderOffsetRight = enemyCollider.offset;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            moveDir = (player.position - transform.position).normalized;
            CalculateFacingDirection();
            UpdateAnimation();

            // if (distanceToPlayer > shootingRange / 2)
            // {
            //     MoveTowardsPlayer();
            // }

            // Shoot if within shooting range and cooldown is ready
            if (distanceToPlayer <= shootingRange && shootTimer >= shootCooldown)
            {
                weapon.MainShoot(player.position);
                shootTimer = 0f;
            }
        }
    }

    private void CalculateFacingDirection()
    {
        if (moveDir.x != 0)
        {
            facingDirection = moveDir.x > 0 ? Directions.RIGHT : Directions.LEFT;
        }
    }

    private void UpdateAnimation()
    {
        if (facingDirection == Directions.LEFT)
        {
            spriteRenderer.flipX = true;
            enemyCollider.offset = colliderOffsetLeft;
        }
        else if (facingDirection == Directions.RIGHT)
        {
            spriteRenderer.flipX = false;
            enemyCollider.offset = colliderOffsetRight;
        }

        // TODO: This isn't working with astar pathing
        if (moveDir.sqrMagnitude > 0)
        {
            animator.CrossFade(animMove, 0);
        }
        else
        {
            animator.CrossFade(animIdle, 0);
        }
    }

    // private void MoveTowardsPlayer()
    // {
    //     // Calculate direction to the player and move toward them
    //     Vector2 direction = (player.position - transform.position).normalized;
    //     transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    // }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            resourceManager.AddResources(resourcesOnDeath);
        }
    }

}
