using System;
using System.Collections;
using UnityEngine;

namespace WeaponContent
{
    public class WeaponSwitching : MonoBehaviour
    {
        [SerializeField] private int _selectedWeapon = -1;
        [SerializeField] private Gun[] _guns;

        private Coroutine _coroutine;
    
        public event Action<Gun> WeaponSwitched;
    
        public Gun _currentGun { get; private set; }

        private void Awake()
        {
            int indexWeapon= PlayerPrefs.GetInt("CurrentWeaponIndex",0);
            Select(indexWeapon);
        }
    
        private void SelectWeapon()
        {
            int i = 0;

            foreach (Transform weapon in transform)
            {
                weapon.gameObject.SetActive(i == _selectedWeapon);
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
}