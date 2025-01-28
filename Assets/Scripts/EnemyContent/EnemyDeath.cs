using System.Collections;
using UnityEngine;

namespace EnemyContent
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(EnemyRigidbodies))]
    [RequireComponent(typeof(EnemyAnimation))]
    [RequireComponent(typeof(EnemyRevive))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private ZombieReviver _zombieReviver;
        [SerializeField] private KillsZombieCounter _zombieKillCounter;

        private Enemy _enemy;
        private EnemyRigidbodies _enemyRigidbodies;
        private EnemyAnimation _enemyAnimation;
        private EnemyRevive _enemyRevive;
        private float _duration = 3.5f;
        private Coroutine _coroutine;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _enemyRigidbodies = GetComponent<EnemyRigidbodies>();
            _enemyAnimation = GetComponent<EnemyAnimation>();
            _enemyRevive = GetComponent<EnemyRevive>();
        }

        public void Die()
        {
            _enemy.HeadCollider.enabled = false;
            _enemy.SetValue(true);
            _zombieKillCounter.AddKills();
            _enemyAnimation.SetEnabled(false);
            _enemyRigidbodies.DisableKinematic();
            StartAddedFromReviver();
        }

        private void StartAddedFromReviver()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(AddedReviver());
        }

        private IEnumerator AddedReviver()
        {
            yield return new WaitForSeconds(_duration);
            _zombieReviver.AddDeadEnemy(_enemyRevive);
        }
    }
}