using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float shootingRange = 5f;
    [SerializeField] private float shootCooldown = 2f;
    // [SerializeField] private float moveSpeed = 2f;
    
    private float currentHealth;
    private Transform player;
    private ResourceManager resourceManager;
    private Weapon weapon;
    private float shootTimer;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, maxHealth);

        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        weapon = GetComponent<Weapon>(); 

        resourceManager = FindObjectOfType<ResourceManager>();
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

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
            resourceManager.AddResources(15);
        }
    }

}
