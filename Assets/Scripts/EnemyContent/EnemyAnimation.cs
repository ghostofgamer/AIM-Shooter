using UnityEngine;

namespace EnemyContent
{
    public class EnemyAnimation : MonoBehaviour
    {
        private const string Idle = "Idle";
        private const string Walk = "Walk";

        [SerializeField] private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetEnabled(bool value)
        {
            _animator.enabled = value;
        }

        public void PlayIdle()
        {
            _animator.Play(Idle);
        }

        public void PlayWalk()
        {
            _animator.Play(Walk);
        }
    }
}