using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int resourceCount = 0;
    [SerializeField] private TextMeshProUGUI resourceText;

    void Start()
    {
        UpdateResourceDisplay();
    }

    public void AddResources(int resourceAmount) {
        resourceCount += resourceAmount;
        UpdateResourceDisplay();
    }

    public void RemoveResources(int amount)
    {
        if (resourceCount >= amount)
        {
            resourceCount -= amount;
            UpdateResourceDisplay();
        }
    }

    public int GetResourceCount()
    {
        return resourceCount;
    }

    private void UpdateResourceDisplay() {
        resourceText.text = "Tech Gathered: " + resourceCount.ToString();
    }
}
