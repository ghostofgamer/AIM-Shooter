using System;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerInput : MonoBehaviour
    {
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Jump = "Jump";

        [SerializeField] private LookMouse _lookMouse;
        
        private PlayerMovement _playerMovement;
        private float _mouseX;
        private float _mouseY;

        public float X { get; private set; }

        public float Z { get; private set; }

        public bool IsRunning { get; private set; }

        public bool IsCrouching { get; private set; }

        public event Action RKeyPressed;

        public event Action MouseZeroKeyPressed;

        public event Action MouseZeroKeyHoldDown;

        public event Action PausePressed;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (Time.timeScale > 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    RKeyPressed?.Invoke();

                if (Input.GetMouseButtonDown(0))
                    MouseZeroKeyPressed?.Invoke();

                if (Input.GetMouseButton(0))
                    MouseZeroKeyHoldDown?.Invoke();

                if (Input.GetButtonDown(Jump))
                    _playerMovement.Jump();

                _mouseX = Input.GetAxis(MouseX);
                _mouseY = Input.GetAxis(MouseY);
                _lookMouse.Rotate(_mouseX, _mouseY);
                IsRunning = Input.GetKey(KeyCode.LeftShift);
                IsCrouching = Input.GetKey(KeyCode.LeftControl);
                X = Input.GetAxis(Horizontal);
                Z = Input.GetAxis(Vertical);
            }
        }
    }
}