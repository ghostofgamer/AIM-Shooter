using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FovSettings : MonoBehaviour
{
    [SerializeField] private Slider _fovSlider;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private TMP_Text _fovText;

    private float _defaultFOV = 60;
    private float _currentFOV;

    private void Start()
    {
        float currentFov = PlayerPrefs.GetFloat("FOV", _defaultFOV);
        _mainCamera.fieldOfView = currentFov;

        if (_fovSlider != null)
        {
            _fovSlider.onValueChanged.AddListener(OnFOVChanged);
            _fovSlider.value = _mainCamera.fieldOfView;
        }

        _fovText.text = _fovSlider.value.ToString("F1");
    }

    private void OnFOVChanged(float value)
    {
        if (_mainCamera == null) return;


        _fovText.text = value.ToString("F1");
        _mainCamera.fieldOfView = value;
        PlayerPrefs.SetFloat("FOV", _mainCamera.fieldOfView);
        PlayerPrefs.Save();
    }
}