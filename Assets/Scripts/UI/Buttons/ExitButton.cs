using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class ExitButton : AbstractButton
    {
        private const string MainScene = "MainScene";

        protected override void OnClick() => LoadScene();
    
        private void LoadScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(MainScene);
        }
    }
}