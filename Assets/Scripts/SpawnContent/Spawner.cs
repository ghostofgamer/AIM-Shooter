using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _contaner;
    [SerializeField] private DifficultySettings _difficultySettings;
    [SerializeField] private StartGame _startGame;
    [SerializeField]private RecordCounter _recordCounter;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _startGame.GameStarting += StopSpawn;
        _startGame.GameStarted += StartSpawn;
    }

    private void OnDisable()
    {
        _startGame.GameStarting -= StopSpawn;
        _startGame.GameStarted -= StartSpawn;
    }

    public void StartSpawn(DifficultySettings difficultySettings)
    {
        _difficultySettings = difficultySettings;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SpawnTarget());
    }

    private IEnumerator SpawnTarget()
    {
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

            Target target = Instantiate(_prefab, newPos, Quaternion.identity, _contaner).GetComponent<Target>();
            target.Init(_recordCounter);
            spawnCount--;
            yield return new WaitForSeconds(_difficultySettings.spawnDelay);
        }
    }

    private void StopSpawn()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        foreach (Transform child in _contaner)
            child.gameObject.SetActive(false);
    }
}