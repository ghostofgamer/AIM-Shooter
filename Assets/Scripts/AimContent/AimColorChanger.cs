using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimColorChanger : MonoBehaviour, IValueChanger
{
    [SerializeField]private Image _image;
    [SerializeField] private Color _color;

    public void ChangeValue()
    {
        _image.color = _color;
    }
}
