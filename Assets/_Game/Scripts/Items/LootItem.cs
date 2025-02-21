using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Loot Item", menuName = "Loot Item")]
public class LootItem : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public GameObject itemPrefab;
    [SerializeField] public int rarity;
    [SerializeField] public int maxStacks = 99;

    public List<StatModifier> statModifiers = new List<StatModifier>
    {
        new StatModifier { key = "MoveSpeed", value = 0f },
        new StatModifier { key = "MaxHealth", value = 0f },
        new StatModifier { key = "MainDamage", value = 0f },
        new StatModifier { key = "CritChance", value = 0f },
        new StatModifier { key = "CritDamageMultiplier", value = 0f },
        new StatModifier { key = "PrimaryCooldown", value = 0f }
    };

    [SerializeField] public int modifyHealth = 0;

    [SerializeField] public GameObject visualEffect;

    [SerializeField] public GameObject newAbilityPrefab;
}

[System.Serializable]
public class StatModifier
{
    public string key;
    public float value;
}
