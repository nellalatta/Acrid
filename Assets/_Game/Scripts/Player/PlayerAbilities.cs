using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities = new List<Ability>();
    private Dictionary<Ability, float> cooldowns = new Dictionary<Ability, float>();

    public event System.Action<Ability, float> OnCooldownUpdated; // TODO Will notify UI 

    private void Update()
    {
        UpdateCooldowns();
        HandleAbilityInput();
    }

    private void UpdateCooldowns()
    {
        List<Ability> cooldownKeys = new List<Ability>(cooldowns.Keys);
        foreach (var ability in cooldownKeys)
        {
            cooldowns[ability] -= Time.deltaTime;
            OnCooldownUpdated?.Invoke(ability, Mathf.Max(0, cooldowns[ability])); // TODO Update UI
            if (cooldowns[ability] <= 0)
                cooldowns.Remove(ability);
        }
    }

    private void HandleAbilityInput()
    {
        if (abilities.Count > 0 && Input.GetMouseButtonDown(1))
        {
            UseAbility(abilities[0]);
        }
        if (abilities.Count > 1 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            UseAbility(abilities[1]);
        }
        if (abilities.Count > 2 && Input.GetKeyDown(KeyCode.R))
        {
            UseAbility(abilities[2]);
        }
    }

    public void UseAbility(Ability ability)
    {
        if (!cooldowns.ContainsKey(ability))
        {
            ability.Activate(gameObject);
            cooldowns[ability] = ability.cooldownTime;
            OnCooldownUpdated?.Invoke(ability, ability.cooldownTime); // TODO Notify UI
        }
    }

    public List<Ability> GetAbilities() => abilities;
}
