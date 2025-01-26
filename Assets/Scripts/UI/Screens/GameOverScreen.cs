using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private YandexLeaderboard _yandexLeaderBoard;
    [SerializeField] private RecordCounter _recordCounter;
    [SerializeField] private ScoreCalculator _scoreCalculator;
    [SerializeField] private bool _isTimeBased;
    [SerializeField] private TimerNew _timerNew;
    [SerializeField] private StartGame _startGame;

    [Header("TMP_Text Elements ")] [SerializeField]
    private TMP_Text _shootsText;

    [SerializeField] private TMP_Text _hitsText;
    [SerializeField] private TMP_Text _percentText;
    [SerializeField] private TMP_Text _recordScoreText;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _targetAmountText;
    [SerializeField] private TMP_Text _percentKillTarget;
    // [SerializeField]private Timer _timer;

    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _recordCounter.LevelCompleted += ShowScreen;
        // _timer.GameEnded += ShowScreen;
    }

    private void OnDisable()
    {
        _recordCounter.LevelCompleted -= ShowScreen;
        // _timer.GameEnded -= ShowScreen;
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void ShowScreen(int shootsCount, int hitsCount, float percentValue, int targetAmount, bool isSumTargetSpawn)
    {
        _shootsText.text = shootsCount.ToString();
        _hitsText.text = hitsCount.ToString();
        _percentText.text = percentValue.ToString("F3") + " %";

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int score = 0;

        if (_isTimeBased)
        {
            score = _scoreCalculator.CalculateScore(_timerNew.CurrentTime, _startGame.Difficulty, percentValue);
            _currentScoreText.text = score.ToString();
            int record = PlayerPrefs.GetInt("Record" + currentSceneIndex, 0);

            if (score > record)
            {
                // Debug.Log("сохраняем " + record);
                PlayerPrefs.SetInt("Record" + currentSceneIndex, score);
            }

            int currentRecord = PlayerPrefs.GetInt("Record" + currentSceneIndex, 0);
            _recordScoreText.text = currentRecord.ToString();
        }
        else
        {
            score = _scoreCalculator.CalculateScoreWithoutTime(_startGame.Difficulty, percentValue);
            _currentScoreText.text = score.ToString();
            int record = PlayerPrefs.GetInt("Record" + currentSceneIndex, 0);

            if (score > record)
            {
                // Debug.Log("сохраняем " + record);
                PlayerPrefs.SetInt("Record" + currentSceneIndex, score);
            }

            int currentRecord = PlayerPrefs.GetInt("Record" + currentSceneIndex, 0);
            _recordScoreText.text = currentRecord.ToString();
        }

        int leaderboardScore = PlayerPrefs.GetInt("LeaderboardScore", 0);
        leaderboardScore += score;
        PlayerPrefs.SetInt("LeaderboardScore", leaderboardScore);
        // _yandexLeaderBoard.SetPlayerScore(leaderboardScore);


        if (isSumTargetSpawn)
        {
            _targetAmountText.text = targetAmount.ToString();
            float percent = (float)hitsCount / targetAmount * 100;
            _percentKillTarget.text = percent.ToString("F3") + "%";
        }

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