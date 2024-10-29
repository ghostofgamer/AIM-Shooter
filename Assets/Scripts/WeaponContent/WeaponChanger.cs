using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField] private WeaponSwitching _weaponSwitching;
    
    [field: SerializeField]public int Index { get;private set; }
}
