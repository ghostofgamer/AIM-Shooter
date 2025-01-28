using UnityEngine;

namespace EnemyContent
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Collider _headCollieder;

        public bool IsDead { get; private set; } = false;

        public Collider HeadCollider => _headCollieder;

        public void SetValue(bool value) => IsDead = value;
    }
}