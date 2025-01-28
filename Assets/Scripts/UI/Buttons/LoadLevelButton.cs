using System.Collections;
using ADS;
using UnityEngine;

public class LoadLevelButton : AbstractButton
{
    [SerializeField] private FullAd _fullAd;
    [SerializeField]private LoadingScreen _loadingScreen;
    
    protected override void OnClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(ShowAdAndLoadScene());
#else
        int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0) + 2;
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.LoadScene(levelIndex);
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
        
        int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0) + 2;
        _loadingScreen.LoadScene(levelIndex);
    }
}