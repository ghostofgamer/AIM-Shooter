using SingletonsContent;
using UnityEngine;
using UnityEngine.UI;

namespace SettingsContent
{
    public class SoundChanger : MonoBehaviour
    {
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Toggle _sfxToggle;

        private void Start()
        {
            if (_soundToggle != null)
            {
                _soundToggle.onValueChanged.AddListener(ChangeValueSound);
                _soundToggle.isOn = Sound.Instance.IsSoundOn();
            }

            if (_sfxToggle != null)
            {
                _sfxToggle.onValueChanged.AddListener(ChangeValueSFX);
                _sfxToggle.isOn = Sound.Instance.IsSFXOn();
            }
        }
    
        private void ChangeValueSound(bool enabled)
        {
            Sound.Instance.SetSound(enabled);
        }

        private void ChangeValueSFX(bool enabled)
        {
            Sound.Instance.SetSFX(enabled);
        }
    }
}