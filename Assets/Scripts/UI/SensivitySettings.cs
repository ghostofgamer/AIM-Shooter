using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensivitySettings : MonoBehaviour
{
    [field: SerializeField] public float SensitivityMouse { get; private set; } = 100f;
    [SerializeField] private TMP_Text _valueText;

    public Slider sensitivitySlider;
    public float minSensitivity = 0f;
    public float maxSensitivity = 500f;

    private float _defaultSensitivity = 150f;

    private void Start()
    {
        float currentSensitivity = PlayerPrefs.GetFloat("Sensitivity", _defaultSensitivity);

        sensitivitySlider.minValue = 0f;
        sensitivitySlider.maxValue = 5f;
        sensitivitySlider.value = MapValue(currentSensitivity, minSensitivity, maxSensitivity, 0, 5f);
        _valueText.text = sensitivitySlider.value.ToString("F1");
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnSensitivityChanged(float value)
    {
        SensitivityMouse = MapValue(value, 0f, 5f, minSensitivity, maxSensitivity);
        _valueText.text = value.ToString("F1");

        PlayerPrefs.SetFloat("Sensitivity", SensitivityMouse);
        PlayerPrefs.Save();
    }

    private float MapValue(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }
}