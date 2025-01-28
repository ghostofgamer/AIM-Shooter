using System;
using Interfaces;
using UnityEngine;

namespace EnvironmentContent
{
    public class StopGameButton : MonoBehaviour, IValueChanger
    {
        [SerializeField] private StartGame _startGame;
        [SerializeField] private float _step = 3f;

        private Vector3 _defaultPosition;

        public event Action Stoping;

        private void OnEnable()
        {
            _startGame.Started += ButtonMove;
        }

        private void OnDisable()
        {
            _startGame.Started -= ButtonMove;
        }

        private void Start()
        {
            _defaultPosition = transform.position;
        }

        public void ChangeValue()
        {
            Stoping?.Invoke();
            transform.position = _defaultPosition;
        }

        private void ButtonMove()
        {
            transform.position = new Vector3(transform.position.x + _step, transform.position.y, _defaultPosition.z);
        }
    }
}