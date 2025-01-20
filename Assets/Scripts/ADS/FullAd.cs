using Agava.YandexGames;
using UnityEngine;

public class FullAd : AD
{
    public override void Show()
    {
        Debug.Log("Full Ad");
        
        if (YandexGamesSdk.IsInitialized)
        {
            Debug.Log("START Full Ad");
            InterstitialAd.Show(OnOpen, OnClose);
        }
    }
    
    public bool GetAdCompleted()
    {
        return IsAdCompleted;
    }
}
