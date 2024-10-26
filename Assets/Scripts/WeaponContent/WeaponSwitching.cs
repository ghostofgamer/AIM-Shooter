using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private int _selectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
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
        
        if(previousSelectedWeapon!=_selectedWeapon)
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
}