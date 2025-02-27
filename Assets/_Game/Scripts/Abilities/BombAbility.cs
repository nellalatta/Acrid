using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Bomb")]
public class BombAbility : Ability
{
    public GameObject bombPrefab;

    public float bombSpeed = 10f;
    public float explosionRadius = 2f;
    public float explosionDamage = 2f;
    public LayerMask damageableLayers;

    public override void Activate(GameObject player)
    {
        Player_Aim_Indicator aim = player.GetComponentInChildren<Player_Aim_Indicator>();
        Transform spawnPoint = aim.mainProjectileSpawnPoint;

        GameObject bomb = Instantiate(bombPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = -spawnPoint.up * bombSpeed;
        }

        BombBehaviour bombScript = bomb.GetComponent<BombBehaviour>();
        if (bombScript != null)
        {
            bombScript.Initialize(explosionRadius, explosionDamage, damageableLayers);
        }
    }
}
