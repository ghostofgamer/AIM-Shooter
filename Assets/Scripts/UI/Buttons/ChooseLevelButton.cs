using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevelButton : AbstractButton
{
    [SerializeField] private int _index;
    [SerializeField] private string _name;
    [SerializeField] private bool isTimeScaleDisabled;
    
    protected override void OnClick()
    {
        if(isTimeScaleDisabled)
            Time.timeScale = 1;
        
        SceneManager.LoadScene(_name);
    }
}