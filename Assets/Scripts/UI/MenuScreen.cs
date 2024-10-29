using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;

    private bool _isOpened;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isOpened)
                Close();
            else
                Open();
        }
    }

    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ChangeValue(0, false);
    }

    private void Open()
    {
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