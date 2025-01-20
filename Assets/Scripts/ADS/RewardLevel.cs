using UnityEngine;

public class RewardLevel : RewardAd
{
    [SerializeField] private int _indexLevel;
    [SerializeField] private GameObject _block;

    private int _purchasedIndex = 1;

    protected override void OnReward()
    {
        PlayerPrefs.SetInt("RewardLevel" + _indexLevel, _purchasedIndex);
        _block.SetActive(false);
    }
}