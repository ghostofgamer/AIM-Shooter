using UnityEngine;

public class OpenWeaponAD : RewardAd
{
    [SerializeField] private GameObject _block;
    [SerializeField] private Collider _weaponCollider;

    private int _purchaseIndex = 1;

    protected override void OnReward()
    {
        PlayerPrefs.SetInt("isPurchaseWeapon", _purchaseIndex);
        _weaponCollider.enabled = true;
        _block.gameObject.SetActive(false);
    }
}