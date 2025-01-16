using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public float timeMultiplier = 1f;
    public float easyDifficultyMultiplier = 1f; 
    public float mediumDifficultyMultiplier = 3f; 
    public float hardDifficultyMultiplier = 5f;
    
    
    public void CalculateScore(float timeSpent, int difficultyLevel, float accuracyPercentage)
    {
        // Вычисляем множитель времени
        float timeFactor = timeSpent * timeMultiplier;

        // Вычисляем множитель сложности
        float difficultyMultiplier = 1f;
        
        Debug.Log("ЧТО ТУТ? " + difficultyLevel);
        
        switch (difficultyLevel)
        {
            case 1:
                difficultyMultiplier = easyDifficultyMultiplier;
                break;
            case 2:
                difficultyMultiplier = mediumDifficultyMultiplier;
                break;
            case 3:
                difficultyMultiplier = hardDifficultyMultiplier;
                break;
            default:
                Debug.LogWarning("Invalid difficulty level");
                break;
        }

        // Вычисляем множитель точности
        float accuracyMultiplier = accuracyPercentage / 100f;

        // Вычисляем итоговые очки
        
        
        Debug.Log("TimeFactor " + timeFactor);
        Debug.Log("DifficultyMultiplier " +difficultyMultiplier);
        Debug.Log("AccuracyMultiplier " + accuracyMultiplier);
        float finalScore = timeFactor * difficultyMultiplier * accuracyMultiplier;

        // Выводим результат
        Debug.Log("Final Score: " + finalScore);
    }
}
