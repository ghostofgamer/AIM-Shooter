using UnityEngine;

namespace StatisticContent
{
    public class ScoreCalculator : MonoBehaviour
    {
        private float _timeMultiplier = 1f;
        private float _easyDifficultyMultiplier = 1f;
        private float _mediumDifficultyMultiplier = 3f;
        private float _hardDifficultyMultiplier = 6f;
        private float _timeFactor;
        private float _difficultyMultiplier;
        private float _accuracyMultiplier;
        private float _fixedMultiplier;
        private float _factor = 100f;

        public int CalculateScore(float timeSpent, int difficultyLevel, float accuracyPercentage)
        {
            _timeFactor = timeSpent * _timeMultiplier;
            _difficultyMultiplier = GetDifficultyMultiplier(difficultyLevel);
            _accuracyMultiplier = accuracyPercentage / _factor;
            return Mathf.RoundToInt(_timeFactor * _difficultyMultiplier * _accuracyMultiplier);
            ;
        }

        public int CalculateScoreWithoutTime(int difficultyLevel, float accuracyPercentage)
        {
            _difficultyMultiplier = GetDifficultyMultiplier(difficultyLevel);
            _accuracyMultiplier = accuracyPercentage / _factor;
            _fixedMultiplier = _factor;
            return Mathf.RoundToInt(_fixedMultiplier * _difficultyMultiplier * _accuracyMultiplier);
        }

        private float GetDifficultyMultiplier(int difficultyLevel)
        {
            return difficultyLevel switch
            {
                1 => _easyDifficultyMultiplier,
                2 => _mediumDifficultyMultiplier,
                3 => _hardDifficultyMultiplier,
                _ => ThrowInvalidDifficultyLevelWarning()
            };
        }

        private float ThrowInvalidDifficultyLevelWarning()
        {
            Debug.LogWarning("Invalid difficulty level");
            return 1f;
        }
    }
}