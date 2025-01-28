using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeaderboardContent
{
    public class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerRank;
        [SerializeField] private TMP_Text _playerScore;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite[] _leaderPositionSprites;

        public void Initialize(string playerName, int rank, int score)
        {
            _playerName.text = playerName;
            _playerRank.text = rank.ToString();
            _playerScore.text = score.ToString();
        }

        public void ShowLeaderPosition(int position)
        {
            if (position >= 1 && position <= 3)
            {
                _image.gameObject.SetActive(true);
                _image.sprite = _leaderPositionSprites[position - 1];
            }
            else
            {
                _image.gameObject.SetActive(false);
            }
        }
    }
}