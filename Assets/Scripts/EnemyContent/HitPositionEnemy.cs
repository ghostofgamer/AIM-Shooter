using UnityEngine;

namespace EnemyContent
{
    public class HitPositionEnemy : MonoBehaviour
    {
        [SerializeField] private bool _isHead;
        [SerializeField]private EnemyDeath _death;

        public bool IsHead => _isHead;

        void Start()
        {
            _death = GetComponentInParent<EnemyDeath>();
        }
        
        public void Hit()
        {
            if (_isHead)
                _death.Die();
        }
    }
}