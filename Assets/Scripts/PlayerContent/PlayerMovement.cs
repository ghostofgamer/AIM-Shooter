using UnityEngine;

namespace PlayerContent
{
    public class PlayerMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speedWalk = 13;
        [SerializeField] private float _speedRun = 31;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _jumpHight;
        [SerializeField] private float _gravity = -9.81f;

        private float _x;
        private float _z;
        private Vector3 _move;
        private Vector3 _velocity;
        private bool _isGrounded;
        private float _speed;
        private PlayerInput _playerInput;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            _speed = _playerInput.IsRunning && _isGrounded ? _speedRun : _speedWalk;
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
            _characterController.height = _playerInput.IsCrouching ? 1.5f : 3;

            if (_isGrounded && _velocity.y < 0)
                _velocity.y = -2f;

            _x = _playerInput.X;
            _z = _playerInput.Z;
            _move = transform.right * _x + transform.forward * _z;
            _characterController.Move(_move * _speed * Time.deltaTime);
            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        public void Jump()
        {
            if (_isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpHight * -2 * _gravity);
        }
    }
}