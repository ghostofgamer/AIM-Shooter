using System.Collections;
using UnityEngine;

public abstract class AbstractSpawner : MonoBehaviour
{
    [SerializeField] private Transform _contaner;
    [SerializeField] private StartGame _startGame;
    [SerializeField]private RecordCounter _recordCounter;
    
    private DifficultySettings _difficultySettings;
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
    
    public virtual void StartSpawn(DifficultySettings difficultySettings)
    {
        _difficultySettings = difficultySettings;
        // SpawnTargetAmount = 0;
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SpawnTarget());
    }
    
    public virtual IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(0f);
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