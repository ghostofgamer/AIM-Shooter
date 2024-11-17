using UnityEngine;

public class Target : MonoBehaviour,IDamageable,ITargetHandler
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

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void HandleHit()
    {
        Debug.Log("ВЫКЛ");
        gameObject.SetActive(false);
    }
}
