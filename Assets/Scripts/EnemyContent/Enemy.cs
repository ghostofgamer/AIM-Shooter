using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigidbodies;

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
        _health = 0;
        // _health = 0;
        Die();
    }

    private void Die()
    {
        _animator.enabled = false;

        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;
    }

    public void StartRevive()
    {
        StartCoroutine(Revive());
    }    
    
    private IEnumerator Revive()
    {
        yield return new WaitForSeconds(0f);

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