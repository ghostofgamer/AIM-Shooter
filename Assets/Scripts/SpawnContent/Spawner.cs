using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : AbstractSpawner
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _maxSize;
    
    public int SpawnTargetAmount { get; private set; }
    
   
    private ObjectPool<Target> _objectPool;

    private void Start()
    {
        _objectPool = new ObjectPool<Target>(_prefab.GetComponent<Target>(),_maxSize, Contaner);
        _objectPool.EnableAutoExpand();
    }

    protected override void StartSpawn(DifficultySettings difficultySettings)
    {
        base.StartSpawn(difficultySettings);
        SpawnTargetAmount = 0;
    }

    protected override IEnumerator SpawnTarget()
    {
        int spawnCount = 99999999;
        float minDistance = DifficultySettings.minDistanceBetweenTargets;
        Vector3 spawnRange = DifficultySettings.spawnRange;

        while (IsWork)
        {
            // Debug.Log("Spawning");
            
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

            if (_objectPool.TryGetObject(out Target target, _prefab.GetComponent<Target>()))
            {
                target.gameObject.SetActive(true);
                target.transform.position = newPos;
                SpawnTargetAmount++;
                target.Init(RecordCounter);
                spawnCount--;
            }
            
            // Target target = Instantiate(_prefab, newPos, Quaternion.identity, Contaner).GetComponent<Target>();
            /*SpawnTargetAmount++;
            target.Init(RecordCounter);
            spawnCount--;*/
            yield return new WaitForSeconds(DifficultySettings.spawnDelay);
        }
    }
}