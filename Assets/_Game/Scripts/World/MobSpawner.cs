using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int minPackSize = 1;
    [SerializeField] private int maxPackSize = 3;
    [SerializeField] private float spawnDistanceMin = 10f;
    [SerializeField] private float spawnDistanceMax = 20f;
    [SerializeField] private float spawnInterval = 5f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnMobsRoutine());
    }

    private IEnumerator SpawnMobsRoutine()
    {
        while (true)
        {
            SpawnMobPack();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMobPack()
    {
        int packSize = Random.Range(minPackSize, maxPackSize + 1);

        for (int i = 0; i < packSize; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = GameObject.Find("SpawnedEnemies").transform;
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(spawnDistanceMin, spawnDistanceMax);
        Vector3 spawnPosition = playerPosition + (Vector3)(randomDirection * randomDistance);

        return spawnPosition;
    }
}
