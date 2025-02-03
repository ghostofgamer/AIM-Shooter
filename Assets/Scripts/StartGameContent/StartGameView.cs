using System;
using System.Collections;
using EnvironmentContent;
using TMPro;
using UnityEngine;

namespace StartGameContent
{
    public class StartGameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countdownText;
        [SerializeField] private StartGame _startGame;
        [SerializeField] private StopGameButton _stopGameButton;

        private float _countDownDuration = 1f;
        private Coroutine _coroutine;
        private float _elapsedTime;
        private float _scale;

        public event Action CountdownStarting;

        private void OnEnable()
        {
            _stopGameButton.Stoping += StopCountdown;
            _startGame.GameStarting += StartCountdownGame;
        }

        private void OnDisable()
        {
            _stopGameButton.Stoping -= StopCountdown;
            _startGame.GameStarting -= StartCountdownGame;
        }

        private void StartCountdownGame()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            _countdownText.gameObject.SetActive(true);
            CountdownStarting?.Invoke();

            for (int i = 3; i >= 1; i--)
                yield return StartCoroutine(AnimateText(i.ToString(), _countDownDuration));

            yield return StartCoroutine(AnimateText("GO", _countDownDuration));
            _countdownText.gameObject.SetActive(false);
        }

        private IEnumerator AnimateText(string text, float duration)
        {
            _countdownText.text = text;
            _elapsedTime = 0f;

            while (_elapsedTime < duration)
            {
                _scale = Mathf.Lerp(1f, 0f, _elapsedTime / duration);
                _countdownText.transform.localScale = Vector3.one * _scale;
                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            _countdownText.transform.localScale = Vector3.one;
        }

        private void StopCountdown()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _countdownText.gameObject.SetActive(false);
        }
    }
}