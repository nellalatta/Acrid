using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory = new Inventory();
    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>(); // TODO Check this out for other stats accessors too
    }

    public void AddItem(LootItem item, int quantity = 1)
    {
        inventory.AddItem(item, quantity);
        ApplyItemEffects(item, quantity);
    }

    public void RemoveItem(LootItem item, int quantity = 1)
    {
        inventory.RemoveItem(item, quantity);
        ApplyItemEffects(item, -quantity);
    }

    private void ApplyItemEffects(LootItem item, int quantity)
    {
        foreach (var stat in item.statModifiers)
        {
            playerStats.ModifyStat(stat.key, stat.value * quantity);
        }

        if (item.newAbilityPrefab != null)
        {
            //EquipNewAbility(item.newAbilityPrefab); TODO
        }

        if (item.visualEffect != null)
        {
            Instantiate(item.visualEffect, transform.position, Quaternion.identity, transform);
        }
    }
}