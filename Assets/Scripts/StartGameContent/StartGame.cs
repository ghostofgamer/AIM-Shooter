using System;
using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(4f);
    private Coroutine _coroutine;
    
    public event Action GameStarting;

    public event Action<DifficultySettings> GameStarted;
    
    public event Action Started;
    
    public int Difficulty{ get; private set; }

    public void Play(DifficultySettings difficultySettings)
    {
        Difficulty = difficultySettings.Difficulty;
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartAimGame(difficultySettings));
    }

    private IEnumerator StartAimGame(DifficultySettings difficultySettings)
    {
        GameStarting?.Invoke();
        yield return _waitForSeconds;
        GameStarted?.Invoke(difficultySettings);
        Started?.Invoke();
    }
}