using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private RecordCounter _recordCounter;
    [SerializeField] private TMP_Text _shootsText;
    [SerializeField] private TMP_Text _hitsText;
    [SerializeField] private TMP_Text _percentText;

    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _recordCounter.LevelCompleted += ShowScreen;
    }

    private void OnDisable()
    {
        _recordCounter.LevelCompleted -= ShowScreen;
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void ShowScreen(int shootsCount, int hitsCount, float percentValue)
    {
        _shootsText.text = shootsCount.ToString();
        _hitsText.text = hitsCount.ToString();
        _percentText.text = percentValue.ToString("F3") + " %";
        Debug.Log(_percentText.text);
        Time.timeScale = 0;

        ChangeValue(1, true);
    }

    private void ChangeValue(float alphaValue, bool boolValue)
    {
        _canvasGroup.alpha = alphaValue;
        _canvasGroup.interactable = boolValue;
        _canvasGroup.blocksRaycasts = boolValue;
        Time.timeScale = 0;
        Cursor.visible = boolValue;

        if (boolValue)
            Cursor.lockState = CursorLockMode.None;

        // _canvasGroup = boolValue;
    }
}