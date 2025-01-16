using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "ScriptableObjects/DifficultySettings", order = 1)]
public class DifficultySettings : ScriptableObject
{
    public float spawnDelay;
    public float scaleDuration;
    public Vector3 spawnRange;
    public float minDistanceBetweenTargets;
    public int Difficulty;
}
