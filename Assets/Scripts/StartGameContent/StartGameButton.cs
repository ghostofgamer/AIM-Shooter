using StartGameContent;
using UnityEngine;

public class StartGameButton : MonoBehaviour, ISettingsHandler
{
    [SerializeField] private DifficultySettings _difficultySettings;
    [SerializeField] private StartGame _startGame;

    public void SetSettings() => _startGame.Play(_difficultySettings);
}