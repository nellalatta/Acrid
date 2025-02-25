using UnityEngine;
using System.Collections.Generic;

public class AbilityBarUI : MonoBehaviour
{
    [SerializeField] private PlayerAbilities playerAbilities;
    [SerializeField] private GameObject slotPrefab;

    private Dictionary<Ability, AbilityCooldown> abilityToSlot = new Dictionary<Ability, AbilityCooldown>();

    private void Start()
    {
        var abilities = playerAbilities.GetAbilities();

        foreach (var ability in abilities)
        {
            GameObject slotInstance = Instantiate(slotPrefab, transform);

            var abilityCooldown = slotInstance.GetComponent<AbilityCooldown>();
            abilityCooldown.Initialize(ability);

            abilityToSlot[ability] = abilityCooldown;
        }

        playerAbilities.OnCooldownUpdated += HandleCooldownUpdated;
    }

    private void OnDestroy()
    {
        playerAbilities.OnCooldownUpdated -= HandleCooldownUpdated; // Note: Prevent memory leaks
    }

    private void HandleCooldownUpdated(Ability ability, float timeLeft)
    {
        if (abilityToSlot.TryGetValue(ability, out AbilityCooldown slot))
        {
            slot.UpdateCooldown(timeLeft);
        }
    }
}
