using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]private TMP_Text _timerText; 
    [SerializeField] private StartGame _startGame;
    [SerializeField]private RecordCounter _recordCounter;
    
    private bool _isTimerActive = false;
    private float _currentTime = 10f;
    
    public event Action GameEnded ;
    
    private void OnEnable()
    {
        // _startGame.GameStarting += StopSpawn;
        _startGame.GameStarted += StartTimer;
    }

    private void OnDisable()
    {
        // _startGame.GameStarting -= StopSpawn;
        _startGame.GameStarted -= StartTimer;
    }
    
    private void Start()
    {
        _timerText.text = "00:00";
        // StartTimer();
    }
    
    private void StartTimer(DifficultySettings  difficultySettings)   
    {
        _isTimerActive = true;
        InvokeRepeating(nameof(UpdateTimer), 0f, 1f);
    }

    private void UpdateTimer()
    {
        if (_isTimerActive)
        {
            _currentTime -= 1f;
            
            if (_currentTime <= 0f)
            {
                _currentTime = 0f;
                _isTimerActive = false;
                CancelInvoke(nameof(UpdateTimer));
                GameEnded?.Invoke();
            }
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);
        _timerText.text = string.Format("{0:00}:{1:00} ", minutes, seconds);
    }
}
