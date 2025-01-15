using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]private TMP_Text _timerText; 
    
    private bool _isTimerActive = false;
    private float _currentTime = 6f;
    
    public event Action GameEnded ;
    
    private void Start()
    {
        _timerText.text = "00:00";
        StartTimer();
    }
    
    private void StartTimer()
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
