using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelButton : AbstractButton
{
    protected override void OnClick()
    {
        int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 0) + 1;
        Debug.Log("Индекс что грузим " + levelIndex);
        
        SceneManager.LoadScene(levelIndex);
    }
}