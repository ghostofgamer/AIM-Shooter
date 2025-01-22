using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

    private List<LeaderboardElement> _leaderboardElements = new List<LeaderboardElement>();

    public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardElements)
    {
        ClearLeaderboard();

        foreach (LeaderboardPlayer player in leaderboardElements)
        {
            LeaderboardElement leaderboardElement = Instantiate(_leaderboardElementPrefab, _container);
            leaderboardElement.Initialize(player.Name, player.Rank, player.Score);

            _leaderboardElements.Add(leaderboardElement);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _leaderboardElements)
        {
            Debug.Log("колько удалений прошло ");
            Destroy(element);
            element.gameObject.SetActive(false);
        }

        _leaderboardElements = new List<LeaderboardElement>();
    }
}