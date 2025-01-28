using UnityEngine;
using UnityEngine.UI;

namespace AimChangerContent
{
    public class AimScaleChanger : MonoBehaviour, IValueChanger
    {
        private const string AimScaleKey = "AimScale";
    
        [SerializeField] private Image _image;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private int _factor;
    
        public void ChangeValue()
        {
            _image.transform.localScale = _scale;
            PlayerPrefs.SetInt(AimScaleKey, _factor);
        }
    }
}