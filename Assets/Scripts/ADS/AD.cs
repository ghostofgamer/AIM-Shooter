using UnityEngine;

public abstract class AD : MonoBehaviour
{
    private int _activeValue = 1;
    private int _inactiveValue = 0;

    public bool IsAdCompleted = false;
    
    public abstract void Show();

    protected void OnOpen()
    {
        IsAdCompleted = false;
        SetValue(_inactiveValue);
    }

    protected void OnClose(bool isClosed)
    {
        IsAdCompleted = true;
        SetValue(_activeValue);
    }

    protected virtual void OnClose()
    {
        IsAdCompleted = true;
        SetValue(_activeValue);
    }

    private void SetValue(int value)
    {
        Time.timeScale = value;
        AudioListener.volume = value;
    }
}