using System.Collections;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    private float explosionRadius;
    private float explosionDamage;
    private LayerMask damageableLayers;

    [SerializeField] private float destroyTime = 3f;

    private Rigidbody2D rb;

    public void Initialize(float radius, float damage, LayerMask layers)
    {
        explosionRadius = radius;
        explosionDamage = damage;
        damageableLayers = layers;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((damageableLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayers);

        foreach (Collider2D hit in hitObjects)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(explosionDamage);
            }
        }
        
        // TODO Add Explosion Effect

        Destroy(gameObject);
    }
}
