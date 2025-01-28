using Agava.YandexGames;

namespace ADS
{
    public class FullAd : AD
    {
        public override void Show()
        {
            if (YandexGamesSdk.IsInitialized)
                InterstitialAd.Show(OnOpen, OnClose);
        }

        public bool GetAdCompleted()
        {
            return IsAdCompleted;
        }
    }
}