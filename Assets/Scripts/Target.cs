using UnityEngine;

public class Target : MonoBehaviour,IDamageable
{
    [SerializeField] private int _health = 50;

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }
}
