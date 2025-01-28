using UnityEngine;

namespace ADS
{
    public class OpenWeaponAD : RewardAd
    {
        private const string WeaponPurchase = "IsPurchaseWeapon";

        [SerializeField] private GameObject _block;
        [SerializeField] private Collider _weaponCollider;

        private int _purchaseIndex = 1;

        protected override void OnReward()
        {
            PlayerPrefs.SetInt(WeaponPurchase, _purchaseIndex);
            _weaponCollider.enabled = true;
            _block.gameObject.SetActive(false);
        }
    }
}