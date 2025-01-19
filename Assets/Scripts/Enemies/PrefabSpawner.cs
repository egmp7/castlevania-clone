using egmp7.Game.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private BaseEnemy Chuck; // Weak enemy
    [SerializeField] private BaseEnemy StrongEnemy1; // Mid-level enemy
    [SerializeField] private BaseEnemy StrongEnemy2; // Strong enemy
    [SerializeField] private List<Transform> spawnLocations = new(); // Possible spawn points
    [SerializeField] private float spawnInterval = 3f; // Interval between spawns
    [SerializeField] private int maxEnemies = 10; // Maximum number of enemies

    private int currentRound = 1; // Tracks the current round
    private bool spawning = true;

    private void Start()
    {
        // Validate enemy references
        if (Chuck == null) ErrorManager.LogMissingProperty<BaseEnemy>(gameObject);
        if (StrongEnemy1 == null) ErrorManager.LogMissingProperty<BaseEnemy>(gameObject);
        if (StrongEnemy2 == null) ErrorManager.LogMissingProperty<BaseEnemy>(gameObject);

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            // Wait until all current enemies are destroyed before starting the next round
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return null;
            }

            // Spawn enemies for the current round
            int enemiesToSpawn = Mathf.Min(currentRound, maxEnemies);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (spawnLocations.Count == 0) break;

                Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
                SpawnEnemyBasedOnDifficulty(spawnLocation);
            }

            // Increment round
            currentRound++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemyBasedOnDifficulty(Transform spawnLocation)
    {
        // Determine enemy type based on the current round
        BaseEnemy enemyToSpawn;
        if (currentRound <= 3) // Early rounds, spawn weaker enemies
        {
            enemyToSpawn = Chuck;
        }
        else if (currentRound <= 6) // Mid-level rounds
        {
            enemyToSpawn = Random.Range(0, 2) == 0 ? Chuck : StrongEnemy1;
        }
        else // Later rounds, mix stronger enemies
        {
            int choice = Random.Range(0, 3);
            enemyToSpawn = choice == 0 ? Chuck : (choice == 1 ? StrongEnemy1 : StrongEnemy2);
        }

        // Instantiate the selected enemy at the spawn location
        enemyToSpawn.Copy(spawnLocation);
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    public void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }
}
