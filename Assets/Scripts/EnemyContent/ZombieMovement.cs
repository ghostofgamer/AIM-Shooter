using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyContent
{
    public class ZombieMovement : MonoBehaviour, IMovable
    {
        [Header("Settings")] [SerializeField] private float _minX = 3f;
        [SerializeField] private float _maxX = 3f;
        [SerializeField] private float _minZ = 3f;
        [SerializeField] private float _maxZ = 3f;
        [SerializeField] private bool _isLoppedPosition = true;

        private float _moveSpeed = 1.65f;
        private float _minWaitTime = 1f;
        private float _maxWaitTime = 3f;
        private Enemy _enemy;
        private EnemyAnimation _enemyAnimation;
        private Vector3 _targetPosition;
        private float _waitTime;
        private float _waitCounter;
        private bool _isWaiting = true;
        private Coroutine _moveCoroutine;
        private float _x;
        private float _z;
        private Vector3 _localTargetPosition;
        private Quaternion _targetRotation;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _enemyAnimation = GetComponent<EnemyAnimation>();
        }

        private void OnEnable()
        {
            if (!_enemy.IsDead)
                SearchTargetPosition();
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            if (_isLoppedPosition || _enemy.IsDead)
                return;

            if (_isWaiting)
            {
                _waitCounter += Time.deltaTime;

                if (_waitCounter >= _waitTime)
                {
                    _isWaiting = false;
                    _enemyAnimation.PlayWalk();
                }
            }
            else if (!_isWaiting && !_enemy.IsDead)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    _targetPosition,
                    _moveSpeed * Time.deltaTime);
                _targetRotation = Quaternion.LookRotation(_targetPosition - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime);

                if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
                {
                    _enemyAnimation.PlayIdle();
                    SearchTargetPosition();
                }
            }
            else
            {
                _isWaiting = true;
            }
        }

        public void SetLoopPositionValue(bool value)
        {
            _isLoppedPosition = value;

            if (value)
            {
                _enemyAnimation.PlayIdle();
                _isWaiting = true;
            }
        }

        public void SearchTargetPosition()
        {
            _isWaiting = true;
            _waitCounter = 0;
            _waitTime = Random.Range(_minWaitTime, _maxWaitTime);
            _x = Random.Range(_minX, _maxX);
            _z = Random.Range(_minZ, _maxZ);
            _localTargetPosition = new Vector3(_x, 0, _z);
            _targetPosition = transform.parent.TransformPoint(_localTargetPosition);
            _targetPosition.y = transform.position.y;
        }

        public void SetValue(float value, float timeWait)
        {
            _moveSpeed = value;
            _maxWaitTime = timeWait;
        }
    }
}