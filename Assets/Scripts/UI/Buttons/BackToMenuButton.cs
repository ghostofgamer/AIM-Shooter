using UnityEngine;

namespace UI.Buttons
{
    public class BackToMenuButton : AbstractButton
    {
        [SerializeField] private LoadingScreen _loadingScreen;

        private int _mainMenuIndexScene = 1;

        protected override void OnClick() => _loadingScreen.LoadScene(_mainMenuIndexScene);
    }
}