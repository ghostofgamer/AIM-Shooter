using Interfaces;
using UnityEngine;

public class Target : MonoBehaviour, ITargetHandler
{
    [SerializeField] private int _health = 50;
    [SerializeField]  private bool _isDeadCount = true;

    private RecordCounter _recordCounter;
    
    public void Init(RecordCounter recordCounter)
    {
        _recordCounter = recordCounter;
    }

    public void Die()
    {
        if (_isDeadCount)
            _recordCounter.AddDie();

        gameObject.SetActive(false);
    }

    public void HandleHit() => gameObject.SetActive(false);
}