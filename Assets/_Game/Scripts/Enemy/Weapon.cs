using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject enemyMainProjectile;

    public void MainShoot(Vector3 targetPosition)
    {
        // Calculate direction to target
        Vector2 direction = (targetPosition - firePoint.position).normalized;
        
        // Calculate angle to face target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Instantiate at position and rotation
        Instantiate(enemyMainProjectile, firePoint.position, Quaternion.Euler(0f, 0f, angle));
    }
}
