using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : AbstractButton
{
    [SerializeField] private FullAd _fullAd;

    protected override void OnClick() => LoadScene();

    private void LoadScene()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(ShowAdAndReloadScene());
#else
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }

    private IEnumerator ShowAdAndReloadScene()
    {
        _fullAd.Show();

        while (!_fullAd.GetAdCompleted())
            yield return null;

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}