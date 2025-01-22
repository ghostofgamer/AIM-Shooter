using UnityEngine;

public abstract class AD : MonoBehaviour
{
    [SerializeField]private FocusScreen _focusScreen;
    
    private int _activeValue = 1;
    private int _inactiveValue = 0;

    public bool IsAdCompleted = false;
    
    public abstract void Show();

    protected void OnOpen()
    {
        _focusScreen.SetValueWork(false);
        SetValue(_inactiveValue);
    }

    protected void OnClose(bool isClosed)
    {
        _focusScreen.SetValueWork(true);
        SetValueAdCompleted(true);
        SetValue(_activeValue);
    }

    protected virtual void OnClose()
    {
        _focusScreen.SetValueWork(true);
        SetValueAdCompleted(true);
        SetValue(_activeValue);
    }

    private void SetValue(int value)
    {
        Time.timeScale = value;
        AudioListener.volume = value;
    }

    public void SetValueAdCompleted(bool isCompleted)
    {
        IsAdCompleted = isCompleted;
    }
}