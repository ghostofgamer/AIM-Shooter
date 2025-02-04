using Agava.YandexGames;

namespace UI.Screens
{
    public class AutorizationScreen : AbstractScreen
    {
        public void Autorize()
        {
            PlayerAccount.Authorize();
        }
    }
}