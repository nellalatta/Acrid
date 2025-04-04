using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public float spawnWeight = 1f; // Higher = more likely to spawn
    public int minPackSize = 1;
    public int maxPackSize = 3;
}

public class MobSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<EnemySpawnData> enemyTypes;
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
        if (enemyTypes.Count == 0)
        {
            Debug.LogWarning("No enemy types assigned to spawn");
            return;
        }

        EnemySpawnData chosenEnemy = GetRandomEnemyType();
        int packSize = Random.Range(chosenEnemy.minPackSize, chosenEnemy.maxPackSize + 1);

        List<Vector3> spawnPositions = new List<Vector3>();

        for (int i = 0; i < packSize; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            spawnPositions.Add(spawnPosition);
            GameObject newEnemy = Instantiate(chosenEnemy.enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = GameObject.Find("SpawnedEnemies").transform;
        }

        LogSpawn(chosenEnemy, packSize, spawnPositions);
    }

    private EnemySpawnData GetRandomEnemyType()
    {
        float totalWeight = 0;
        foreach (var enemy in enemyTypes)
        {
            totalWeight += enemy.spawnWeight;
        }

        float randomPoint = Random.Range(0, totalWeight);
        float currentWeight = 0;

        foreach (var enemy in enemyTypes)
        {
            currentWeight += enemy.spawnWeight;
            if (randomPoint <= currentWeight)
            {
                return enemy;
            }
        }

        return enemyTypes[0]; // TODO Should not reach this fallback
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 playerPosition = player.position;
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(spawnDistanceMin, spawnDistanceMax);
        Vector3 spawnPosition = playerPosition + (Vector3)(randomDirection * randomDistance);

        return spawnPosition;
    }

    private void LogSpawn(EnemySpawnData enemy, int packSize, List<Vector3> positions)
    {
        string enemyName = enemy.enemyPrefab.name;
        string positionText = string.Join(", ", positions.ConvertAll(p => $"({p.x:F1}, {p.y:F1})"));
        Debug.Log($"Spawned {packSize}x {enemyName} at: {positionText}");
    }
}
