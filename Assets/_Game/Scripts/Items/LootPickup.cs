using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour
{
    [SerializeField] public LootItem lootItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddItem(lootItem);
            }

            if (player != null)
            {
                player.ModifyHealth(lootItem.modifyHealth);
            }

            Destroy(gameObject);
        }
    }
}