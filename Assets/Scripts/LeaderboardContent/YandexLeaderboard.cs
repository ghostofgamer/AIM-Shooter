using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class YandexLeaderboard : MonoBehaviour
{
    private const string LeaderBoardName = "Leaderboard";
    private const string AnonymousName = "Anonymous";

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();

    [SerializeField] private LeaderboardView _leaderboardView;

    public void SetPlayerScore(int score)
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetPlayerEntry(LeaderBoardName, (result) =>
        {
            if (result == null || result.score < score)
            {
                Leaderboard.SetScore(LeaderBoardName, score);
            }
        });
    }

    public void Fill()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        _leaderboardPlayers.Clear();

        Leaderboard.GetEntries(LeaderBoardName, (result =>
        {
            foreach (var entry in result.entries)
            {
                int rank = entry.rank;
                int score = entry.score;
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = AnonymousName;

                _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
            }

            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
        }));
    }
}