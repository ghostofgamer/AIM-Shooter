using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public float timeMultiplier = 1f;
    public float easyDifficultyMultiplier = 1f; 
    public float mediumDifficultyMultiplier = 3f; 
    public float hardDifficultyMultiplier = 5f;
    
    
    public int CalculateScore(float timeSpent, int difficultyLevel, float accuracyPercentage)
    {
        float timeFactor = timeSpent * timeMultiplier;
        float difficultyMultiplier = 1f;
        
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
        
        float accuracyMultiplier = accuracyPercentage / 100f;
        
        /*Debug.Log("TimeFactor " + timeFactor);
        Debug.Log("DifficultyMultiplier " +difficultyMultiplier);
        Debug.Log("AccuracyMultiplier " + accuracyMultiplier);*/
        
        // float finalScore = timeFactor * difficultyMultiplier * accuracyMultiplier;
        int finalScore = Mathf.RoundToInt(timeFactor * difficultyMultiplier * accuracyMultiplier);
return finalScore;
        // Выводим результат
        Debug.Log("Final Score: " + finalScore);
    }
}
