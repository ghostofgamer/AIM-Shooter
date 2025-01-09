using System;
using UnityEngine;

public class HitPositionEnemy : MonoBehaviour,IDamageable
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _multiplication;
    [SerializeField] private bool _isHead;

    public bool IsHead => _isHead;

    private void Start()
    {
        
    }

    public void Damage(int damage)
    {
        if (_isHead)
            _enemy.HeadShoot();
        else
            _enemy.TakeDamage(damage * _multiplication);
    }

    public void TakeDamage(int damage)
    {
        if (_isHead)
            _enemy.HeadShoot();
        else
            _enemy.TakeDamage(damage * _multiplication);
    }
}