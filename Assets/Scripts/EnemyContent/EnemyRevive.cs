using UnityEngine;

namespace EnemyContent
{
    public class EnemyRevive : MonoBehaviour
    {
        private Enemy _enemy;
        private EnemyAnimation _enemyAnimation;
        private EnemyRigidbodies _enemyRigidbodies;
        private ZombieMovement _zombieMovement;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _enemyRigidbodies = GetComponent<EnemyRigidbodies>();
            _enemyAnimation = GetComponent<EnemyAnimation>();
            _zombieMovement = GetComponent<ZombieMovement>();
        }

        public void StartRevive()
        {
            _enemy.HeadCollider.enabled = true;
            _enemy.SetValue(false);
            _enemyRigidbodies.SetDefaultSettings();
            _enemyAnimation.SetEnabled(true);
            _enemyAnimation.PlayIdle();
            _zombieMovement.SearchTargetPosition();
        }
    }
}