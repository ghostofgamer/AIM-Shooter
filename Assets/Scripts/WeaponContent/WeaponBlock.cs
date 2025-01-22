using UnityEngine;

public class WeaponBlock : MonoBehaviour, IValueChanger
{
    [SerializeField] private OpenWeaponAD _openWeaponAD;
    [SerializeField] private BoxCollider _weaponCollider;

    private bool _isPurchase;

    private void Start()
    {
        _isPurchase = PlayerPrefs.GetInt("isPurchaseWeapon", 0) > 0;

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