using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider _loadingSlider;
    public TMP_Text _loadingText;

    private Coroutine _loadingCoroutine;

    public void LoadScene(int index)
    {
        if (_loadingCoroutine != null)
            StopCoroutine(_loadingCoroutine);

        _loadingCoroutine = StartCoroutine(LoadSceneAsync(index));
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        Time.timeScale = 1;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        float progress = 0f;
        
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress / 0.9f, Time.deltaTime);
            _loadingSlider.value = progress;
 
            if (_loadingText != null)
                _loadingText.text = "Loading... " + (progress * 100).ToString("F0") + "%";

            if (asyncOperation.progress >= 0.9f)
            {
                progress = Mathf.MoveTowards(progress, 1f, Time.deltaTime);
                _loadingSlider.value = progress;
                _loadingText.text = "Loading... " + (progress * 100).ToString("F0") + "%";
                
                if (progress >= 1f)
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}