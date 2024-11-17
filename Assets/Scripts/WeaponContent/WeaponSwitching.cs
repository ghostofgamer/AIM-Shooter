using System;
using System.Collections;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private int _selectedWeapon = -1;
    [SerializeField] private Gun[] _guns;

    private Coroutine _coroutine;
    
    public event Action<Gun> WeaponSwitched;
    
    public Gun _currentGun { get; private set; }

    private void Start()
    {
        // SelectWeapon();
        Select(0);
    }


    private void Update()
    {
        int previousSelectedWeapon = _selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_selectedWeapon >= transform.childCount - 1)
                _selectedWeapon = 0;
            else
                _selectedWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_selectedWeapon <= 0)
                _selectedWeapon = transform.childCount - 1;
            else
                _selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
            _selectedWeapon = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            _selectedWeapon = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            _selectedWeapon = 2;

        if (previousSelectedWeapon != _selectedWeapon)
            SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == _selectedWeapon);
            // weapon.gameObject.GetComponent<Gun>().ReadyGun();
            i++;
        }
    }

    public void Select(int index)
    {
        if (_selectedWeapon == index)
            return;

        if(_coroutine!=null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(StartSelectWeapon(index));
    }

    private IEnumerator StartSelectWeapon(int index)
    {
        yield return new WaitForSeconds(0.15f);
        _selectedWeapon = index;

        int i = 0;

        foreach (Gun gun in _guns)
        {
            _currentGun = _guns[_selectedWeapon];
            _currentGun.DefaultAmmo();
            gun.gameObject.SetActive(i == _selectedWeapon);
            i++;
        }
        
        WeaponSwitched?.Invoke(_currentGun);
    }
}