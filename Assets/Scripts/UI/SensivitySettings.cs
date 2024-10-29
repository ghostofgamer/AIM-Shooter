using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensivitySettings : MonoBehaviour
{
    [field: SerializeField] public float SensitivityMouse { get; private set; } = 100f;
    [SerializeField] private TMP_Text _valueText;
    
    public Slider sensitivitySlider; // Ссылка на слайдер
    public float minSensitivity = 0f; // Минимальное значение сенситивности
    public float maxSensitivity = 500f; // Максимальное значение сенситивности

    private void Start()
    {
        // Убедитесь, что слайдер настроен правильно
        sensitivitySlider.minValue = 0f;
        sensitivitySlider.maxValue = 5f;

        // Установите начальное значение слайдера, если нужно
        // sensitivitySlider.value = 3f; // Например, начальное значение 3
        sensitivitySlider.value = MapValue(SensitivityMouse, minSensitivity, maxSensitivity,0,5f);
        _valueText.text = sensitivitySlider.value.ToString("F1");
        Debug.Log("pyfxtybt     " + sensitivitySlider.value);
    }

    private void Update()
    {
        // Преобразуйте значение слайдера в фактическое значение сенситивности
        SensitivityMouse = MapValue(sensitivitySlider.value, 0f, 5f, minSensitivity, maxSensitivity);
        _valueText.text = sensitivitySlider.value.ToString("F1");
    }

    private float MapValue(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }
}
