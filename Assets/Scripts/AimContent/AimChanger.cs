using UnityEngine;
using UnityEngine.UI;

public class AimChanger : MonoBehaviour, IValueChanger
{
    [SerializeField] private Sprite _targetAim;
    [SerializeField] private Image _aim;
    [SerializeField] private int _index;

    public void ChangeValue()
    {
        _aim.sprite = _targetAim;
        PlayerPrefs.SetInt("Aim", _index);
    }
}