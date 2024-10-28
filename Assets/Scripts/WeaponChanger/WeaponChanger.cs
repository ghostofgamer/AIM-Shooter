using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField] private WeaponSwitching _weaponSwitching;
    
    [field: SerializeField]public int Index { get;private set; }

    public void Change()
    {
        _weaponSwitching.Selected(Index);
    }
}
