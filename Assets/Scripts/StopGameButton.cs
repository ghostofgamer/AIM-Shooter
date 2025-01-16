using System;
using UnityEngine;

public class StopGameButton : MonoBehaviour, IValueChanger
{
    public event Action Stoping;

    public void ChangeValue() => Stoping?.Invoke();
}