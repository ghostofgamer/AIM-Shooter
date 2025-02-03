using System.Collections;
using EnvironmentContent;
using StartGameContent;
using StatisticContent;
using UnityEngine;

namespace SpawnContent
{
    public abstract class AbstractSpawner : MonoBehaviour
    {
        [SerializeField] protected Transform Contaner;
        [SerializeField] protected RecordCounter RecordCounter;
        [SerializeField] private StopGameButton _stopGameButton;
        [SerializeField] private StartGame _startGame;

        protected DifficultySettings DifficultySettings;
        protected bool IsWork;
    
        private Coroutine _coroutine;
    
        protected virtual void OnEnable()
        {
            _stopGameButton.Stoping += StopSpawn;
            _startGame.GameStarting += StopSpawn;
            _startGame.GameStarted += StartSpawn;
        }

        protected virtual void OnDisable()
        {
            _stopGameButton.Stoping -= StopSpawn;
            _startGame.GameStarting -= StopSpawn;
            _startGame.GameStarted -= StartSpawn;
        }

        protected virtual void StartSpawn(DifficultySettings difficultySettings)
        {
            DifficultySettings = difficultySettings;
            IsWork = true;
        
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(SpawnTarget());
        }

        protected virtual IEnumerator SpawnTarget()
        {
            yield return null;
        }

        protected void StopSpawn()
        {
            IsWork = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            foreach (Transform child in Contaner)
                child.gameObject.SetActive(false);
        }
    }
}