using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Loot Item", menuName = "Loot Item")]
public class LootItem : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public int rarity;
    public int maxStacks = 99;

    public List<StatModifier> statModifiers = new List<StatModifier>
    {
        new StatModifier { key = "MoveSpeed", value = 0f },
        new StatModifier { key = "MaxHealth", value = 0f },
        new StatModifier { key = "ShootSpeed", value = 0f },
        new StatModifier { key = "MainDamage", value = 0f },
        new StatModifier { key = "CritChance", value = 0f },
        new StatModifier { key = "CritDamageMultiplier", value = 0f }
    };

    public GameObject visualEffect;

    public GameObject newAbilityPrefab;
}

[System.Serializable]
public class StatModifier
{
    public string key;
    public float value;
}
