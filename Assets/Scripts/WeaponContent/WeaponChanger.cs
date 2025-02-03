using Interfaces;
using UnityEngine;
using WeaponContent;

public class WeaponChanger : MonoBehaviour, IValueChanger
{
    [SerializeField] private WeaponSwitching _weaponSwitching;

    [field: SerializeField] public int Index { get; private set; }

    public void Stop()
    {
        _weaponSwitching.Select(Index);
        PlayerPrefs.SetInt("CurrentWeaponIndex", Index);
    }
}