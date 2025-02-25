using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Bomb")]
public class BombAbility : Ability
{
    public GameObject bombPrefab;

    public override void Activate(GameObject player) // TODO Test implementation
    {
        Player_Aim_Indicator aim = player.GetComponent<Player_Aim_Indicator>();
        if (aim != null)
        {
            Instantiate(bombPrefab, aim.mainProjectileSpawnPoint.position, aim.mainProjectileSpawnPoint.rotation);
        }
        else
        {
            Instantiate(bombPrefab, player.transform.position, player.transform.rotation);
        }
    }
}
