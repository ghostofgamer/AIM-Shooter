using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigidbodies;

    private int _health;

    private void Start()
    {
        _health = 100;
    }

    public void TakeDamage(int value)
    {
        _health -= value;

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    public void HeadShoot()
    {
        // _health = 0;
        Die();
    }

    private void Die()
    {
        _animator.enabled = false;

        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;
    }
}