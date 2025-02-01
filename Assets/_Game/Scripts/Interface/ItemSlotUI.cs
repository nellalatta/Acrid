using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI stackText;

    public void Setup(ItemSlot slot)
    {
        iconImage.sprite = slot.item.itemPrefab.GetComponent<SpriteRenderer>().sprite;
        stackText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
    }
}