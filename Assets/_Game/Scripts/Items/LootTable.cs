using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public List<LootItem> lootItems;

    public LootItem GetRandomLoot()
    {
        // Calculate the total weight
        int totalWeight = 0;
        foreach (LootItem item in lootItems)
        {
            totalWeight += item.rarity;
        }

        int randomRoll = Random.Range(0, totalWeight);

        // Find which item the random roll falls into
        int currentWeight = 0;
        foreach (LootItem item in lootItems)
        {
            currentWeight += item.rarity;
            if (randomRoll < currentWeight)
            {
                return item;
            }
        }

        return null;
    }
}
