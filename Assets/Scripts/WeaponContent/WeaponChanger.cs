using UnityEngine;

public class WeaponChanger : MonoBehaviour, IValueChanger
{
    [SerializeField] private WeaponSwitching _weaponSwitching;

    [field: SerializeField] public int Index { get; private set; }

    public void ChangeValue()
    {
        _weaponSwitching.Select(Index);
        PlayerPrefs.SetInt("CurrentWeaponIndex", Index);
    }
}