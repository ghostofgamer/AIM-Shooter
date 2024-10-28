using UnityEngine;
using UnityEngine.UI;

public class AimScaleChanger : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Vector3 _scale;

    public void ChangeScale()
    {
        _image.transform.localScale = _scale;
    }
}