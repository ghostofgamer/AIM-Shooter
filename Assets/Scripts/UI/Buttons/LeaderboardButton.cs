using Agava.YandexGames;
using LeaderboardContent;
using UnityEngine;

public class LeaderboardButton : AbstractButton
{
    [SerializeField]private YandexLeaderboard _leaderboard;
    [SerializeField]private AutorizationScreen _authScreen;
    [SerializeField]private GameObject _menu;
    
    protected override void OnClick()
    {
        if (PlayerAccount.IsAuthorized)
        {
            _menu.SetActive(false);
            _leaderboard.gameObject.SetActive(true);
            PlayerAccount.RequestPersonalProfileDataPermission(_leaderboard.Fill);
        }
        else
        {
            _menu.SetActive(false);
            _authScreen.gameObject.SetActive(true);
        }
    }
}
