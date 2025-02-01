using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory = new Inventory();

    public void AddItem(LootItem item, int quantity = 1)
    {
        inventory.AddItem(item, quantity);
    }

    public void RemoveItem(LootItem item, int quantity = 1)
    {
        inventory.RemoveItem(item, quantity);
    }
}