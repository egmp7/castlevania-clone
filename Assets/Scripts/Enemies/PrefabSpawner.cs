using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using egmp7.Game.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private BaseEnemy Chuck;
    [SerializeField] private List<Transform> spawnsLocations = new();
    [SerializeField] private float spawnInterval = 3f;

    private bool spawning = true; // Control whether spawning is active

    private void Start()
    {
        if (Chuck == null) ErrorManager.LogMissingProperty<BaseEnemy>(gameObject);

        // Start the spawning coroutine
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 5)
            {
                foreach (Transform spawnLocation in spawnsLocations)
                {
                    Debug.Log($"Spawn location: {spawnLocation.position}");
                    Chuck.Copy(spawnLocation);
                }
            }
                // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Call this to stop spawning
    public void StopSpawning()
    {
        spawning = false;
    }

    // Call this to start spawning again
    public void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }
}
