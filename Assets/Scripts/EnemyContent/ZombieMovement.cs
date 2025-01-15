using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;
    [SerializeField] private float _minX = 3f;
    [SerializeField] private float _maxX = 3f;
    [SerializeField] private float _minZ = 3f;
    [SerializeField] private float _maxZ = 3f;

    [SerializeField] private Animator _animator;

    private Vector3 targetPosition;
    private float waitTime;
    private float waitCounter;
    private bool _isWaiting = true;
    [SerializeField] private bool _isLoppedPosition = true;
    private Coroutine _moveCoroutine;

    private void OnEnable()
    {
        if (_enemy.Health <= 0)
        {
            SearchTargetPosition();
        }
    }

    private void Start()
    {
        // SearchTargetPosition();
    }

    private void Update()
    {
        if (_isLoppedPosition || _enemy.Health <= 0)
            return;

        if (_isWaiting)
        {
            waitCounter += Time.deltaTime;

            if (waitCounter >= waitTime)
            {
                _isWaiting = false;
                _animator.Play("Walk");
            }
        }
        else if (!_isWaiting && _enemy.Health > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            // transform.LookAt(targetPosition);
            
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                _animator.Play("Idle");
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
            _animator.Play("Idle");
            _isWaiting = true;
        }
    }

    private void SearchTargetPosition()
    {
        _isWaiting = true;
        waitCounter = 0;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        float x = Random.Range(_minX, _maxX);
        float z = Random.Range(_minZ, _maxZ);
        targetPosition = new Vector3(x, transform.position.y, z);
    }

    public void SetValue(float value,float timeWait)
    {
        _moveSpeed = value;
        maxWaitTime = timeWait;
    }
}