using Interfaces;
using UnityEngine;

public class ZombieModeButton : MonoBehaviour, IValueChanger
{
    [SerializeField] private int _index;
    [SerializeField] private ZombieMode _zombieMode;

    public void ChangeValue() => _zombieMode.ChangeMode(_index);
}