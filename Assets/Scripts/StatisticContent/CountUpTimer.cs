using EnvironmentContent;
using StartGameContent;
using TMPro;
using UnityEngine;

namespace StatisticContent
{
    public class CountUpTimer : MonoBehaviour
    {
        [SerializeField] private StopGameButton _stopGameButton;
        [SerializeField] private StartGame _startGame;
        [SerializeField] private TMP_Text _timerText;

        private float _currentTime = 0f;
        private bool _isTimerRunning = false;
        private float _minutes;
        private float _seconds;
        private float _houre = 60f;

        public float CurrentTime => _currentTime;

        private void OnEnable()
        {
            _startGame.Started += StartTime;
            _stopGameButton.Stoping += StopTime;
        }

        private void OnDisable()
        {
            _startGame.Started -= StartTime;
            _stopGameButton.Stoping -= StopTime;
        }

        private void Update()
        {
            if (!_isTimerRunning) return;

            _currentTime += Time.deltaTime;
            UpdateTimerText();
        }

        private void StartTime()
        {
            _isTimerRunning = true;
            _currentTime = 0f;
            UpdateTimerText();
        }

        private void StopTime()
        {
            _isTimerRunning = false;
            _currentTime = 0f;
            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            if (_currentTime == 0)
            {
                _timerText.text = "0:00";
            }
            else
            {
                _minutes = Mathf.FloorToInt(_currentTime / _houre);
                _seconds = Mathf.FloorToInt(_currentTime % _houre);
                _timerText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
            }
        }
    }
}