using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Aim_Indicator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distanceFromPlayer = 1f;
    [SerializeField] private GameObject mainProjectile;
    [SerializeField] private Transform mainProjectileSpawnPoint;
    [SerializeField] private PlayerStats playerStats;

    private GameObject projectileInstance;
    private float angle;
    private float lastShotTime = 0f;

    void Update()
    {
        if (PauseMenu.gamePaused) return;

        UpdatePositionAndRotation();
        HandleMainShooting();
    }

    private void UpdatePositionAndRotation()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z-axis is 0 since 2D

        // Calculate direction from player to mouse position
        Vector3 direction = (mousePosition - player.position).normalized;
        
        transform.position = player.position + direction * distanceFromPlayer;

        // Rotate indicator to face the direction
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }

    private void HandleMainShooting()
    {
        if (Mouse.current.leftButton.isPressed && CanShoot())
        {
            projectileInstance = Instantiate(mainProjectile, mainProjectileSpawnPoint.position, Quaternion.Euler(0f, 0f, angle));
            lastShotTime = Time.time;
        }
    }

    private bool CanShoot()
    {
        return Time.time - lastShotTime >= playerStats.primaryCooldown;
    }
}
