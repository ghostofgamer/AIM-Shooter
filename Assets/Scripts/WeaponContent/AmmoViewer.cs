using TMPro;
using UnityEngine;

public class AmmoViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoValueText;
    [SerializeField] private WeaponSwitching _weaponSwitching;

    private Gun _currentGun;
    
    private void OnEnable()
    {
        _weaponSwitching.WeaponSwitched += InitializationWeapon;
    }

    private void OnDisable()
    {
        _weaponSwitching.WeaponSwitched -= InitializationWeapon;
    }

    private void ShowAmmoValue(int ammoValue)
    {
        _ammoValueText.text = $"{ammoValue} / <size=50>∞</size>";
    }

    private void InitializationWeapon(Gun gun)
    {
        _currentGun = gun;
        gun.AmmoChanged += ShowAmmoValue;
        ShowAmmoValue(_currentGun.CurrentAmmo);
    }
}