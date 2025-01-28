using UnityEngine;
using UnityEngine.UI;

namespace AimChangerContent
{
    public class AimColorChanger : MonoBehaviour, IValueChanger
    {
        private const string AimColorKey = "AimColor";
    
        [SerializeField]private Image _image;
        [SerializeField] private Color _color;
        [SerializeField] private int _index;

        public void ChangeValue()
        {
            _image.color = _color;
            PlayerPrefs.SetInt(AimColorKey, _index);
        }
    }
}