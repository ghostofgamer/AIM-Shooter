using System.Collections;
using UnityEngine;

public abstract class AbstractSpawner : MonoBehaviour
{
    [SerializeField] protected Transform Contaner;
    [SerializeField] protected RecordCounter RecordCounter;
    [SerializeField] private StopGameButton _stopGameButton;
    [SerializeField] private StartGame _startGame;

    protected DifficultySettings DifficultySettings;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _stopGameButton.Stoping += StopSpawn;
        _startGame.GameStarting += StopSpawn;
        _startGame.GameStarted += StartSpawn;
    }

    private void OnDisable()
    {
        _stopGameButton.Stoping -= StopSpawn;
        _startGame.GameStarting -= StopSpawn;
        _startGame.GameStarted -= StartSpawn;
    }

    protected virtual void StartSpawn(DifficultySettings difficultySettings)
    {
        DifficultySettings = difficultySettings;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SpawnTarget());
    }

    protected virtual IEnumerator SpawnTarget()
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

        foreach (Transform child in Contaner)
            child.gameObject.SetActive(false);
    }
}