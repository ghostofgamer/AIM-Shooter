using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerRank;
    [SerializeField] private TMP_Text _playerScore;

    public void Initialize(string playerName, int rank, int score)
    {
        _playerName.text = playerName;
        _playerRank.text = rank.ToString();
        _playerScore.text = score.ToString();
    }
}