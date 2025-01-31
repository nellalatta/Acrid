using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite openedChestSprite;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private int chestCost = 100;
    [SerializeField] private GameObject interactTextPrefab;
    [SerializeField] private LootTable lootTable;

    private bool isOpened = false;
    private GameObject interactTextTMP;
    private SpriteRenderer spriteRenderer;
    private ResourceManager resourceManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Create the "E to buy" text but make it inactive initially
        if (interactTextPrefab != null)
        {
            interactTextTMP = Instantiate(interactTextPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            interactTextTMP.SetActive(false);
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

        if (IsAnyPlayerInRange())
        {
            if (interactTextTMP != null)
            {
                interactTextTMP.SetActive(true); // Show the "E to buy" text
            }

            // Check if a player presses E
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
                    if (distanceToPlayer <= interactionRange)
                    {
                        Interact();
                        break;
                    }
                }
            }
        }
        else
        {
            if (interactTextTMP != null)
            {
                interactTextTMP.SetActive(false); // Hide the "E to buy" text
            }
        }
    }

    public void Interact()
    {
        if (isOpened || resourceManager == null) return;

        // Check if there are enough resources
        if (resourceManager.GetResourceCount() >= chestCost)
        {
            // Deduct the cost
            resourceManager.RemoveResources(chestCost);

            // Open the chest
            isOpened = true;

            if (openedChestSprite != null)
            {
                spriteRenderer.sprite = openedChestSprite;
            }

            if (interactTextTMP != null)
            {
                Destroy(interactTextTMP.gameObject);
            }

            // Destroy ChestCost child object
            Transform chestCostText = transform.Find("ChestCost");
            if (chestCostText != null)
            {
                Destroy(chestCostText.gameObject);
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
