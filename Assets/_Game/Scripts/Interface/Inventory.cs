using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Inventory
{
    public List<ItemSlot> items = new List<ItemSlot>();

    public void AddItem(LootItem item, int quantity = 1)
    {
        // Check if duplicate item
        foreach (ItemSlot slot in items)
        {
            if (slot.item == item)
            {
                if (slot.quantity + quantity <= item.maxStacks)
                {
                    slot.quantity += quantity;
                    return;
                }
            }
        }

        // If new item
        items.Add(new ItemSlot(item, quantity));
    }

    public void RemoveItem(LootItem item, int quantity = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                items[i].quantity -= quantity;

                if (items[i].quantity <= 0)
                {
                    items.RemoveAt(i);
                }
                return;
            }
        }
    }
}

[Serializable]
public class ItemSlot
{
    public LootItem item;
    public int quantity;

    public ItemSlot(LootItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
