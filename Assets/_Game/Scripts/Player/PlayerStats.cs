using UnityEngine;
using System;

[Serializable]
public class PlayerStats
{
    public float moveSpeed = 50f;
    public float maxHealth = 10f;
    public float shootSpeed = 1f;
    public float mainDamage = 5f;
    public float critChance = 0.1f;
    public float critDamageMultiplier = 2f;

    public event Action OnStatsChanged;

    public void ModifyStat(string statName, float value)
    {
        switch (statName)
        {
            case "MoveSpeed": moveSpeed += value; break;
            case "MaxHealth": maxHealth += value; break;
            case "ShootSpeed": shootSpeed += value; break;
            case "MainDamage": mainDamage += value; break;
            case "CritChance": critChance += value; break;
            case "CritDamageMultiplier": critDamageMultiplier += value; break;
            default: Debug.LogWarning($"Stat {statName} not found!"); return;
        }
        OnStatsChanged?.Invoke(); // TODO This will notify listeners of stat changes
    }
}
