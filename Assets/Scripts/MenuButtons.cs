using UnityEngine;

public class MenuButtons : MonoBehaviour,IValueChanger
{
    [SerializeField] private PauseScreen _pauseScreen;
    
    public void ChangeValue()
    {
        _pauseScreen.Open();
    }
}
