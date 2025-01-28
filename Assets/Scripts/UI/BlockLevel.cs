using ADS;
using UnityEngine;

public class BlockLevel : AbstractButton
{
    [SerializeField] private RewardLevel _rewardLevel;
    [SerializeField] private int _index;

    private void Start()
    {
        int purchasedIndex = PlayerPrefs.GetInt("RewardLevel" + _index, 0);

        if (purchasedIndex > 0)
            gameObject.SetActive(false);
    }


    protected override void OnClick()
    {
        Button.enabled = false;
        _rewardLevel.Show();
    }
}