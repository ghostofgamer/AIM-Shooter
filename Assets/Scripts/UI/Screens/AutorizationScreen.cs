using Agava.YandexGames;
using UnityEngine;

public class AutorizationScreen : AbstractScreen
{
    public void Autorize()
    {
        PlayerAccount.Authorize();
    }
}