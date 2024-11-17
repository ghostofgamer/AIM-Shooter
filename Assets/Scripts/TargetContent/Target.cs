using UnityEngine;

public class Target : MonoBehaviour, IDamageable, ITargetHandler
{
    [SerializeField] private int _health = 50;
    
    private RecordCounter _recordCounter;

    public void Init(RecordCounter recordCounter)
    {
        _recordCounter = recordCounter;
    }
    
    public void Die()
    {
        Debug.Log("Сам ВЫКЛ");
        _recordCounter.AddDie();
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    public void HandleHit() => gameObject.SetActive(false);
}