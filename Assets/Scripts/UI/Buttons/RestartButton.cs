using System.Collections;
using ADS;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class RestartButton : AbstractButton
    {
        [SerializeField] private FullAd _fullAd;
        [SerializeField]private LoadingScreen _loadingScreen;
    
        protected override void OnClick() => LoadScene();

        private void LoadScene()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(ShowAdAndReloadScene());
#else
            Time.timeScale = 1;
            _loadingScreen.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
        }

        private IEnumerator ShowAdAndReloadScene()
        {
            _fullAd.Show();
            _loadingScreen.gameObject.SetActive(true);
        
            while (!_fullAd.GetAdCompleted())
                yield return null;

            Time.timeScale = 1;
            _loadingScreen.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}