using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private HealthBar healthBar;

    private float currentHealth;

    private void Start()
    {
        // if (stats == null) TODO remove or replace with automatically assigning object's components
        // {
        //     stats = GetComponent<PlayerStats>();
        // }

        currentHealth = stats.maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, stats.maxHealth);
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, stats.maxHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyStatModifier(string stat, float value)
    {
        stats.ModifyStat(stat, value);

        if (stat == "MaxHealth")
        {
            currentHealth = stats.maxHealth;
            healthBar.UpdateHealthBar(currentHealth, stats.maxHealth);
        }
    }

    public PlayerStats GetStats()
    {
        return stats;
    }

    public void ModifyHealth(int modifyHealth)
    {
        Debug.Log("Modifying health by " + modifyHealth);
        currentHealth += modifyHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, stats.maxHealth);
        healthBar.UpdateHealthBar(currentHealth, stats.maxHealth);
    }
}
