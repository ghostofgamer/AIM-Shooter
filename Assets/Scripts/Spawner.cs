using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _contaner;
    [SerializeField] private DifficultySettings _difficultySettings;
    
    private void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    private IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(3f);
        int spawnCount = 500;
        float minDistance = _difficultySettings.minDistanceBetweenTargets;
        Vector3 spawnRange = _difficultySettings.spawnRange;

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
                foreach (Transform child in _contaner)
                {
                    if (child.gameObject.activeSelf && Vector3.Distance(child.position, newPos) < minDistance)
                    {
                        isValidPosition = false;
                        break;
                    }
                }
            }

            Instantiate(_prefab, newPos, Quaternion.identity, _contaner);
            spawnCount--;
            yield return new WaitForSeconds(_difficultySettings.spawnDelay);
        }
    }
}