using ADS;
using UnityEngine;

public class WeaponBlock : MonoBehaviour, IValueChanger
{
    private const string WeaponPurchase = "IsPurchaseWeapon";
    
    [SerializeField] private OpenWeaponAD _openWeaponAD;
    [SerializeField] private BoxCollider _weaponCollider;

    private bool _isPurchase;

    private void Start()
    {
        _isPurchase = PlayerPrefs.GetInt(WeaponPurchase, 0) > 0;

        if (_isPurchase)
        {
            _weaponCollider.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void ChangeValue()
    {
        _openWeaponAD.Show();
    }
}