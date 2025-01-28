using System.Collections;
using System.Collections.Generic;
using EnemyContent;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieReviver : MonoBehaviour
{
    [SerializeField] private float _minRespawnTime = 3f;
    [SerializeField] private float _maxRespawnTime = 5f;
    
    private Queue<EnemyRevive> _deadEnemies = new Queue<EnemyRevive>();
    
    private void Start()
    {
        StartCoroutine(RespawnEnemies());
    }

    public void AddDeadEnemy(EnemyRevive enemy)
    {
        _deadEnemies.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }

    private IEnumerator RespawnEnemies()
    {
        while (true)
        {
            if (_deadEnemies.Count > 0)
            {
                EnemyRevive enemy = _deadEnemies.Dequeue();
                float respawnTime = Random.Range(_minRespawnTime, _maxRespawnTime);
                yield return new WaitForSeconds(respawnTime);
                enemy.gameObject.SetActive(true);
                enemy.StartRevive();
            }
            yield return null;
        }
    }
}
