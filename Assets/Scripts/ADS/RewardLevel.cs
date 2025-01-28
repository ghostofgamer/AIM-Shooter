using UnityEngine;

namespace ADS
{
    public class RewardLevel : RewardAd
    {
        private const string RewardLevelKey = "RewardLevel";

        [SerializeField] private int _indexLevel;
        [SerializeField] private GameObject _block;

        private int _purchasedIndex = 1;

        protected override void OnReward()
        {
            PlayerPrefs.SetInt(RewardLevelKey + _indexLevel, _purchasedIndex);
            _block.SetActive(false);
        }
    }
}