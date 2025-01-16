using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : AbstractSpawner
{
    [SerializeField] private GameObject _prefab;

    public int SpawnTargetAmount { get; private set; }

    protected override void StartSpawn(DifficultySettings difficultySettings)
    {
        base.StartSpawn(difficultySettings);
        SpawnTargetAmount = 0;
    }

    protected override IEnumerator SpawnTarget()
    {
        int spawnCount = 500;
        float minDistance = DifficultySettings.minDistanceBetweenTargets;
        Vector3 spawnRange = DifficultySettings.spawnRange;

        while (spawnCount > 0)
        {
            Vector3 newPos = Vector3.zero;
            bool isValidPosition = false;

            while (!isValidPosition)
            {
                newPos = new Vector3(
                    transform.position.x + Random.Range(-spawnRange.x, spawnRange.x),
                    transform.position.y + Random.Range(-spawnRange.y, spawnRange.y),
                    transform.position.z + Random.Range(-spawnRange.z, spawnRange.z)
                );

                isValidPosition = true;
                foreach (Transform child in Contaner)
                {
                    if (child.gameObject.activeSelf && Vector3.Distance(child.position, newPos) < minDistance)
                    {
                        isValidPosition = false;
                        break;
                    }
                }
            }

            Target target = Instantiate(_prefab, newPos, Quaternion.identity, Contaner).GetComponent<Target>();
            SpawnTargetAmount++;
            target.Init(RecordCounter);
            spawnCount--;
            yield return new WaitForSeconds(DifficultySettings.spawnDelay);
        }
    }
}