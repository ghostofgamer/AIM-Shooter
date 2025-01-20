using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelButton : AbstractButton
{
    [SerializeField] private FullAd _fullAd;
    [SerializeField]private GameObject _loadingScreen;
    
    protected override void OnClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(ShowAdAndLoadScene());
#else
        int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0) + 2;
        SceneManager.LoadScene(levelIndex);   
#endif
    }
    
    private IEnumerator ShowAdAndLoadScene()
    {
        _fullAd.Show();
        _loadingScreen.gameObject.SetActive(true);
        
        while (!_fullAd.GetAdCompleted())
        {
            yield return null;
        }

        int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0) + 2;
        SceneManager.LoadScene(levelIndex);
    }
}