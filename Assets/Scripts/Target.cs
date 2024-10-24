using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _health=50f;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
