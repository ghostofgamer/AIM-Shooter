using UI.Screens;
using UnityEngine;

namespace UI.Buttons
{
    public class ChooseLevelButton : AbstractButton
    {
        [SerializeField] private int _index;
        [SerializeField] private TutorialScreen _tutorialScreen;
    
        protected override void OnClick()
        {
            _tutorialScreen.gameObject.SetActive(true);
            _tutorialScreen.OpenPage(_index);
        }
    }
}