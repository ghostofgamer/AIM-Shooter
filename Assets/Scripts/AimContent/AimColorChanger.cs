using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimColorChanger : MonoBehaviour
{
    [SerializeField]private Image _image;
    [SerializeField] private Color _color;

    public void ChangeColor()
    {
        _image.color = _color;
    }
}
