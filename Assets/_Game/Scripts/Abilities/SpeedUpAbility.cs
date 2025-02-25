using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Speed Up")]
public class SpeedUpAbility : Ability
{
    public float speedIncrease = 50f;
    public float duration = 5f;

    public override void Activate(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats == null) return;

        stats.moveSpeed += speedIncrease;

        PlayerAbilities abilitiesManager = player.GetComponent<PlayerAbilities>();
        if (abilitiesManager != null)
        {
            abilitiesManager.StartCoroutine(RemoveBoostAfterTime(stats, speedIncrease, duration)); // Note: Coroutine prevents the game from pausing while waiting for ability to end
        }
    }

    private IEnumerator RemoveBoostAfterTime(PlayerStats stats, float amount, float waitTime) // Note: IEnumerator type used to return coroutines
    {
        yield return new WaitForSeconds(waitTime);
        stats.moveSpeed -= amount;
    }
}
