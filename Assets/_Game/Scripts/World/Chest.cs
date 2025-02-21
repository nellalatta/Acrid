using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite openedChestSprite;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private int chestCost = 100;
    [SerializeField] private GameObject interactText; // Note: slightly better performance to serialize child objects than always doing transform.Find()
    [SerializeField] private GameObject costText;
    [SerializeField] private LootTable lootTable;

    private bool isOpened = false;
    private SpriteRenderer spriteRenderer;
    private ResourceManager resourceManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (interactText != null)
        {
            interactText.SetActive(false);
        }

        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager == null)
        {
            Debug.LogError("ResourceManager not found in the scene.");
        }
    }

    private void Update()
    {
        if (isOpened) return;

        bool playerInRange = IsAnyPlayerInRange();

        // Show the "E to buy" text
        if (interactText != null)
        {
            interactText.SetActive(playerInRange);
        }

        // If player in range and presses "E", open chest
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (isOpened || resourceManager == null) return;

        // Check if there are enough resources
        if (resourceManager.GetResourceCount() >= chestCost)
        {
            resourceManager.RemoveResources(chestCost);
            isOpened = true;

            if (openedChestSprite != null)
            {
                spriteRenderer.sprite = openedChestSprite;
            }

            // Destroy the chest cost & interact text
            if (interactText != null)
            {
                interactText.SetActive(false);
            }
            if (costText != null)
            {
                costText.SetActive(false);
            }

            SpawnLoot();
        }
        else
        {
            Debug.Log("Not enough resources to open the chest!");
        }
    }

    private bool IsAnyPlayerInRange()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= interactionRange)
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnLoot()
    {
        if (lootTable == null)
        {
            Debug.LogError("LootTable not assigned to the chest!");
            return;
        }

        LootItem randomLoot = lootTable.GetRandomLoot();
        if (randomLoot != null && randomLoot.itemPrefab != null)
        {
            Instantiate(randomLoot.itemPrefab, transform.position + Vector3.up, Quaternion.identity);
            Debug.Log($"Spawned loot: {randomLoot.itemName}");
        }
    }
}
