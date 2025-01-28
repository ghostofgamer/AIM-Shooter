using Interfaces;
using UnityEngine;

namespace StatisticContent
{
    public class ClearPercentSandboxScene : MonoBehaviour, ITargetHandler
    {
        [SerializeField] private PercentHitEnemy[] _percentHitEnemy;
        [SerializeField] private KillsZombieCounter _killsZombieCounter;

        public void HandleHit()
        {
            _killsZombieCounter.Clear();

            foreach (var percentHitEnemy in _percentHitEnemy)
                percentHitEnemy.Clear();
        }
    }
}