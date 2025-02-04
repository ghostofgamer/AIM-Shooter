using ADS;
using UI.Buttons;
using UnityEngine;

namespace UI
{
    public class BlockLevel : AbstractButton
    {
        private const string RewardLevel = "RewardLevel";

        [SerializeField] private RewardLevel _rewardLevel;
        [SerializeField] private int _index;

        private int _purchasedIndex;

        private void Start()
        {
            _purchasedIndex = PlayerPrefs.GetInt(RewardLevel + _index, 0);

            if (_purchasedIndex > 0)
                gameObject.SetActive(false);
        }


        protected override void OnClick()
        {
            Button.enabled = false;
            _rewardLevel.Show();
        }
    }
}