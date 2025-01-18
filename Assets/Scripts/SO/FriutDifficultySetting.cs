using UnityEngine;

[CreateAssetMenu(fileName = "FruitDifficultySettings", menuName = "ScriptableObjects/FruitDifficultySettings",
    order = 1)]
public class FriutDifficultySetting : DifficultySettings
{
    [SerializeField] private float _minDelay = 0.3f;
    [SerializeField] private float _maxDelay = 1f;
    [SerializeField] private float _minAngle = -15f;
    [SerializeField] private float _maxAngle = 15f;
    [SerializeField] private float _minForce = 10f;
    [SerializeField] private float _maxForce = 15f;
    [SerializeField] private float _lifeTime = 4f;
    [SerializeField] private float _maxTorque = 5f;
    [SerializeField] private float _spawnBobmChance = 0.05f;

    public float MinDelay => _minDelay;

    public float MaxDelay => _maxDelay;

    public float MinAngle => _minAngle;

    public float MaxAngle => _maxAngle;

    public float MinForce => _minForce;

    public float MaxForce => _maxForce;

    public float LifeTime => _lifeTime;

    public float MaxTorque => _maxTorque;

    public float SpawnBobmChance => _spawnBobmChance;
}