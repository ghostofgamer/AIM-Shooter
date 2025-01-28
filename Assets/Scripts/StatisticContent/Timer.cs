using System;
using EnvironmentContent;
using StartGameContent;
using TMPro;
using UnityEngine;

namespace StatisticContent
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private StartGame _startGame;
        [SerializeField] private RecordCounter _recordCounter;
        [SerializeField]private StopGameButton _stopGameButton;

        private bool _isTimerActive = false;
        private float _currentTime = 60f;
        private float _defaultValue = 60f;
        private float _minutes;
        private float _seconds;
        
        public event Action GameEnded;

        private void OnEnable()
        {
            _stopGameButton.Stoping += StopTimer;
            _startGame.GameStarting += StopTimer;
            _startGame.GameStarted += StartTimer;
        }

        private void OnDisable()
        {
            _stopGameButton.Stoping += StopTimer;
            _startGame.GameStarting -= StopTimer;
            _startGame.GameStarted -= StartTimer;
        }

        private void Start()
        {
            _timerText.text = "00:00";
        }

        private void StartTimer(DifficultySettings difficultySettings)
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
            _minutes = Mathf.FloorToInt(_currentTime / _defaultValue);
            _seconds = Mathf.FloorToInt(_currentTime % _defaultValue);
            _timerText.text = string.Format("{0:00}:{1:00} ", _minutes, _seconds);
        }

        private void StopTimer()
        {
            _isTimerActive = false;
            CancelInvoke(nameof(UpdateTimer));
            _currentTime = _defaultValue;
            _timerText.text = "00:00";
        }
    }
}