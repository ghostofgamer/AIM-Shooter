using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action RKeyPressed;

    public event Action MouseZeroKeyPressed;

    public event Action MouseZeroKeyHoldDown;

    public event Action PausePressed;

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
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
            PausePressed?.Invoke();
    }
}