using UnityEngine;
using UnityEngine.UI;

public class AimScaleChanger : MonoBehaviour, IValueChanger
{
    [SerializeField] private Image _image;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private int _factor;
    
    public void ChangeValue()
    {
        _image.transform.localScale = _scale;
        PlayerPrefs.SetInt("AimScale", _factor);
    }
}