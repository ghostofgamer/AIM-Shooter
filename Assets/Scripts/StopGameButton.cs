using System;
using UnityEngine;

public class StopGameButton : MonoBehaviour, IValueChanger
{
    [SerializeField] private StartGame _startGame;
    
    private Collider _collider;
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
        _collider = GetComponent<Collider>();
    }

    public void ChangeValue()
    {
        Stoping?.Invoke();
        transform.position = _defaultPosition;
    }

    private void ButtonMove()
    {
        transform.position = new Vector3  (transform.position.x+3f, transform.position.y, _defaultPosition.z);
    }
}