using UnityEngine;
using UnityEngine.UI;

public class AimScaleChanger : MonoBehaviour, IValueChanger
{
    [SerializeField] private Image _image;
    [SerializeField] private Vector3 _scale;
    
    public void ChangeValue()
    {
        _image.transform.localScale = _scale;
    }
}