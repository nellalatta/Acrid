using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private GameObject itemSlotPrefab;

    private PlayerInventory playerInventory;

    private void Start()
    {
        inventoryPanel.SetActive(false); // Hide inventory initially
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            if (inventoryPanel.activeSelf)
            {
                // TODO: Maybe change update to just be on item pickup/drop for performance? Maybe this way is best in case removes/adds are missed
                UpdateInventoryDisplay();
            }
        }
    }

    private void UpdateInventoryDisplay()
    {
        // Clear old inventory slots
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        // Populate UI with current inventory
        foreach (ItemSlot slot in playerInventory.inventory.items)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemSlotContainer);
            itemSlot.GetComponent<ItemSlotUI>().Setup(slot);
        }
    }
}