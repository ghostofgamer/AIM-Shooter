using UnityEngine;
using UnityEngine.UI;

public class AimColorChanger : MonoBehaviour, IValueChanger
{
    [SerializeField]private Image _image;
    [SerializeField] private Color _color;
    [SerializeField] private int _index;

    public void ChangeValue()
    {
        _image.color = _color;
        PlayerPrefs.SetInt("AimColor", _index);
    }
}
