using Agava.YandexGames;
using UnityEngine;

namespace SDKContent
{
    public class SDKGameReady : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
YandexGamesSdk.GameReady();
             Debug.Log("READY!");
             Debug.Log("READY!");
             Debug.Log("READY!");
#endif
        }
    }
}