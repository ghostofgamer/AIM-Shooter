using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieReviver : MonoBehaviour
{
    [SerializeField] private float _minRespawnTime = 3f;
    [SerializeField] private float _maxRespawnTime = 5f;
    
    private Queue<Enemy> _deadEnemies = new Queue<Enemy>();

    // public event Action EnemyDeaded;
    
    private void Start()
    {
        StartCoroutine(RespawnEnemies());
    }

    public void AddDeadEnemy(Enemy enemy)
    {
        // EnemyDeaded?.Invoke();
        _deadEnemies.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }

    private IEnumerator RespawnEnemies()
    {
        while (true)
        {
            if (_deadEnemies.Count > 0)
            {
                Enemy enemy = _deadEnemies.Dequeue();
                float respawnTime = Random.Range(_minRespawnTime, _maxRespawnTime);
                yield return new WaitForSeconds(respawnTime);
                enemy.gameObject.SetActive(true);
                enemy.StartRevive();
                // Здесь можно добавить логику для восстановления здоровья или других параметров врага
            }
            yield return null;
        }
    }
}
