using UnityEngine;

public abstract class AD : MonoBehaviour
{
    private int _activeValue = 1;
    private int _inactiveValue = 0;

    public bool IsAdCompleted = false;
    
    public abstract void Show();

    protected void OnOpen()
    {
        SetValue(_inactiveValue);
    }

    protected void OnClose(bool isClosed)
    {
        SetValueAdCompleted(true);
        SetValue(_activeValue);
    }

    protected virtual void OnClose()
    {
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