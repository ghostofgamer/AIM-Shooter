using UnityEngine;

public class PauseScreen : AbstractScreen
{
    [SerializeField] protected CanvasGroup _canvas;
    [SerializeField] private PlayerInput _playerInput;

    private bool _isOpened;

    private void OnEnable()
    {
        _playerInput.PausePressed += ProcessScreen;
    }

    private void OnDisable()
    {
        _playerInput.PausePressed -= ProcessScreen;
    }

    private void ProcessScreen()
    {
        if (_isOpened)
            Close();
        else
            Open();
    }

    public override void Close()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        ChangeValue(0, false);
    }

    public override void Open()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        ChangeValue(1, true);
    }

    private void ChangeValue(float alphaValue, bool boolValue)
    {
        _canvas.alpha = alphaValue;
        _canvas.interactable = boolValue;
        _canvas.blocksRaycasts = boolValue;
        _isOpened = boolValue;
    }
}