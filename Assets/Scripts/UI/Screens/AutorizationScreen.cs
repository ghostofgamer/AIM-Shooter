using Agava.YandexGames;
using UnityEngine;

public class AutorizationScreen : MonoBehaviour
{
    public void Autorize()
    {
        PlayerAccount.Authorize();
    }
}