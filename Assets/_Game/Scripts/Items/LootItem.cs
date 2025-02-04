using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Item", menuName = "Loot Item")]
public class LootItem : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public int rarity;
    public int maxStacks = 99;
}
