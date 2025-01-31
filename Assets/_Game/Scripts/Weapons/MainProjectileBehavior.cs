using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Make extractable for enemy projectiles script like EnemyMainProjectile
public class MainProjectileBehavior : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float normalBulletDamage = 1f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;

    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Is the collision within the whatDestroysBullet layerMask
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            // TODO: Add any other hit effects like spawn particles, SFX, screen shake

            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            if (iDamageable != null)
            {
                iDamageable.Damage(normalBulletDamage);
            }

            Destroy(gameObject, 0.05f); // Added a delay time to counteract the illusion of the bullet disappearing early
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDestroyTime();

        SetStraightVelocity();
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * normalBulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
}
