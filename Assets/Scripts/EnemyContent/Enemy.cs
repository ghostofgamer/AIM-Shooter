using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Collider _headCollieder;
    [SerializeField]private ZombieReviver _zombieReviver;
    [SerializeField]private KillsZombieCounter _zombieKillCounter;

    private int _health;
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    public int Health => _health;
    
    private void Start()
    {
        _health = 100;
        
        initialPositions = new Vector3[_rigidbodies.Length];
        initialRotations = new Quaternion[_rigidbodies.Length];
        
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            initialPositions[i] = _rigidbodies[i].transform.localPosition;
            initialRotations[i] = _rigidbodies[i].transform.localRotation;
        }
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
        _headCollieder.enabled = false;
        _health = 0;
        // _health = 0;
        Die();
    }

    private void Die()
    {
        _zombieKillCounter.AddKills();
        _animator.enabled = false;

        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;

        StartAddedFromReviver();
    }

    private void StartAddedFromReviver()
    {
        StartCoroutine(AddedReviver());
    }

    private IEnumerator AddedReviver()
    {
        yield return new WaitForSeconds(3.5f);
         _zombieReviver.AddDeadEnemy(this);
    }

    public void StartRevive()
    {
        _headCollieder.enabled = true;
        // StartCoroutine(Revive());
        _health = 100;
        
        foreach (var rb in _rigidbodies)
        {
            rb.isKinematic = true;
            rb.transform.localPosition = initialPositions[System.Array.IndexOf(_rigidbodies, rb)];
            rb.transform.localRotation = initialRotations[System.Array.IndexOf(_rigidbodies, rb)];
        }
        /*foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = true;*/
        
        _animator.enabled = true;
        _animator.Play("Idle");
    }    
}