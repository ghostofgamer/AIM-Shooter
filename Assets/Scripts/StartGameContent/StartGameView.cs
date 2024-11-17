using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StartGameView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField]private StartGame _startGame;
    
    private float _countDownDuration = 1f;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _startGame.GameStarting += StartCountdownGame;
    }

    private void OnDisable()
    {
        _startGame.GameStarting -= StartCountdownGame;
    }

    private void StartCountdownGame()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        _countdownText.gameObject.SetActive(true);
        
        for (int i = 3; i >= 1; i--)
        {
            _countdownText.text = i.ToString();
            float elapsedTime = 0f;

            while (elapsedTime < _countDownDuration)
            {
                float scale = Mathf.Lerp(1f, 0f, elapsedTime / _countDownDuration);
                _countdownText.transform.localScale = Vector3.one * scale;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _countdownText.transform.localScale = Vector3.one;
        }

        _countdownText.text = "GO";
        float elapsedTimeGo = 0f;
        
        while (elapsedTimeGo < _countDownDuration)
        {
            float scale = Mathf.Lerp(1f, 0f, elapsedTimeGo / _countDownDuration);
            _countdownText.transform.localScale = Vector3.one * scale;
            elapsedTimeGo += Time.deltaTime;
            yield return null;
        }

        _countdownText.gameObject.SetActive(false);
    }
}
