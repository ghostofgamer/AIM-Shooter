using UnityEngine;

namespace UI.Screens
{
    public class SettingsScreen : AbstractScreen
    {
        [SerializeField] private CanvasGroup _canvasGroup;
    
        public override void Open()
        {
            SetValue(1, true);
        }

        public override void Close()
        {
            SetValue(0, false);
        }

        private void SetValue(float alpha, bool value)
        {
            _canvasGroup.alpha = alpha;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }
}
