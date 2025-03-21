using System.Collections;
using UnityEngine;
using Pathfinding;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 5f;
    [SerializeField] protected int resourcesOnDeath = 100;
    
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected GameObject deadBodyPrefab;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BoxCollider2D enemyCollider;

    protected float currentHealth;
    protected ResourceManager resourceManager;
    protected AIPath aiPath;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar?.UpdateHealthBar(maxHealth, maxHealth);

        resourceManager = FindObjectOfType<ResourceManager>();
        aiPath = GetComponent<AIPath>();
    }

    protected virtual void Update()
    {
        UpdateAnimation();
    }

    public virtual void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar?.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        SpawnDeadBody();
        resourceManager?.AddResources(resourcesOnDeath);
        Destroy(gameObject);
    }

    protected void SpawnDeadBody()
    {
        if (deadBodyPrefab != null)
        {
            Instantiate(deadBodyPrefab, transform.position, Quaternion.identity);
        }
    }

    protected abstract void UpdateAnimation();
}
