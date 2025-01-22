using Agava.YandexGames;
using UnityEngine;

public class LeaderboardButton : AbstractButton
{
    [SerializeField]private YandexLeaderboard _leaderboard;
    [SerializeField]private AutorizationScreen _authScreen;
    
    protected override void OnClick()
    {
        if (PlayerAccount.IsAuthorized)
        {
            _leaderboard.gameObject.SetActive(true);
            PlayerAccount.RequestPersonalProfileDataPermission(_leaderboard.Fill);
        }
        else
        {
            _authScreen.gameObject.SetActive(true);
        }
    }
}
