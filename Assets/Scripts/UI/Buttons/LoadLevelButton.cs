using System.Collections;
using ADS;
using UnityEngine;

namespace UI.Buttons
{
    public class LoadLevelButton : AbstractButton
    {
        private const string CurrentLevel = "CurrentLevel";
        
        [SerializeField] private FullAd _fullAd;
        [SerializeField]private LoadingScreen _loadingScreen;

        private int _levelIndex;
        private int _factor = 2;
        
        protected override void OnClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(ShowAdAndLoadScene());
#else
            _levelIndex = PlayerPrefs.GetInt(CurrentLevel, 0) + _factor;
            _loadingScreen.gameObject.SetActive(true);
            _loadingScreen.LoadScene(_levelIndex);
#endif
        }
    
        private IEnumerator ShowAdAndLoadScene()
        {
            _fullAd.SetValueAdCompleted(false);
            _fullAd.Show();
            _loadingScreen.gameObject.SetActive(true);
        
            while (!_fullAd.GetAdCompleted())
            {
                yield return null;
            }
        
            _levelIndex = PlayerPrefs.GetInt(CurrentLevel, 0) + _factor;
            _loadingScreen.LoadScene(_levelIndex);
        }
    }
}