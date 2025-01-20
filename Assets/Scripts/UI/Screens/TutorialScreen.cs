using UnityEngine;

public class TutorialScreen : AbstractScreen
{
    [SerializeField] private GameObject[] _pages;
    [SerializeField] private CanvasGroup _canvasGroup;

    private int _currentLevelIndex;

    public void OpenPage(int index)
    {
        // _currentLevelIndex = index;
        PlayerPrefs.SetInt("CurrentLevel", index);
        
        foreach (var page in _pages)
            page.SetActive(false);

        _pages[index].SetActive(true);
    }
}