using Agava.YandexGames;
using UnityEngine;

public class SDKGameReady : MonoBehaviour
{
    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
YandexGamesSdk.GameReady();
#endif
    }
}